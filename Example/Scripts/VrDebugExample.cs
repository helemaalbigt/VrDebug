using UnityEngine;
using VrDebugPlugin;

public class VrDebugExample : MonoBehaviour
{
    public Transform referenceTransform;
	
	void Start () {
        VrDebug.DrawPoint(Vector3.zero);
        VrDebug.DrawAxis(referenceTransform);
    }
}
