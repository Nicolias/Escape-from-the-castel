using System;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts.DotsLevel
{
    public class DotsLockLevel : Level
    {
        [SerializeField] private List<Light> _lights;
        [SerializeField] private List<RectTransform> _locks;

        public override event Action Complet;

        public override void Init()
        {
        }
    }

    public class CycleContainer
    {
        private List<RectTransform> _items;

        public CycleContainer(List<RectTransform> items)
        {
            _items = items;
        }

        public void SwitchItems(int index)
        {

        }
    }

    [RequireComponent(typeof(RectTransform))]
    public class TransformableItem : MonoBehaviour
    {
        public Vector2 Position;
    }
}