using Asset.Servise;
using IJunior.TypedScenes;
using UnityEngine;
using YG;

namespace Asset.GameScene
{
    public class EntryPoint : MonoBehaviour, ISceneLoadHandler<GameData>
    {
        public void OnSceneLoaded(GameData gameData)
        {
            YG2.SaveProgress();
        }
    }
}