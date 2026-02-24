using System;
using UnityEngine;
using UnityEngine.EventSystems;

public class NumpadItem : MonoBehaviour, IPointerClickHandler
{
    [field: SerializeField] public int Digit { get; private set; }

    public event Action<NumpadItem> Clicked;

    public void OnPointerClick(PointerEventData eventData) => Clicked?.Invoke(this);
}
