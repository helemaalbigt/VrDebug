# VrDebug

VrDebug is a tiny library to help you debug positions in VR by spawning simple prefabs in the scene.

## Usage

Add `using.VrDebugPlugin` to your script, then add one of the following anywhere:
`VrDebug.DrawPoint(Vector3 pos)` Add a point to the scene at this position.
`VrDebug.DrawAxis(Vector3 pos, Quaternion rot)` Add an axis to the scene at this position and rotation.
`VrDebug.DrawAxis(Transform transform)` Add an axis to the scene which will follow the referenced transform.

By default, these will only be added to your scene in the editor. Add `VRDEBUG_PROD` to your script defines to display the debug prefabs in a build as well.
