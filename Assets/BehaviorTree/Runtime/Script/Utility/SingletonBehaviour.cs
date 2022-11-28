using System;
using UnityEngine;

namespace Pumpkin.Utility
{
    public class SingletonBehaviour<T> : MonoBehaviour where T : SingletonBehaviour<T>
    {
        public static T Instance { get; protected set; }

        void Awake()
        {
            if (Instance != null && Instance != this)
            {
                Destroy(this);
                throw new System.Exception("An Instance of this singleton already exists.");
            }
            else
            {
                Instance = (T)this;
            }
        }
    }
}
