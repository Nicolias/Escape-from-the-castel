using UnityEngine;

public class CameraPerspectiveFixer : MonoBehaviour
{
    [Header("Настройки камер")]
    [Tooltip("Камера, которая будет активна в горизонтальном режиме")]
    [SerializeField] private Camera _landscapeCamera;

    [Tooltip("Камера, которая будет активна в вертикальном режиме")]
    [SerializeField] private Camera _portraitCamera;

    private bool _isCurrentlyLandscape;

    void Start()
    {
        ForceUpdateCamera();
    }

    void Update()
    {
        bool checkLandscape = Screen.width > Screen.height;

        if (checkLandscape != _isCurrentlyLandscape)
            ForceUpdateCamera();
    }

    private void ForceUpdateCamera()
    {
        _isCurrentlyLandscape = Screen.width > Screen.height;

        if (_isCurrentlyLandscape)
        {
            _landscapeCamera.gameObject.SetActive(true);
            _portraitCamera.gameObject.SetActive(false);
        }
        else
        {
            _landscapeCamera.gameObject.SetActive(false);
            _portraitCamera.gameObject.SetActive(true);
        }
    }
}
