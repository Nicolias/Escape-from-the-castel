using DG.Tweening;
using UnityEngine;

namespace Assets.Scripts.DotsLevel
{
    [RequireComponent(typeof(RectTransform))]
    public class TransformableLightPoint : CirclePoint
    {
        private float _animationDuration = 0.5f;

        public YieldInstruction Move(Vector2 anchoredPosition) => RectTransform.DOAnchorPos(anchoredPosition, _animationDuration).WaitForCompletion();
    }
} 