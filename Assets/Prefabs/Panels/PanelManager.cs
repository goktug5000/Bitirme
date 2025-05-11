using UnityEngine;

public class PanelManager : MonoBehaviour
{
    public static PanelManager _panelManager;
    public GameObject[] panels;

    private void Awake()
    {
        if (_panelManager != null)
        {
            Destroy(this);
        }
        else
        {
            _panelManager = this;
        }
    }

    public GameObject AnyOpen()
    {
        for(int i = 0; i < panels.Length; i++)
        {
            if (panels[i].activeSelf)
            {
                return panels[i];
            }
        }
        return null;
    }
}
