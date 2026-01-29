using DG.Tweening;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scripts.DotsLevel
{
    public class LockItem : CirclePoint
    {
        [SerializeField] private Image _lockColor;
        [SerializeField] private RectTransform _shutter;

        private float _moveDistance = 0.15f;
        private Vector2 _openPosition;
        private Vector2 _closePosition;

        public override void Init()
        {
            base.Init();
            if (IsActive)
            {
                _lockColor.color = Color; 
                _openPosition = _shutter.anchoredPosition;
                _closePosition = _shutter.anchoredPosition + (Vector2)(_shutter.localRotation * (Vector2.up * _moveDistance));
            }
            else
            {
                _lockColor.enabled = false;
            }
        }

        public IEnumerator OpenShutter()
        {
            yield return _shutter.DOAnchorPos(_openPosition, 0.5f).SetEase(Ease.OutElastic).WaitForCompletion();
        }

        public IEnumerator CloseShutter()
        {
            yield return _shutter.DOAnchorPos(_closePosition, 0.5f).SetEase(Ease.InElastic).WaitForCompletion();
        }
    }
}