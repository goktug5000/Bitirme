using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System;
using System.Globalization;

public class LogCreater : MonoBehaviour
{
    string filePath;
    string formattedTime;

    public PlayerRoot playerRoot;
    [SerializeField] private Animator animator;

    [SerializeField] private Transform watcher_root;

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


    void Start()
    {
        watcher_root = GameObject.Find("Watcher").transform;
        playerRoot = this.gameObject.transform.parent.GetComponent<PlayerRoot>();
        //GetExeLocation();
        //CreateAndWriteToFile();

        CheckAndCreateFile();
    }

    void Update()
    {
        // timer += Time.deltaTime;
        if (playerRoot.CurrentAnim != "starting")
        {
            AppendToFile(TimeNamePos());
        }
    }

    string TimeNamePos()
    {
        //formattedTime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff");
        // formattedTime = timer.ToString();

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

        return (innerText + " " + playerRoot.CurrentAnim + "_" + playerRoot.CurrentLocation);
        //return ("[" + formattedTime + "] " + innerText); // saati kapattým test için
    }

    string PointsLocation(Transform inputObj)
    {
        Vector3 worldPositionObjectA = watcher_root.position;
        Vector3 worldPositionObjectB = inputObj.position;
        Vector3 relativePositionInput = worldPositionObjectA - worldPositionObjectB;

        return (
            //inputObj.gameObject.name +
            relativePositionInput.x.ToString(CultureInfo.InvariantCulture)
            + " " + relativePositionInput.y.ToString(CultureInfo.InvariantCulture)
            + " " + relativePositionInput.z.ToString(CultureInfo.InvariantCulture)
            + " ");
    }

    void GetExeLocation()
    {
        string exeDirectory = Directory.GetParent(Application.dataPath).ToString();
        string filePath = Path.Combine(exeDirectory, @"Datas" + @"\" + "Data" + LogCreatorManager._logCreatorManager.dataNumber.ToString() + ".txt");
        Debug.Log(filePath);
    }

    void CreateAndWriteToFile()
    {
        if (File.Exists(filePath))
        {
            formattedTime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff");
            //AppendToFile("Created new file" + formattedTime);
            return;
        }
        //string content = "File Created" + "\n";
        //File.WriteAllText(filePath, content);
        Debug.Log("File created and written to: " + filePath);
    }

    void AppendToFile(string newText)
    {
        if (!string.IsNullOrEmpty(filePath))
        {
            File.AppendAllText(filePath, newText + "\n");
        }
    }

    public void CheckAndCreateFile()
    {
        string exeDirectory = Directory.GetParent(Application.dataPath).ToString();
        string _filePath = Path.Combine(exeDirectory, @"Datas", "Data" + LogCreatorManager._logCreatorManager.dataNumber.ToString() + ".txt");
        Debug.Log(_filePath);

        if (File.Exists(_filePath))
        {
            return;
        }

        File.WriteAllText(_filePath, "");
        filePath = _filePath;
    }
}
