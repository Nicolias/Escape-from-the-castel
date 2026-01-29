using System;
using UnityEngine;

namespace Assets.Scripts.DotsLevel
{
    public class DotsLockLevel : Level
    {
        [SerializeField] private LightCircle _lightCircle;
        [SerializeField] private LockCircle _lockCircle;

        public override event Action Complet;

        public override void Init()
        {
            _lockCircle.Init();
            _lightCircle.Init();
            _lightCircle.Changed += OnCircleChanged;

            OnCircleChanged();
        }

        private void OnCircleChanged() =>_lockCircle.StartCoroutine(_lockCircle.ValidateItems(_lightCircle, () => Complet?.Invoke()));

        private void Update()
        {
            if (_lightCircle.IsMoving)
            {
                return;
            }

            if (Input.GetKeyDown(KeyCode.R))
            {
                _lightCircle.Rotate();
                return;
            }

            if (Input.GetKeyDown(KeyCode.G))
            {
                _lightCircle.SwitchBottomItems();
                return;
            }
        }
    }
}