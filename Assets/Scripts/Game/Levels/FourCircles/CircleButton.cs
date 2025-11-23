using System;
using UnityEngine;
using UnityEngine.UI;

namespace Scripts.Levels.FourCircles
{
    [RequireComponent(typeof(Button))]
    public class CircleButton : MonoBehaviour
    {
        [SerializeField] private Circle _circle;

        private Button _button;

        public event Action<Circle> Clicked;

        public void Init()
        {
            _button = GetComponent<Button>();
            _button.onClick.AddListener(() => Clicked?.Invoke(_circle));
            _circle.Init();
        }

        private void OnDisable()
        {
            _button.onClick.RemoveAllListeners();
        }
    }
}
