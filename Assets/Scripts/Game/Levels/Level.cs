using System;
using UnityEngine;

public abstract class Level : MonoBehaviour
{
    public abstract event Action Complete;

    public abstract void Init();
}
