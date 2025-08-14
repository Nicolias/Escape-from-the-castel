using NaughtyAttributes;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Init : MonoBehaviour
{
    [SerializeField, Scene] private string _menu;

    private void Awake()
    {
        SceneManager.LoadScene(_menu);
    }
}
