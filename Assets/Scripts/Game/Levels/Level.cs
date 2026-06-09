using System;
using UnityEngine;

public abstract class Level : MonoBehaviour
{
    public abstract event Action Complete;

    public abstract void Init();

    public void Open()
    {
        gameObject.SetActive(true);
    }

    public void Close()
    {
        gameObject.SetActive(false);
    }
}
