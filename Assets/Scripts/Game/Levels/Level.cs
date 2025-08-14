using System;
using UnityEngine;

public abstract class Level : MonoBehaviour
{
    public abstract event Action Complet;

    public abstract void Init();
}
