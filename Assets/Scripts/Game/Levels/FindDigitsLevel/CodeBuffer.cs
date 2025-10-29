using System;
using UnityEngine;
using static UnityEditor.Progress;

public class CodeBuffer : DigitRaw<CodeBufferItem>
{
    public override void Init()
    {
        base.Init();

        gameObject.SetActive(true);
    }

    public override void ResetState()
    {
        base.ResetState();

        gameObject.SetActive(false);
    }

    public void SetDigit(int digit, Color color)
    {
        if (ItemsCount == 0)
        {
            throw new InvalidOperationException();
        }

        GetItem().SetState(digit, color);
    }
}
