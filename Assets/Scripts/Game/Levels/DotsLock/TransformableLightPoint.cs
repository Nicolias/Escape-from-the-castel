using DG.Tweening;
using UnityEngine;

namespace Assets.Scripts.DotsLevel
{
    [RequireComponent(typeof(RectTransform))]
    public class TransformableLightPoint : CirclePoint
    {
        public YieldInstruction Move(Vector2 anchoredPosition) => RectTransform.DOAnchorPos(anchoredPosition, 0.5f).WaitForCompletion();
    }
} 