using System;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;

[RequireComponent(typeof(RectTransform))]
public class MatrixItem : MonoBehaviour, IPointerClickHandler
{
    [SerializeField] private TMP_Text _text;

    private RectTransform _rectTransform;

    public event Action<MatrixItem> Clicked;

    public Vector2 Position { get; private set; }

    public void Init()
    {
        _rectTransform = GetComponent<RectTransform>();
        Position = _rectTransform.anchoredPosition;
    }

    public void SetText(string value) => _text.text = value;

    public void OnPointerClick(PointerEventData eventData) => Clicked?.Invoke(this);
}