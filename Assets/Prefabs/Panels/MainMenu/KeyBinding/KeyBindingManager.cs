using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeyBindingManager : MonoBehaviour
{
    [SerializeField] private GameObject KeyObj;
    [SerializeField] private Transform KeysParent;

    private void Awake()
    {
        SetBindings();
        ShowKeys();
    }

    public void SetBindings()
    {
        KeyBindings.KeyCodes = new Dictionary<string, KeyCode>();
        KeyBindings.KeyCodes.Add(KeyBindings.KeyCode_MainMenu, KeyCode.Escape);
        KeyBindings.KeyCodes.Add(KeyBindings.KeyCode_Inventory, KeyCode.I);
        KeyBindings.KeyCodes.Add(KeyBindings.KeyCode_HideCursor, KeyCode.Mouse2);

        KeyBindings.KeyCodes.Add(KeyBindings.KeyCode_Left, KeyCode.A);
        KeyBindings.KeyCodes.Add(KeyBindings.KeyCode_Right, KeyCode.D);
        KeyBindings.KeyCodes.Add(KeyBindings.KeyCode_Back, KeyCode.S);
        KeyBindings.KeyCodes.Add(KeyBindings.KeyCode_Forward, KeyCode.W);

        KeyBindings.KeyCodes.Add(KeyBindings.KeyCode_Jump, KeyCode.Space);
        KeyBindings.KeyCodes.Add(KeyBindings.KeyCode_Dash, KeyCode.LeftShift);

        KeyBindings.KeyCodes.Add(KeyBindings.KeyCode_Attack1, KeyCode.Mouse0);
        KeyBindings.KeyCodes.Add(KeyBindings.KeyCode_Attack2, KeyCode.Mouse1);

        KeyBindings.KeyCodes.Add(KeyBindings.KeyCode_Interaction, KeyCode.E);
    }

    public void ShowKeys()
    {
        int count = 0;
        foreach(var keyCode in KeyBindings.KeyCodes)
        {
            var newObj = Instantiate(KeyObj, KeysParent);
            newObj.GetComponent<UI_KeyCode>().SetUIKey(keyCode.Key, keyCode.Value);

            Vector2 newPosition = newObj.GetComponent<RectTransform>().anchoredPosition;
            newPosition.y = -120 * count;
            count++;
            newObj.GetComponent<RectTransform>().anchoredPosition = newPosition;
        }
    }
}
