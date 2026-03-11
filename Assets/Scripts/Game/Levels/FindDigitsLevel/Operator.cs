using UnityEngine;
using UnityEngine.UI;

namespace Assets.Game.Levels.FindDigitsLevel
{
    [RequireComponent(typeof(Image))]
    public class Operator : MonoBehaviour
    {
        [SerializeField] private Sprite _largerSprite;
        [SerializeField] private Sprite _lessSprite;

        private Image _image;

        public void SetOperator(Operators operators)
        {
            _image ??= GetComponent<Image>();

            if (operators == Operators.Larger)
            {
                _image.sprite = _largerSprite;
            }
            else if (operators == Operators.Less)
            {
                _image.sprite = _lessSprite;
            }
        }
    }
}