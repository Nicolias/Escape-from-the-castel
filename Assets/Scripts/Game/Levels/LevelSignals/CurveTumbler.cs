using System;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scripts.LevelSignals
{
    [RequireComponent(typeof(Slider))]
    public abstract class CurveTumbler : MonoBehaviour
    {
        private Curve _curve;
        private Slider _slider;

        public event Action CurveChanged;

        public void Init(Curve curve)
        {
            _slider = GetComponent<Slider>();
            _curve = curve;
            _curve.Init();
            SetCurve(_slider.value);
            _slider.onValueChanged.AddListener(SetCurve);
        }

        private void OnDisable() => _slider.onValueChanged.RemoveListener(SetCurve);

        protected abstract void ChangeCurve(Curve curve, float percent);

        private void SetCurve(float percent)
        {
            ChangeCurve(_curve, percent);

            CurveChanged?.Invoke();
        }
    }
}