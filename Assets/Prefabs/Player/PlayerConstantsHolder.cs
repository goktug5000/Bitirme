using UnityEngine;
using Cinemachine;

public class PlayerConstantsHolder : ConstantHolder
{
    public static PlayerConstantsHolder _playerConstantsHolder;

    [Header("Gülücük")]
    [SerializeField] public Movement playerMovement;

    [SerializeField] public CinemachineVirtualCamera virtualCameraTP;
    [SerializeField] public CinemachineVirtualCamera virtualCameraTPS;
    [SerializeField] public Camera mainCamera;
    [SerializeField] public GameObject[] hitBoxes = new GameObject[2];

    void Awake()
    {
        if (_playerConstantsHolder != null)
        {
            Destroy(this);
        }
        else
        {
            _playerConstantsHolder = this;
        }
    }
}
