using Scripts.MiniGames;
using UnityEngine;

public class MiniGamesEntryPoint : MonoBehaviour
{
    [SerializeField] private ThimblesGame _game;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.F))
        {
            _game.StartGame();
        }
    }
}