using UnityEngine;
using System.Collections.Generic;

namespace VrDebugPlugin
{
    public static class VrDebug
    {
        private static List<GameObject> _debugObjects = new List<GameObject>();

        /// <summary>
        /// Adds an axis to the scene
        /// </summary>
        /// <param name="pos"></param>
        /// <param name="rot"></param>
        public static void DrawAxis(Vector3 pos, Quaternion rot) {
#if UNITY_EDITOR || VRDEBUG_PROD
            GameObject obj = SpawnAxis();
            obj.transform.position = pos;
            obj.transform.rotation = rot;

            _debugObjects.Add(obj);
#endif
        }

        /// <summary>
        /// Adds an axis to the scene
        /// </summary>
        /// <param name="tran"></param>
        /// <param name="follow">the debug axis will follow the transform's position and rotation</param>
        public static void DrawAxis(Transform tran, bool follow = true) {
#if UNITY_EDITOR || VRDEBUG_PROD
            GameObject obj = SpawnAxis();
            obj.transform.position = tran.position;
            obj.transform.rotation = tran.rotation;

            if (follow) {
                var matchTransform = obj.AddComponent<MatchTransform>();
                matchTransform.target = tran;
            }

            _debugObjects.Add(obj);
#endif
        }

        /// <summary>
        /// Adds a point to the scene
        /// </summary>
        /// <param name="pos"></param>
        /// <param name="color"></param>
        public static void DrawPoint(Vector3 pos, Color color = default(Color)) {
#if UNITY_EDITOR || VRDEBUG_PROD
            GameObject obj = StaticBehaviour.Instantiate(Resources.Load("VrDebug/Point")) as GameObject;
            obj.transform.position = pos;

            if (color != default(Color)) {
                obj.GetComponent<Renderer>().material.color = color;
            }

            _debugObjects.Add(obj);
#endif
        }

        /// <summary>
        /// Remove all current VrDebug objects from the scene
        /// </summary>
        public static void ClearDebugObjects() {
#if UNITY_EDITOR || VRDEBUG_PROD
            for (int i = 0; i < _debugObjects.Count; i++) {
                StaticBehaviour.Destroy(_debugObjects[i]);
            }

            _debugObjects.Clear();
#endif
        }

        private static GameObject SpawnAxis() {
            return StaticBehaviour.Instantiate(Resources.Load("VrDebug/Axis")) as GameObject;
        }
    }
}