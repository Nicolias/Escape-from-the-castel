using Asset.Servise;
using IJunior.TypedScenes;
using UnityEngine;

public class Init : MonoBehaviour
{
    private GameData _gameData;

    private void Awake()
    {
        _gameData = new GameData();

        Menu.Load(_gameData);
    }
}
