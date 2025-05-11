using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

[System.Serializable]
public class MainMenuScript : MonoBehaviour
{
    public static bool isGamePaused = false;

    [SerializeField] private GameObject menu;
    [SerializeField] private GameObject settingsMenu;

    void Start()
    {
        Resume();
        settingsMenu.SetActive(false);
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyBindings.KeyCodes[KeyBindings.KeyCode_MainMenu]))
        {
            OpenCloseMenu();
        }
    }

    void OpenCloseMenu()
    {
        var panel = PanelManager._panelManager.AnyOpen();
        if (panel != null && panel != settingsMenu)
        {
            return;
        }

        if (isGamePaused)
        {
            Resume();
        }
        else
        {
            Pause();
        }
    }

    public void Resume()
    {
        menu.SetActive(false);
        Time.timeScale = 1f;
        isGamePaused = false;
        HideCursor.LockCursorMode(true);
    }

    void Pause()
    {
        menu.SetActive(true);
        Time.timeScale = 0f;
        isGamePaused = true;
        HideCursor.LockCursorMode(false);
    }

    public void RestartSection()
    {
        Debug.Log("Restarting");
        Resume();
        StartCoroutine(loadThis(SceneManager.GetActiveScene().name));
    }

    static public bool isFullyLoaded;
    IEnumerator loadThis(string sceneName)
    {
        isFullyLoaded = false;
        SceneManager.LoadScene(sceneName);
        isFullyLoaded = true;

        yield return null;
    }

    public void OpenSettings()
    {
        settingsMenu.SetActive(!settingsMenu.activeSelf);
    }

    public void ExitGame()
    {
        Resume();
        Debug.Log("Exiting the game");
        Application.Quit();
    }
}
