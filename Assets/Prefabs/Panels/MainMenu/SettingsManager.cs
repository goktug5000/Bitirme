using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SettingsManager : MonoBehaviour
{
    [SerializeField] public GameObject keyBindingsPanel;
    [SerializeField] public GameObject voicePanel;

    private void Awake()
    {
        CloseAll();
    }

    public void KeyBindingsPanel()
    {
        CloseAll();
        keyBindingsPanel.SetActive(true);
    }

    public void VoicePanel()
    {
        CloseAll();
        voicePanel.SetActive(true);
    }

    public void CloseAll()
    {
        keyBindingsPanel.SetActive(false);
        voicePanel.SetActive(false);
    }
}