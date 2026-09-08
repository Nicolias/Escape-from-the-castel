using UnityEngine;
using UnityEngine.UI;

public class Learning : MonoBehaviour
{
    [SerializeField] private Button _learningButton;
    [SerializeField] private LearningWindow _learningWindow;

    private void OnEnable()
    {
        _learningButton.onClick.AddListener(Open);
    }

    private void OnDisable()
    {
        _learningButton.onClick.RemoveListener(Open);
    }

    public void Open()
    {
        _learningWindow.gameObject.SetActive(true);
    }
}
