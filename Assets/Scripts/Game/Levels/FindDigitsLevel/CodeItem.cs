using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class CodeItem : CodeDigit
{
    [SerializeField] private Image _selectedMark;

    public void Select() => _selectedMark.enabled = true;

    public override void ResetState()
    {
        base.ResetState();

        _selectedMark.enabled = false;
    }

    public void SetState(int digit)
    {
        SetDigit(digit);

        _selectedMark.enabled = false;
    }
}
