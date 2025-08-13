using Asset.Servise;
using NaughtyAttributes;
using UnityEngine;
using YG;

namespace Asset.GameScene
{
    public class EntryPoint : MonoBehaviour
    {
        [SerializeField, Scene] private string _currentSceneName;

        public void Awake()
        {
            YG2.saves.CurrentLevelName = _currentSceneName;
            YG2.SaveProgress();
        }
    }
}