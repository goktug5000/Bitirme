using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerRoot : MonoBehaviour
{
    public string CurrentAnim;
    public string CurrentLocation;

    public void SetPlayerRoot(string _anim, string _location)
    {
        CurrentAnim = _anim;
        CurrentLocation = _location;
    }


    public void TurnAFew()
    {

    }
}
