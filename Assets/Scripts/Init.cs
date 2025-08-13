using Asset.Servise;
using NaughtyAttributes;
using UnityEngine;
using UnityEngine.SceneManagement;
using YG;

public class Init : MonoBehaviour
{
    [SerializeField, Scene] private string _menu;

    private GameData _gameData;

    private void Awake()
    {
        _gameData = new GameData();
        _gameData.CurrentLevelName = YG2.saves.CurrentLevelName;

        SceneManager.LoadScene(_menu);
    }
}
