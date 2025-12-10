using System;
using System.Collections;
using TMPro;
using UnityEngine;
using YG;

namespace Cryptography.Servis
{
    public class Timer : MonoBehaviour
    {
        [SerializeField] private TMP_Text _view;

        private Coroutine _counter;

        public TimeSpan CurrentTime { get; private set; }

        public void Initialize()
        {
            _view.text = CurrentTime.ToString();
        }

        public void Play()
        {
            CurrentTime = TimeSpan.Zero;

            if (_counter != null)
                StopCoroutine(_counter);

            _counter = StartCoroutine(TimeCount());
        }

        public void Stop()
        {
            if (_counter != null)
                StopCoroutine(_counter);

            _counter = null;
        }

        private IEnumerator TimeCount()
        {
            WaitForSeconds waitOneSecond = new WaitForSeconds(1);

            while (true)
            {
                _view.text = CurrentTime.ToString();
                yield return waitOneSecond;
                CurrentTime = CurrentTime.Add(TimeSpan.FromSeconds(1));
            }
        }
    }
}