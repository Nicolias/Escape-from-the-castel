using AYellowpaper.SerializedCollections;
using UnityEngine;

namespace Assets.Game.Levels.Level_1
{
    public class Slot : MonoBehaviour
    {
        [SerializedDictionary("DictionaryType", "EmptySlot"), SerializeField] private SerializedDictionary<Direction, Slot> _accesDirections = new();
        
        [field: SerializeField] public bool IsEmpty { get; private set; }

        public CellCollor CurrentCollor { get; private set; }

        public void Set(CellCollor cellCollor)
        {
            IsEmpty = false;
            CurrentCollor = cellCollor;
        }

        public void UnSet()
        {
            IsEmpty = true;
            CurrentCollor = CellCollor.None;
        }

        public bool TryGetSlot(Direction direction, out Slot slot)
        {
            if (_accesDirections.ContainsKey(direction))
            {
                slot = _accesDirections[direction];
                return true;
            }
            
            slot = null;
            return false;
        }
    }
}