using NaughtyAttributes;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using YG;

namespace Asset.Menu
{
    public class EntryPoint : MonoBehaviour
    {
        [SerializeField] private Button _newGameButton;
        [SerializeField] private Button _continueGameButton;

        [SerializeField, Scene] private string _firstLevel;

        public void Awake()
        {
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

        private void NewGame()
        {
            YG2.SetDefaultSaves();
            YG2.saves.IsNewGame = false;
            SceneManager.LoadScene(_firstLevel);
        }

        private void LoadGame()
        {
            SceneManager.LoadScene(YG2.saves.CurrentLevelName);
        }
    }
}