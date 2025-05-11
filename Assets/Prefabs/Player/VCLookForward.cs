using UnityEngine;

public class VCLookForward : MonoBehaviour
{
    public static VCLookForward _vCLookForward;

    [SerializeField] private Transform player;
    [SerializeField] private Transform camRoot;

    [SerializeField] private float mouseSensitivityX = 600;
    [SerializeField] private float mouseSensitivityY = 400;
    [SerializeField] private float xRotation = 90;
    [SerializeField] private float yRotation = 0;

    void Awake()
    {
        if (_vCLookForward != null)
        {
            Destroy(this);
        }
        else
        {
            _vCLookForward = this;
        }
    }

    void Update()
    {
        if (Cursor.lockState == CursorLockMode.Locked)
        {
            MouseLook();
        }
    }

    void MouseLook()
    {
        float mouseX = Input.GetAxis("Mouse X") * mouseSensitivityX * Time.deltaTime;
        float mouseY = Input.GetAxis("Mouse Y") * mouseSensitivityY * Time.deltaTime;

        xRotation -= mouseY;
        yRotation += mouseX;
        xRotation = Mathf.Clamp(xRotation, -90, 70);


        camRoot.localRotation = Quaternion.Euler(xRotation, yRotation, 0);
    }

    public float GetCamAngle()
    {
        float angle = camRoot.transform.eulerAngles.y;
        return angle;
    }
}
