using System;
using UnityEngine;
using System.Collections;

namespace VrDebugPlugin
{
// Use to access coroutines in non-monobehaviours
    public class StaticBehaviour : MonoBehaviour
    {
        public event Action Updated;
        public event Action Destroyed;

        private static StaticBehaviour _instance;

        public static StaticBehaviour GetInstance() {
            if (!HasInstance()) {
                _instance = CreateInstance();
            }
            return _instance;
        }

        private static StaticBehaviour CreateInstance() {
            var go = new GameObject("VrDebugStaticBehaviour");
            var behaviour = go.AddComponent<StaticBehaviour>();
            return behaviour;
        }

        public static bool HasInstance() {
            return _instance != null;
        }
    }
}