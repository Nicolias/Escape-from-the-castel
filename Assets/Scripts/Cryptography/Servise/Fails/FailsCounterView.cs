using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Cryptography.Servis
{
    public class FailsCounterView : MonoBehaviour
    {
        [SerializeField] private FailsCounter _model;
        [SerializeField] private List<Image> _failSignals;

        private void Awake()
        {
            Reset();
        }

        private void OnEnable()
        {
            _model.Faild += OnFaild;
            _model.Reset += Reset;
        }

        private void OnDisable()
        {
            _model.Faild -= OnFaild;
            _model.Reset -= Reset;
        }

        public void Reset()
        {
            _failSignals.ForEach(signal => signal.color = Color.green);
        }

        private void OnFaild()
        {
            if (_model.CurrentFailCount <= _failSignals.Count)
                _failSignals[_model.CurrentFailCount - 1].color = Color.red;
        }
    }
}