using System.Threading;
using UnityEngine;

namespace GSL
{

    // This object wont be destroyed accross the scenes. Lifetime is same as App's
    public class SingletonDontDestroy<T> : MonoBehaviour where T : MonoBehaviour
    {
        private static T _instance;
        private static bool _isDestroyed;

        public static T instance
        {
            get
            {
                if (_isDestroyed)
                {
                    //Debug.LogError("<color=#ff0000ff>[SingletonDontDestroy] Instance '" + typeof(T) +
                    //"' cannot be DESTROYED!. Something is very wrong.</color>");
                    return _instance;
                }
                if (_instance == null)
                {
                    _instance = (T)FindObjectOfType(typeof(T));
                    if (FindObjectsOfType(typeof(T)).Length > 1)
                    {
                        Debug.LogError("[Singleton] Something went really wrong " +
                                " - there should never be more then 1 singletion!" +
                                " Reopening the scene might fix it. Thread: " + Thread.CurrentThread.Name);
                        return _instance;
                    }

                    if (_instance == null)
                    {
                        GameObject singleton = new GameObject();
                        _instance = singleton.AddComponent<T>();
                        singleton.name = "(singleton) " + typeof(T).ToString();
                        Debug.Log("[Singleton] An instance of " + typeof(T) +
                            " is needed in the scene, so '" + singleton +
                                    ". Thread: " + Thread.CurrentThread.ManagedThreadId);

                        DontDestroyOnLoad(singleton);

                    }
                    else
                    {
                        Debug.Log("[Singleton] Using instance already created: " + _instance.gameObject.name);
                    }
                }
                return _instance;
            }
        }

        private void OnDestroy()
        {
            _isDestroyed = true;
        }
    }
}
