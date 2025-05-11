using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System.Linq;

public class UI_KeyCode : MonoBehaviour, IKeyInputOpener
{
    [SerializeField] private TextMeshProUGUI myText;
    [SerializeField] private TextMeshProUGUI myButtonText;
    [SerializeField] private GameObject KeyInputPanel;

    public void SetUIKey(string keyName, KeyCode keyCode)
    {
        myText.text = keyName;
        myButtonText.text = keyCode.ToString();
    }

    public void UpdateKey(KeyCode newKey)
    {
        if (KeyBindings.KeyCodes.Values.Any(x => x == newKey))
        {
            Debug.LogWarning("kullanýlan karakter");
            return;
        }
        KeyBindings.KeyCodes[myText.text] = newKey;
        myButtonText.text = newKey.ToString();
    }

    public void SetInputValue(KeyCode keyCode)
    {
        UpdateKey(keyCode);
    }

    public void OpenKeyInputPanel()
    {
        var keyInput = Instantiate(KeyInputPanel);
        keyInput.GetComponent<KeyInputPanel>().SetMyOpener(this);
    }
}
