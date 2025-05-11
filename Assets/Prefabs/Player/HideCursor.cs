using UnityEngine;

public class HideCursor : MonoBehaviour
{
    private void Start()
    {
        LockCursorMode(true);
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyBindings.KeyCodes[KeyBindings.KeyCode_HideCursor]))
        {
            bool isCursorLocked = Cursor.lockState == CursorLockMode.Locked;
            isCursorLocked = !isCursorLocked;
            LockCursorMode(isCursorLocked);
        }
    }

    public static void LockCursorMode(bool isLock)
    {
        if (isLock)
        {
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
        }
        else
        {
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
        }
    }
}
