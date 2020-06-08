using UnityEngine;

namespace VrDebugPlugin
{
    public class MatchTransform : MonoBehaviour
    {
        public Transform target;

        void Update() {
            if (target != null) {
                transform.position = target.position;
                transform.rotation = target.rotation;
            }
        }
    }
}

