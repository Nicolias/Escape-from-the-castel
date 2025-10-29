using System;
using TMPro;
using UnityEngine;

public class CodeBufferItem : CodeDigit
{
    private Color _color;

    public void SetState(int digit, Color color)
    {
        _color = color;

        SetDigit(digit);
    }

    protected override void SetText(TMP_Text text, int digit)
    {
        base.SetText(text, digit);

        text.color = _color;
    }
}
