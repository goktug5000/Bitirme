using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnObject : MonoBehaviour
{
    public Transform[] farCorners;
    public Transform[] closeCorners;
    public Transform[] soCloseCorners;
    public GameObject objectToSpawn;

    public GameObject lookAtTarget;

    private void Awake()
    {
        lookAtTarget = GameObject.Find("NpcRoot");
    }

    public GameObject SpawnObjectWithDistanceSelection(int distanceNumber)
    {
        if (distanceNumber == 1)
        {
            return SpawnObjectInsideBounds(soCloseCorners, "SoClose");
        }
        else if (distanceNumber == 2)
        {
            return SpawnObjectInsideBounds(closeCorners, "Close");
        }
        return SpawnObjectInsideBounds(farCorners, "Far");
    }

    GameObject SpawnObjectInsideBounds(Transform[] corners, string _location)
    {
        Vector3 min = corners[0].position;
        Vector3 max = corners[0].position;

        foreach (Transform corner in corners)
        {
            min = Vector3.Min(min, corner.position);
            max = Vector3.Max(max, corner.position);
        }

        float randomX = Random.Range(min.x, max.x);
        float randomY = Random.Range(min.y, max.y);
        float randomZ = Random.Range(min.z, max.z);

        Vector3 randomPosition = new Vector3(randomX, randomY, randomZ);
        var a = Instantiate(objectToSpawn, randomPosition, Quaternion.identity);
        LookAtTarget(a);
        a.GetComponent<PlayerRoot>().SetPlayerRoot("starting", _location);

        return a;
    }

    void LookAtTarget(GameObject a)
    {
        Vector3 direction = lookAtTarget.transform.position - a.transform.position;

        direction.y = 0;

        if (direction != Vector3.zero)
        {
            Quaternion rotation = Quaternion.LookRotation(direction);
            a.transform.rotation = rotation;
        }
    }
}
