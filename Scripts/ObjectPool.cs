using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace VrDebugPlugin
{
    public class ObjectPool
    {
        public HideMethod hideMethod = HideMethod.Disable;

        private Transform _unusedParent;
        private string _prefabLocation;
        private Queue<GameObject> _unused = new Queue<GameObject>();

        [Header("Debug")]
        private int _instancesCreated;
        private int _inPool;
        private int _outOfPool;

        public ObjectPool(string prefabLocation, Transform parent, int initialSize = 0) {
            _prefabLocation = prefabLocation;
            _unusedParent = parent;
            Populate(initialSize);
        }

        public GameObject Get() {
            _outOfPool++;
            _inPool--;

            if (_unused.Count == 0) {
                AddToPool();
            }
            var go = _unused.Dequeue();
            Show(go);
            return go;
        }

        public void Return(GameObject obj) {
            Hide(obj);
            if (obj.transform.parent != _unusedParent) {
                obj.transform.SetParent(_unusedParent);
            }
            _unused.Enqueue(obj);

            _inPool++;
            _outOfPool--;
        }

        private void Populate(int size) {
            for (int i = 0; i < size; i ++) {
                AddToPool();
            }
        }

        private void AddToPool() {
            GameObject go = StaticBehaviour.Instantiate(Resources.Load(_prefabLocation), _unusedParent) as GameObject;
            Hide(go);
            _unused.Enqueue(go);

            _instancesCreated++;
        }

        private void Hide(GameObject go) {
            if (hideMethod == HideMethod.Disable) {
                go.SetActive(false);
            } else {
                go.transform.position = Vector3.one * 1000f;
            }
        }

        private void Show(GameObject go) {
            if (hideMethod == HideMethod.Disable) {
                go.SetActive(true);
            }
        }
    }

    public enum HideMethod
    {
        Disable,
        Distance
    }
}