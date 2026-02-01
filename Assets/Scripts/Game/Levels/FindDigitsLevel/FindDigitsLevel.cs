using System;
using UnityEngine;

public class FindDigitsLevel : Level
{
    [SerializeField] private FindDigitsGame _game;

    public override event Action Complete;

    public override void Init()
    {
        _game.Init();
        _game.Won += GameWon;
    }

    private void GameWon()
    {
        Complete?.Invoke();
    }

    private void OnDisable()
    {
        _game.Won -= GameWon;
    }
}

