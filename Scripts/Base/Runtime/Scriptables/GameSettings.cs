using System.Collections.Generic;
using Base.Runtime.Misc;
using UnityEngine;

namespace Base.Runtime.Scriptables
{
    [CreateAssetMenu(menuName = "BaseScriptable/GameSettings", fileName = "GameSettings", order = 0)]
    public class GameSettings : BaseScriptableObject
    {
        [Header("General")] 
        public float ManagerDelay;
        
        [Header("Datas")]
        [ContextMenuItem("Update","FindLevels")]
        public Level[] Levels;

        
        #if UNITY_EDITOR
        
        public void FindLevels()
        {
            Levels = null;

            List<Level> foundLevels = new List<Level>();
            Object[] objects = Resources.LoadAll(CommonTypes.EDITOR_LEVELS_PATH);

            foreach (Object targetObject in objects)
            {
                if(targetObject is not Level)
                    continue;
                
                foundLevels.Add(targetObject as Level);
            }

            Levels = foundLevels.ToArray();
        }
        
        #endif
   
    } 
}