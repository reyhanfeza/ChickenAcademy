using UnityEngine;

namespace Base.Runtime.Scriptables
{
    [CreateAssetMenu(menuName = "BaseScriptable/Theme", fileName = "Theme", order = 3)]
    public class Theme : BaseScriptableObject
    {
        [Header("Materials")] 
        public Material SkyBox;
    }
}