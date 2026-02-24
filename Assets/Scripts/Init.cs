using NaughtyAttributes;
using Scripts.Servises;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Init : MonoBehaviour
{
    [SerializeField, Scene] private string _menu;
    [SerializeField] private Locolization _locolization;

    private void Awake()
    {
        SceneManager.LoadScene(_menu);
        _locolization.Initialize();
    }
}