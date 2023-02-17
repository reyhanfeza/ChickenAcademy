using UnityEngine;

namespace Base.Runtime.Misc
{
    public abstract class Singleton<T> : MonoBehaviour where T : UnityEngine.Component
    {
        #region Serializable Fields
    
        [SerializeField] private bool mIsPersistant;
    
        #endregion
        #region Private Fields
        
        private static T instance;
    
        #endregion
        #region Properties

        public static T Instance
        {
            get
            {
                if ( instance == null )
                {
                    instance = FindObjectOfType<T> ();
                    if ( instance == null )
                    {
                        GameObject obj = new GameObject ();
                        obj.name = typeof ( T ).Name;
                        instance = obj.AddComponent<T> ();
                    }
                }
                return instance;
            }
        }
    
        #endregion
        
        protected virtual void Awake ()
        {
            if ( instance == null )
            {
                instance = this as T;
                
                if(mIsPersistant)
                    DontDestroyOnLoad ( gameObject );
            }
            else
            {
                Destroy ( gameObject );
            }
        }

        protected virtual void OnDestroy() { }
    }
}
