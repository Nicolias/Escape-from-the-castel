using Cryptography.Panels;
using Cryptography.Servis;
using NaughtyAttributes;
using Scripts.Servises;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Cryptography
{
    public class EntryPoint : MonoBehaviour
    {
        [SerializeField] private CiphersChanger _ciphersChanger;
        [SerializeField] private Timer _timer;
        [SerializeField] private FailsCounter _failCounter;
        [SerializeField] private Tutorial _tutorial;
        [SerializeField] private Locolizer _alphabet;
        [SerializeField] private Helper _helper;

        [SerializeField] private WinPanel _winPanel;
        [SerializeField] private LosePanel _losePanel;

        [SerializeField] private Locolization _locolization;

        [SerializeField] private CipherView _cipherView;
        [SerializeField, Scene] private string _menuScene;

        private void Awake()
        {
            _failCounter.Initialize(_cipherView);
            _alphabet.Initialize();
            _helper.Initialize();
            _timer.Initialize();
            _ciphersChanger.Initialize(_cipherView, _helper, _timer, _alphabet);
            _locolization.Initialize();
        }

        private void OnEnable()
        {
            _tutorial.End += Enable;
        }

        private void OnDisable()
        {
            _tutorial.End -= Enable;
        }

        public void Reset()
        {
            Enable();
            _failCounter.Reseting();
        }

        public void Exit()
        {
            SceneManager.LoadScene(_menuScene);
        }

        private void Enable()
        {
            _ciphersChanger.Enable();
            _failCounter.Enable();

            _ciphersChanger.Win += OnWin;
            _failCounter.Lose += OnLose;
        }

        private void Disable()
        {
            _ciphersChanger.Disable();
            _failCounter.Disable();

            _ciphersChanger.Win -= OnWin;
            _failCounter.Lose -= OnLose;
        }

        private void OnWin()
        {
            _winPanel.Enable();
            _timer.Stop();
            Disable();
        }

        private void OnLose()
        {
            _losePanel.Enable();
            _timer.Stop();
            Disable();
        }
    }
}