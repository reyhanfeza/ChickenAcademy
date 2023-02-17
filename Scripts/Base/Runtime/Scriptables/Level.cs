using UnityEngine;

namespace Base.Runtime.Scriptables
{
    [CreateAssetMenu(menuName = "BaseScriptable/Level", fileName = "Level", order = 1)]
    public class Level : BaseScriptableObject
    {
        [Header("General")]
        public int Id;
        public string SceneName;
        public Theme Theme;

        [Header("Misc")]
        public int EndGameBonusCurrency;
    }
}