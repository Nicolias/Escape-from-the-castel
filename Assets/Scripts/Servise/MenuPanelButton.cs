using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Button))]
public class MenuPanelButton : MonoBehaviour
{
    [SerializeField] private GameObject _menuPanel;

    private Button _selfButton;
    private bool _isActive;

    private void Awake()
    {
        _selfButton = GetComponent<Button>();
    }

    private void OnEnable()
    {
        _selfButton.onClick.AddListener(ToggleActive);
    }

    private void OnDisable()
    {
        _selfButton.onClick.RemoveListener(ToggleActive);
    }

    private void ToggleActive()
    {
        _isActive = !_isActive;
        _menuPanel.SetActive(_isActive);
    }
}