using System.Collections.Generic;
using System.Linq;
using Base.Runtime.Misc;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Base.Runtime.Manager
{
    public class SoundManager : Singleton<SoundManager>
    {
        // Serialized Field
        [SerializeField] private List<Sound> mSounds;

        // Private Field
        private bool _state;
        
        protected override void Awake()
        {
            foreach (Sound sound in mSounds)
            {
                AudioSource source = gameObject.AddComponent<AudioSource>();
                
                if(sound.Clips.Length == 0)
                    continue;
                
                AudioClip audioClip = sound.Clips[Random.Range(0, sound.Clips.Length)];
                
                source.clip = audioClip;
                source.pitch = sound.Pitch;
                source.volume = sound.Volume;
                source.loop = sound.IsLoop;

                sound.Source = source;  
            }

            _state = PlayerPrefs.GetInt(CommonTypes.SOUND_STATE_KEY, 1) != 0;

            base.Awake();
        }

        public void Play(string tag)
        {
            Sound targetSound = mSounds.SingleOrDefault(x => x.Tag == tag);
            AudioClip targetClip = null;
            
            if (targetSound == null)
                return;

            if(targetSound.Clips.Length == 0)
                return;
            
            targetClip = targetSound.Clips[Random.Range(0, targetSound.Clips.Length)];

            if (targetClip == null)
            {
                return;
            }
            
            targetSound.Source.PlayOneShot(targetClip);
        }
        
        public void ChangeState(bool state)
        {
            this._state = state;
            AudioListener.volume = _state ? 1 : 0;
            PlayerPrefs.SetInt(CommonTypes.SOUND_STATE_KEY, _state ? 1 : 0);
        }
        public bool GetState()
        {
            return _state;
        }
    }
}


