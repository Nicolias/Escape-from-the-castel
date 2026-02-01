using System;
using UnityEngine;

namespace Assets.Scripts.LevelSignals
{
    public class LevelSignals : Level
    {
        [SerializeField] private Curve _targetCurve;
        [SerializeField] private Curve _playerCurve;
        [SerializeField] private HzCurveTumbler _hzTumbler;
        [SerializeField] private AmplitudeCurveTumbler _amplitudeTumbler;
        [SerializeField] private float _hzToleranceValue;
        [SerializeField] private float _amplitudeToleranceValue;

        public override event Action Complete;

        public override void Init()
        {
            _targetCurve.Init();
            _hzTumbler.Init(_playerCurve);
            _amplitudeTumbler.Init(_playerCurve);

            _hzTumbler.CurveChanged += CheckComplete;
            _amplitudeTumbler.CurveChanged += CheckComplete;
        }

        private void OnDisable()
        {
            _hzTumbler.CurveChanged -= CheckComplete;
            _amplitudeTumbler.CurveChanged -= CheckComplete;
        }

        private void CheckComplete()
        {
            if (Mathf.Abs(_targetCurve.Hz - _playerCurve.Hz) <= _hzToleranceValue)
            {
                if (Mathf.Abs(_targetCurve.Amplitude - _playerCurve.Amplitude) <= _amplitudeToleranceValue)
                {
                    Complete?.Invoke();
                }
            }
        }
    }
}