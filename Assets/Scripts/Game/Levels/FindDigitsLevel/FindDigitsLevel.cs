using System;
using UnityEngine;

public class FindDigitsLevel : Level
{
    [SerializeField] private FindDigitsGame _game;

    public override event Action Complet;

    public override void Init()
    {
        _game.Init();
        _game.Won += GameWon;
    }

    private void GameWon()
    {
        Complet?.Invoke();
    }

    private void OnDisable()
    {
        _game.Won -= GameWon;
    }
}

