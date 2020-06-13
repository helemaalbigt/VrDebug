using UnityEngine;
using System.Collections.Generic;

namespace VrDebugPlugin
{
    public class VrDebug : MonoBehaviour
    {
        private static Transform _debugObjectsParent;
        private static ObjectPool _axisPool;
        private static ObjectPool _pointPool;
        private static ObjectPool _linePool;

        private static Dictionary<GameObject, ObjectPool> _activeObjects = new Dictionary<GameObject, ObjectPool>();
        private Dictionary<GameObject, ObjectPool> _markedForRemoval = new Dictionary<GameObject, ObjectPool>();

        private static VrDebug _instance;

        private static void CheckToInit() {
            if (GameObject.Find("VrDebug") == null) {
                var go = new GameObject("VrDebug");
                _debugObjectsParent = go.transform;
                _instance = go.AddComponent<VrDebug>();

                _axisPool = new ObjectPool("VrDebug/Axis", _debugObjectsParent, 3);
                _pointPool = new ObjectPool("VrDebug/Point", _debugObjectsParent, 3);
                _linePool = new ObjectPool("VrDebug/Line", _debugObjectsParent, 3);
            }
        }

        //hacky solution to have the objects draw for one frame only
        void Update() {
            foreach (var obj in _markedForRemoval) {
                obj.Value.Return(obj.Key);
                _activeObjects.Remove(obj.Key);
            }

            _markedForRemoval.Clear();

            foreach (var obj in _activeObjects) {
                if (obj.Key.activeInHierarchy) {
                    _markedForRemoval.Add(obj.Key, obj.Value);
                }
            }
        }

        /// <summary>
        /// Adds an axis to the scene
        /// </summary>
        /// <param name="pos"></param>
        /// <param name="rot"></param>
        public static void DrawAxis(Vector3 pos, Quaternion rot, float scale = 0.02f) {
#if UNITY_EDITOR || VRDEBUG_PROD
            CheckToInit();

            GameObject obj = _axisPool.Get();
            obj.transform.position = pos;
            obj.transform.rotation = rot;
            obj.transform.localScale = Vector3.one * scale;

            _activeObjects.Add(obj, _axisPool);
#endif
        }

        /// <summary>
        /// Adds an axis to the scene
        /// </summary>
        /// <param name="tran"></param>
        /// <param name="follow">the debug axis will follow the transform's position and rotation</param>
        public static void DrawAxis(Transform tran, float scale = 0.02f) {
#if UNITY_EDITOR || VRDEBUG_PROD
            CheckToInit();

            GameObject obj = _axisPool.Get();
            obj.transform.position = tran.position;
            obj.transform.rotation = tran.rotation;
            obj.transform.localScale = Vector3.one * scale;

            _activeObjects.Add(obj, _axisPool);
#endif
        }

        /// <summary>
        /// Adds a point to the scene
        /// </summary>
        /// <param name="pos"></param>
        /// <param name="color"></param>
        public static void DrawPoint(Vector3 pos, Color color = default(Color), float scale = 0.005f) {
#if UNITY_EDITOR || VRDEBUG_PROD
            CheckToInit();

            GameObject obj = _pointPool.Get();
            obj.transform.position = pos;
            obj.transform.localScale = scale * Vector3.one;

            if (color != default(Color)) {
                obj.GetComponent<Renderer>().material.color = color;
            }

            _activeObjects.Add(obj, _pointPool);
#endif
        }

        public static void DrawLine(Vector3 start, Vector3 end, Color color = default(Color), float thickness = 0.008f) {
#if UNITY_EDITOR || VRDEBUG_PROD
            CheckToInit();

            GameObject obj = _linePool.Get();
            obj.transform.position = start;
            obj.transform.forward = end - start;
            obj.transform.localScale = new Vector3(thickness, thickness, Vector3.Distance(start,end));

            if (color != default(Color)) {
                obj.GetComponentInChildren<Renderer>().material.color = color;
            }

            _activeObjects.Add(obj, _linePool);
#endif
        }
    }
}