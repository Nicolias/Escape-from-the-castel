using System;
using UnityEngine;

public class FindColorsLevel : Level
{
    [SerializeField] private FindColorsGame _game;

    public override event Action Complet;

    public override void Init()
    {
        _game.Init();

        _game.Won += OnGameWon;
    }

    private void OnDisable()
    {
        _game.Won -= OnGameWon;
    }

    private void OnGameWon()
    {
        Complet?.Invoke();
    }
}
