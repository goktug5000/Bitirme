using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LogCreatorManager : MonoBehaviour
{
    public static LogCreatorManager _logCreatorManager;
    public int dataNumber;
    public int distanceNumber;
    public Transform spawnParent;
    public int runningAnimsLimit;

    public SpawnObject spawnObject;

    private void Awake()
    {
        if (_logCreatorManager == null)
        {
            _logCreatorManager = this;
        }
        else
        {
            Destroy(this);
        }
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            _logCreatorManager.dataNumber++;
            var a = spawnObject.SpawnObjectWithDistanceSelection(1);
            a.transform.parent = spawnParent;
        }
        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            _logCreatorManager.dataNumber++;
            var a = spawnObject.SpawnObjectWithDistanceSelection(2);
            a.transform.parent = spawnParent;
        }
        if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            _logCreatorManager.dataNumber++;
            var a = spawnObject.SpawnObjectWithDistanceSelection(3);
            a.transform.parent = spawnParent;
        }
    }

    public void CheckForNew()
    {
        if (runningAnimsLimit < spawnParent.childCount)
        {
            return;
        }
        int randomNumber = Random.Range(1, 4);
        _logCreatorManager.dataNumber++;
        var a = spawnObject.SpawnObjectWithDistanceSelection(randomNumber);
        a.transform.parent = spawnParent;
    }
}
