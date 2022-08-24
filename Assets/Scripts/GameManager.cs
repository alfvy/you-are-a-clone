using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] Cinemachine.CinemachineVirtualCamera _virtualCamera;
    [SerializeField] Cinemachine.CinemachineConfiner2D _cameraConfiner;
    [SerializeField] CloneManager _cloneManager;

    public static Cinemachine.CinemachineVirtualCamera VirtualCamera;
    public static Cinemachine.CinemachineConfiner2D CameraConfiner;
    public static CloneManager CloneManager;

    // Start is called before the first frame update
    void Awake()
    {
        VirtualCamera = _virtualCamera;
        CameraConfiner = _cameraConfiner;
        CloneManager = _cloneManager;
    }
}
