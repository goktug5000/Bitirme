using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.Networking;

public class AiAppService : MonoBehaviour
{
    public static AiAppService _aiAppService;
    private static readonly HttpClient client = new HttpClient();
    private DataProcessor processor;
    [SerializeField] private Animator anim;

    [Header("model iþlemleri")]
    public bool isInAnim;
    private List<Coroutine> activeRequests = new List<Coroutine>();
    public List<string> animCounter;

    [Header("konumlar")]
    [SerializeField] private Transform watcher_root;

    // TODO: bunlarý playerConstantsHolder dan falan otomatik çek
    [SerializeField] private Transform Head;

    [SerializeField] private Transform R_Shoulder;
    [SerializeField] private Transform R_ForeArm;
    [SerializeField] private Transform R_Hand;

    [SerializeField] private Transform L_Shoulder;
    [SerializeField] private Transform L_ForeArm;
    [SerializeField] private Transform L_Hand;

    [SerializeField] private Transform Spine2;

    [SerializeField] private Transform R_UpLeg;
    [SerializeField] private Transform R_Knee;
    [SerializeField] private Transform R_Foot;

    [SerializeField] private Transform L_UpLeg;
    [SerializeField] private Transform L_Knee;
    [SerializeField] private Transform L_Foot;

    void Awake()
    {
        _aiAppService = this;
    }

    void Start()
    {
        watcher_root = GameObject.Find("Watcher").transform;
        processor = new DataProcessor();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            StartCoroutine(SendPositionLoop());
            anim.SetBool("Response", true);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Player")
        {
            StopCoroutine(SendPositionLoop());
            anim.SetBool("Response", false);
        }
    }

    public void whosThatAnim()
    {
        var pokemon = animCounter.GroupBy(s => s)
            .OrderByDescending(g => g.Count())
            .First().Key;
        setAnim(pokemon);

        Debug.Log(pokemon);
        animCounter = new List<string>();
        activeRequests.Clear();
    }

    private void setAnim(string pokemon)
    {
        Debug.Log(pokemon);

        string[] parts = pokemon.Split('_');
        float AnimId = 0;
        
        for (int x = 0; x < 100; x++)
        {
            var distance = processor.UpdateDistanceEma(parts[2]);
            var direction = processor.UpdateDirectionEma(parts[0]);
            anim.SetFloat("Distance", distance);
            anim.SetFloat("Attack-Def", direction.x);
            anim.SetFloat("Dance-Others", direction.y);
            anim.SetTrigger("Res");
        }
        if (parts[0] == "O")
        {
            switch (parts[1])
            {
                case "Hello":
                    AnimId = 0;
                    break;
                case "Jump":
                    AnimId = 0.4f;
                    break;
                case "Gathering":
                    AnimId = 0.8f;
                    break;
            }
        }
        anim.SetFloat("AnimId", AnimId);
    }

    private IEnumerator SendPositionLoop()
    {
        while (true)
        {
            if (isInAnim)
            {
                Coroutine request = StartCoroutine(SendPositionRequestAsync());
                activeRequests.Add(request);
            }
            yield return new WaitForSeconds(0.1f);
        }
    }

    public IEnumerator SendPositionRequestAsync()
    {
        string innerText = PointsLocation(Head);

        innerText += PointsLocation(R_Shoulder);
        innerText += PointsLocation(R_ForeArm);
        innerText += PointsLocation(R_Hand);

        innerText += PointsLocation(L_Shoulder);
        innerText += PointsLocation(L_ForeArm);
        innerText += PointsLocation(L_Hand);

        innerText += PointsLocation(Spine2);

        innerText += PointsLocation(R_UpLeg);
        innerText += PointsLocation(R_Knee);
        innerText += PointsLocation(R_Foot);

        innerText += PointsLocation(L_UpLeg);
        innerText += PointsLocation(L_Knee);
        innerText += PointsLocation(L_Foot);
        innerText = innerText.Substring(0, innerText.Length - 1);
        string json = "{\"input\": [" + innerText + "]}";

        using (UnityWebRequest request = new UnityWebRequest("http://127.0.0.1:5000/predict", "POST"))
        {
            byte[] postData = Encoding.UTF8.GetBytes(json);
            request.uploadHandler = new UploadHandlerRaw(postData);
            request.downloadHandler = new DownloadHandlerBuffer();
            request.SetRequestHeader("Content-Type", "application/json");

            yield return request.SendWebRequest();

            if (request.result == UnityWebRequest.Result.Success)
            {
                animCounter.Add(request.downloadHandler.text);
            }
            else
            {
                Debug.LogError("Request error: " + request.error);
            }
        }
    }

    string PointsLocation(Transform inputObj)
    {
        Vector3 worldPositionObjectA = watcher_root.position;
        Vector3 worldPositionObjectB = inputObj.position;
        Vector3 relativePositionInput = worldPositionObjectA - worldPositionObjectB;

        return (
            //inputObj.gameObject.name +
            relativePositionInput.x.ToString(CultureInfo.InvariantCulture)
            + "," + relativePositionInput.y.ToString(CultureInfo.InvariantCulture)
            + "," + relativePositionInput.z.ToString(CultureInfo.InvariantCulture)
            + ",");
    }
}
