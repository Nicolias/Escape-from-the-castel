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
        [SerializeField] private Button _ciphersGameButton;

        [SerializeField, Scene] private string _firstLevel;
        [SerializeField, Scene] private string _ciphersLevel;

        public void Awake()
        {
            _continueGameButton.interactable = YG2.saves.IsNewGame == false;
        }

        private void OnEnable()
        {
            _continueGameButton.onClick.AddListener(LoadGame);
            _newGameButton.onClick.AddListener(NewGame);
            _ciphersGameButton.onClick.AddListener(OpenCiphersGame);
        }

        private void OnDisable()
        {
            _continueGameButton.onClick.RemoveListener(LoadGame);
            _newGameButton.onClick.RemoveListener(NewGame);
            _ciphersGameButton.onClick.RemoveListener(OpenCiphersGame);
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

        private void OpenCiphersGame()
        {
            SceneManager.LoadScene(_ciphersLevel);
        }
    }
}