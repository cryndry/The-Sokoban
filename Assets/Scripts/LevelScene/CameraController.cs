using Unity.Cinemachine;
using UnityEngine;

public class CameraController : LazySingleton<CameraController>
{
    [SerializeField] private CinemachineCamera cinemachineCam;

    public void SetTarget(Transform target)
    {
        cinemachineCam.Follow = target;
    }
}