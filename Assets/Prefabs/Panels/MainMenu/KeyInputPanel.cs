using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class KeyInputPanel : MonoBehaviour
{
    IKeyInputOpener keyInputOpener;

    public void SetMyOpener(IKeyInputOpener iKeyInputOpener)
    {
        keyInputOpener = iKeyInputOpener;
    }

    void Update()
    {
        foreach (KeyCode vkey in Enum.GetValues(typeof(KeyCode)))
        {
            if (Input.GetKey(vkey))
            {
                Debug.Log("keycode down: " + vkey);
                keyInputOpener.SetInputValue(vkey);
                Destroy(this.gameObject);
            }
        }
    }
}
