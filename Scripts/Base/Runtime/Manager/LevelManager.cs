using System;
using Base.Runtime.Misc;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Base.Runtime.Manager
{
    public class LevelManager : Singleton<LevelManager>
    {
        private void Start()
        {
            Application.targetFrameRate = CommonTypes.DEFAULT_FPS;
        }

        public void RestartLevel()
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }
        
    }
}
