using UnityEngine;
using UnityEngine.UI;

public class LearningWindow : MonoBehaviour
{
    [SerializeField] private Button _learningButton;

    private void OnEnable()
    {
        _learningButton.onClick.AddListener(Close);
    }

    private void OnDisable()
    {
        _learningButton.onClick.RemoveListener(Close);
    }

    public void Close()
    {
        gameObject.SetActive(false);
    }
}