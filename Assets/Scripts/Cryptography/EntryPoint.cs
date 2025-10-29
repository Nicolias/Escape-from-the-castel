using Cryptography.Panels;
using Cryptography.Servis;
using NaughtyAttributes;
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

        [SerializeField] private WinPanel _winPanel;
        [SerializeField] private LosePanel _losePanel;

        [SerializeField] private CipherView _cipherView;

        [SerializeField, Scene] private string _menuScene;

        private void Awake()
        {
            _failCounter.Initialize(_cipherView);
            _alphabet.Initialize();
            _ciphersChanger.Initialize(_cipherView);
            _timer.Initialize();
        }

        private void OnEnable()
        {
            _tutorial.End += Enable;
            
            _ciphersChanger.Win += OnWin;
            _failCounter.Lose += OnLose;
        }

        private void OnDisable()
        {
            _tutorial.End -= Enable;

            _ciphersChanger.Disable();
            _failCounter.Disable();

            _ciphersChanger.Win -= OnWin;
            _failCounter.Lose -= OnLose;
        }

        public void Reset()
        {
            _ciphersChanger.Reset();
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
        }

        private void OnWin()
        {
            _winPanel.Enable(_timer.CurrentTime);
            _timer.Stop();
        }

        private void OnLose()
        {
            _losePanel.Enable(_timer.CurrentTime);
            _timer.Stop();
        }
    }
}