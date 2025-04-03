using Assets.Scripts.MiniGames.Factories;
using Cysharp.Threading.Tasks;
using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Scripts.MiniGames
{
    public class ThimblesGame : MonoBehaviour
    {
        [SerializeField] private CupFactory _cupFactory;
        [SerializeField] private Transform _ball;
        [SerializeField] private Table _table;
        [SerializeField] private int _cupsCount;

        private List<Cup> _cups = new List<Cup>();
        private Shuffler _shuffler = new Shuffler();
        private Cup _winnerCup;

        public event Action IsWinn;

        public event Action IsLoose;

        private void Start()
        {
            InitCups();
        }

        private void OnDisable()
        {
            foreach (Cup cup in _cups)
            {
                cup.Interacted -= OnCupInteracted;
            }
        }

        public async UniTaskVoid StartGame()
        {
            int randomCupIndex = UnityEngine.Random.Range(0, _cupsCount);
            Transform ball = Instantiate(_ball);
            Cup randomCup = _winnerCup = _cups[randomCupIndex];

            await randomCup.InsertItem(ball);

            await _shuffler.Blend(_cups.Select(cup => cup.Transform).ToList(), randomCupIndex);
        }

        private void InitCups()
        {
            foreach (Vector3 position in _table.GeneratePoints(_cupsCount))
            {
                Cup cup = _cupFactory.Create(position);
                _cups.Add(cup);
                cup.Interacted += OnCupInteracted;
            }
        }

        private async UniTaskVoid InteractCup(Cup cup)
        {
            await cup.ShowInside();

            if (cup == _winnerCup)
            {
                IsWinn?.Invoke();
            }
            else
            {
                IsLoose?.Invoke();
            }
        }

        private void OnCupInteracted(Cup cup)
        {
            InteractCup(cup).Forget();
        }
    }
}