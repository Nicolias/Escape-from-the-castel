using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Image))]
public class Operator : MonoBehaviour
{
    [SerializeField] private Sprite _largerSprite;
    [SerializeField] private Sprite _lessSprite;

    private Image _image;

    public Operators CurrentOperator;

    public void SetOperator(Operators operators)
    {
        _image ??= GetComponent<Image>();
        CurrentOperator = operators;

        if ((Operators)operators == Operators.Larger)
        {
            _image.sprite = _largerSprite;
        }
        else if ((Operators)operators == Operators.Less)
        {
            _image.sprite = _lessSprite;
        }
    }
}