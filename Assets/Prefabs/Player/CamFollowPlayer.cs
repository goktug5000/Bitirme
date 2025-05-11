using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CamFollowPlayer : MonoBehaviour
{
    private Transform playerObj;

    void Start()
    {
        playerObj = PlayerConstantsHolder._playerConstantsHolder.holderObj.transform;
    }

    void Update()
    {
        transform.position = new Vector3(playerObj.position.x, playerObj.position.y + 2, playerObj.position.z);
    }
}
