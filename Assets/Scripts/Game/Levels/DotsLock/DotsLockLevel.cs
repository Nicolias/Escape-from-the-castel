using System;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scripts.DotsLevel
{
    public class DotsLockLevel : Level
    {
        [SerializeField] private LightCircle _lightCircle;
        [SerializeField] private LockCircle _lockCircle;
        [SerializeField] private Button _rotateButton;
        [SerializeField] private Button _switchButton;

        public override event Action Complet;

        public override void Init()
        {
            _lockCircle.Init();
            _lightCircle.Init();
            _lightCircle.Changed += OnCircleChanged;
            _rotateButton.onClick.AddListener(() => _lightCircle.Rotate());
            _switchButton.onClick.AddListener(() => _lightCircle.SwitchBottomItems());

            OnCircleChanged();
        }

        private void OnDisable()
        {
            _rotateButton.onClick.RemoveAllListeners();
            _switchButton.onClick.RemoveAllListeners();
        }

        private void OnCircleChanged() =>_lockCircle.StartCoroutine(_lockCircle.ValidateItems(_lightCircle, () => Complet?.Invoke()));
    }
}