using DG.Tweening;
using System;
using UnityEngine;

namespace Asset.GameScene
{
    public class LevelEnder : MonoBehaviour
    {
        [SerializeField] private Animator _safeBoxAnimator;
        [SerializeField] private float _closeDoorDuration;

        [SerializeField] private GameObject _canvas;

        public event Action Ended;

        public void EndGame()
        {
            Sequence sequence = DOTween.Sequence();

            sequence
                .AppendCallback(() => _safeBoxAnimator.SetTrigger(Consts.CloseSafe))
                .AppendInterval(_closeDoorDuration)
                .AppendCallback(() => Ended?.Invoke());
            sequence.Play();
        }
    }
}