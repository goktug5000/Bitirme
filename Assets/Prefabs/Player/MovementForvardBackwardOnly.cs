using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovementForvardBackwardOnly : MonoBehaviour
{
    private GameObject playerObj;

    void Start()
    {
        playerObj = PlayerConstantsHolder._playerConstantsHolder.holderObj;
    }

    // Update is called once per frame
    void Update()
    {
        PlayerMove();
    }

    private void PlayerMove()
    {
        Vector2 MovementVec = MovementVector();
        playerObj.transform.Translate(0, 0, MovementVec.x * Time.deltaTime * 4);
    }

    private Vector2 MovementVector()
    {
        Vector2 MovementVec = new Vector2();

        if (Input.GetKey(KeyBindings.KeyCodes[KeyBindings.KeyCode_Forward]))
        {
            MovementVec += new Vector2(1, 0);
        }
        if (Input.GetKey(KeyBindings.KeyCodes[KeyBindings.KeyCode_Back]))
        {
            MovementVec += new Vector2(-1, 0);
        }

        return MovementVec.normalized;
    }

}
