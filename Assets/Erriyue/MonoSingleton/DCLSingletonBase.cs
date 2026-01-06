using System;
using UnityEditor;
using UnityEngine;

namespace RenaissanceRestart
{



    /// <summary>
    /// 双检锁单例父类
    /// </summary>
    /// <typeparam name="T">单例</typeparam>
    [DefaultExecutionOrder(DCL_SINGLETON)]
    public abstract class DCLSingletonBase<T> : MonoBehaviour, IDisposable where T : MonoBehaviour
    {
        public const int DCL_SINGLETON = -6000;
        protected static T instance;
        protected static object @lock = new object();

        /// <summary>
        /// Instance of this Singleton
        /// </summary>
        public static T I => Instance;

        /// <summary>
        /// Instance of this Singleton
        /// </summary>
        protected static T Instance
        {
            get
            {
                lock (@lock)
                {
                    if (null == instance)
                    {
                        instance = (T)FindObjectOfType(typeof(T));
                        if (FindObjectsOfType(typeof(T)).Length > 1)
                        {
                            return instance;
                        }

                        if (null == instance)
                        {
                            var singleton = new GameObject();
                            instance = singleton.AddComponent<T>();
                            singleton.name = "(singleton)" + typeof(T);
#if UNITY_EDITOR
                            if (EditorApplication.isPlaying)
                            {
                                singleton.hideFlags = HideFlags.DontSaveInEditor;
                            }
                            else
                            {
                                singleton.hideFlags = HideFlags.DontSaveInEditor | HideFlags.HideInHierarchy;
                            }
#endif
                            if (Application.isPlaying)
                                DontDestroyOnLoad(singleton);
                        }
                    }
                    return instance;
                }
            }
        }

        public static T CreateInstance() => Instance;

        public virtual void Dispose()
        {
            instance = null;
            Destroy(gameObject);
        }

        public object GetInstance()
        {
            return Instance;
        }

        private void Awake()
        {
            if (I != null && I != this)
            {
                Destroy(gameObject);
                return;
            }
            if (I == null)
            {
                instance = this as T;
            }
            OnAwake();
        }
        public abstract void OnAwake();
    }
}

