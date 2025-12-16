using System;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts.DotsLevel
{
    public class DotsLockLevel : Level
    {
        [SerializeField] private List<Light> _lights;
        [SerializeField] private RectTransform _container;
        [SerializeField] private List<Light> _targetLights;

        public override event Action Complet;

        public override void Init()
        {
            foreach (Light light in _targetLights)
            {
                light.Init();
            }

            foreach (Light light in _lights)
            {
                light.Init();
            }
        }

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.W))
            {
                _container.rotation *= Quaternion.Euler(0f, 0f, 45f);
                CheckWin();
            }
        }

        private void CheckWin()
        {
            foreach (Light light in _lights)
            {
                int index = GetLightIndex(light);
                bool unique = _targetLights[index].Color == light.Color;

                if (unique == false)
                {
                    Debug.Log(false);
                    return;
                }
            }

            Debug.Log(true);
        }

        private int GetLightIndex(Light light)
        {
            float rotation = Quaternion.FromToRotation(_container.rotation * light.AnchoredPosition, Vector3.up).eulerAngles.z;

            return Mathf.RoundToInt(rotation / 45f);
        }
    }
}