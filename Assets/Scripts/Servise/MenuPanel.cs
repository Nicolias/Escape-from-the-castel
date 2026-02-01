using NaughtyAttributes;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MenuPanel : MonoBehaviour
{
    [SerializeField, Scene] private string _menuSceneName;

    [SerializeField] private Button _musicToggleButton;
    [SerializeField] private Button _mainMenuButton;

    [SerializeField] private Sprite _disableMusicSprite;
    [SerializeField] private Sprite _enableMusicSprite;

    private BackgroundMusic _musicManager;

    private void Awake()
    {
        if (_musicManager == null)
            _musicManager = FindAnyObjectByType<BackgroundMusic>();

        ValidateMusicSprite();
    }

    private void OnEnable()
    {
        if (_musicToggleButton != null)
            _musicToggleButton.onClick.AddListener(ToggleMusic);

        if (_mainMenuButton != null)
            _mainMenuButton.onClick.AddListener(ReturnToMainMenu);
    }

    private void OnDisable()
    {
        if (_musicToggleButton != null)
            _musicToggleButton.onClick.RemoveListener(ToggleMusic);

        if (_mainMenuButton != null)
            _mainMenuButton.onClick.RemoveListener(ReturnToMainMenu);
    }

    private void ToggleMusic()
    {
        if (_musicManager.IsEnable)
            _musicManager.Disable();
        else
            _musicManager.Enable();

        ValidateMusicSprite();
    }

    private void ReturnToMainMenu()
    {
        SceneManager.LoadScene(_menuSceneName);
    }

    private void ValidateMusicSprite()
    {
        _musicToggleButton.image.sprite =  _musicManager.IsEnable ? _enableMusicSprite : _disableMusicSprite;
    }
}