using Asset.Servise;
using IJunior.TypedScenes;
using UnityEngine;
using UnityEngine.UI;
using YG;

namespace Asset.Menu
{
    public class EntryPoint : MonoBehaviour, ISceneLoadHandler<GameData>
    {
        private const string GameSaveKey = nameof(GameSaveKey);

        [SerializeField] private Button _newGameButton;
        [SerializeField] private Button _continueGameButton;

        private GameData _gameData;

        public void OnSceneLoaded(GameData gameData)
        {
            _gameData = gameData;
            _continueGameButton.interactable = YG2.saves.IsNewGame == false;
        }

        private void OnEnable()
        {
            _continueGameButton.onClick.AddListener(LoadGame);
            _newGameButton.onClick.AddListener(NewGame);
        }

        private void OnDisable()
        {
            _continueGameButton.onClick.RemoveListener(LoadGame);
            _newGameButton.onClick.RemoveListener(NewGame);
        }

        private void LoadGame()
        {
            Game.Load(_gameData);
        }

        private void NewGame()
        {
            YG2.SetDefaultSaves();
            YG2.saves.IsNewGame = false;
            Game.Load(_gameData);
        }
    }
}