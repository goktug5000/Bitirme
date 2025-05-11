using System.Collections.Generic;
using UnityEngine;

public class KeyBindings
{
    [Header("Keyboard")]
    [SerializeField] public static Dictionary<string, KeyCode> KeyCodes;

    [SerializeField] public static string KeyCode_MainMenu = "KeyCode_MainMenu";
    [SerializeField] public static string KeyCode_Inventory = "KeyCode_Inventory";
    [SerializeField] public static string KeyCode_HideCursor = "KeyCode_HideCursor";

    [SerializeField] public static string KeyCode_Left = "KeyCode_Left";
    [SerializeField] public static string KeyCode_Right = "KeyCode_Right";
    [SerializeField] public static string KeyCode_Back = "KeyCode_Back";
    [SerializeField] public static string KeyCode_Forward = "KeyCode_Forward";

    [SerializeField] public static string KeyCode_Jump = "KeyCode_Jump";
    [SerializeField] public static string KeyCode_Dash = "KeyCode_Dash";

    [SerializeField] public static string KeyCode_Attack1 = "KeyCode_Attack1";
    [SerializeField] public static string KeyCode_Attack2 = "KeyCode_Attack2";

    [SerializeField] public static string KeyCode_Interaction = "KeyCode_Interaction";


    [Header("Gamepad")]
    [SerializeField] public static string KeyCode_joy_Esc = "KeyCode_joy_Esc";
}
