using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

[RequireComponent(typeof(Image))]
public class ColorItem : MonoBehaviour, IPointerClickHandler
{
    [field:SerializeField] public Color BaseColor { get; private set; }

    private Image _image;

    public event Action<ColorItem> Clicked;

    public Color Color { get; private set; }

    public void Init(Color color)
    {
        _image = GetComponent<Image>();
        Color = color;
    }

    public void SetTargetColor() => _image.color = Color;

    public void ReturnToBaseColor() => _image.color = BaseColor;

    public void OnPointerClick(PointerEventData eventData) => Clicked?.Invoke(this);
}
