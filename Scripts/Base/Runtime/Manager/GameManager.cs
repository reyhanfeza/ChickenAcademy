using System;
using System.Threading.Tasks;
using Base.Runtime.Misc;
using Base.Runtime.Scriptables;
using Base.Runtime.Services.Error;
using UnityEngine;

namespace Base.Runtime.Manager
{
    public class GameManager : Singleton<GameManager>
    {
        #region Serialzable Fields

        [Header("General")]
        [SerializeField] private GameSettings mGameSettings;

        #endregion
    
        private async void Start()
        {
            Application.targetFrameRate = CommonTypes.DEFAULT_FPS;

            await Task.Delay(TimeSpan.FromSeconds(GetGameSettings().ManagerDelay));
            
        }

        public GameSettings GetGameSettings()
        {
            if (mGameSettings == null)
            {
                ErrorService.DispatchError("Game Settings is null!", true);
            }

            return mGameSettings;
        }
    }
}
