using DG.Tweening;
using UnityEngine;

namespace Assets.Game.Levels.Level_1
{
    public class Cell : MonoBehaviour
    {
        [SerializeField] private int _duration;
        [SerializeField] private Slot _currentSlot;
        [SerializeField] private CellDirectionFinder _directionFinder;

        private void OnEnable()
        {
            _directionFinder.DirectionFound += OnDirectionFound;
        }

        private void OnDisable()
        {
            _directionFinder.DirectionFound -= OnDirectionFound;
        }

        private void OnDirectionFound(Direction direction)
        {
            if (_currentSlot.TryGetSlot(direction, out Slot newSlot))
            {
                _currentSlot.UnSet();
                newSlot.Set();
                _currentSlot = newSlot;
                MoveTo(_currentSlot.transform.localPosition.x, _currentSlot.transform.localPosition.y);
            }
        }

        private void MoveTo(float x, float y)
        {
            transform.DOLocalMove(new Vector3(x, y, 0), _duration);
        }
    }
}