using DG.Tweening;
using NaughtyAttributes;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using YG;

namespace Asset.Menu
{
    public class EntryPoint : MonoBehaviour
    {
        [SerializeField] private GameObject _buttons;
        [SerializeField] private Intro _intro;

        [SerializeField] private Button _newGameButton;
        [SerializeField] private Button _continueGameButton;
        [SerializeField] private Button _ciphersGameButton;
        [SerializeField] private Button _leaderboardButton;

        [SerializeField, Scene] private string _firstLevel;
        [SerializeField, Scene] private string _ciphersLevel;

        [SerializeField] private Animator _safeBoxAnimator;
        [SerializeField] private Camera _camera;

        [SerializeField] private Leaderboard _leaderboard;

        public void Awake()
        {
            _continueGameButton.interactable = YG2.saves.IsNewGame == false;
            _leaderboard.Initialize();
            //_locolization.Initialize();
        }

        private void OnEnable()
        {
            _continueGameButton.onClick.AddListener(LoadGame);
            _newGameButton.onClick.AddListener(NewGame);
            _ciphersGameButton.onClick.AddListener(OpenCiphersGame);
            _leaderboardButton.onClick.AddListener(OpenLeaderboard);
        }

        private void OnDisable()
        {
            _continueGameButton.onClick.RemoveListener(LoadGame);
            _newGameButton.onClick.RemoveListener(NewGame);
            _ciphersGameButton.onClick.RemoveListener(OpenCiphersGame);
            _leaderboardButton.onClick.RemoveListener(OpenLeaderboard);
        }

        private void NewGame()
        {
            YG2.SetDefaultSaves();
            YG2.saves.IsNewGame = false;
            PlayAnimations(_firstLevel);
        }

        private void LoadGame()
        {
            PlayAnimations(YG2.saves.CurrentLevelName);
        }

        private void OpenCiphersGame()
        {
            PlayAnimations(_ciphersLevel);
        }

        private void OpenLeaderboard()
        {
            _leaderboard.Open();
        }

        private void PlayAnimations(string sceneName)
        {
            _intro.Kill();

            Sequence sequence = DOTween.Sequence();

            sequence
                .Append(_buttons.transform.DOMoveX(0.8f, 1.5f))
                .AppendInterval(1.5f)
                .AppendCallback(() => SceneManager.LoadScene(sceneName));
            sequence.Play();
            
        }
    }
}