using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayAnim : MonoBehaviour
{
    [SerializeField] private Animator anim;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            anim.SetTrigger("1");
        }
        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            anim.SetTrigger("2");
        }
        if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            anim.SetTrigger("3");
        }
        if (Input.GetKeyDown(KeyCode.Alpha4))
        {
            anim.SetTrigger("4");
        }
        if (Input.GetKeyDown(KeyCode.Alpha5))
        {
            anim.SetTrigger("5");
        }
        if (Input.GetKeyDown(KeyCode.Alpha6))
        {
            anim.SetTrigger("6");
        }
        if (Input.GetKeyDown(KeyCode.Alpha7))
        {
            anim.SetTrigger("7");
        }
        if (Input.GetKeyDown(KeyCode.Alpha8))
        {
            anim.SetTrigger("8");
        }
    }
}
