using UnityEngine;
using Unity.Cinemachine;

public class CameraManager : MonoBehaviour
{
    [SerializeField] private CinemachinePanTilt panTilt;
    [SerializeField] private CinemachineInputAxisController cameraInput;

    public float PanAxis => panTilt.PanAxis.Value;

    public void SetCameraInputEnabled(bool isActive)
    {
        cameraInput.enabled = isActive;
    }

    public void ResetCameraRotation()
    {
        panTilt.PanAxis.Value = 0;
        panTilt.TiltAxis.Value = 0;
    }

    public void SetPanAxisValue(float panValue)
    {
        panTilt.PanAxis.Value = panValue;
    }

    public void SetTiltAxisValue(float tiltValue)
    {
        panTilt.TiltAxis.Value = tiltValue;
    }
}
