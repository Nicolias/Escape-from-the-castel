using Asset.Servise;
using IJunior.TypedScenes;
using UnityEngine;
using YG;

public class Init : MonoBehaviour
{
    private GameData _gameData;

    private void Awake()
    {
        _gameData = new GameData();
        _gameData.CurrentLevelName = YG2.saves.CurrentLevelName;

        Menu.Load(_gameData);
    }
}
