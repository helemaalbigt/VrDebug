# VrDebug

VrDebug is a tiny library to help you debug positions, axis and lines in VR.
In code it works similar to Unity's Debug.DrawLine(), except here it's visible in game view (and in VR).

![vrdebug line example](https://i.imgur.com/49zmTSY.gif)
![vrdebug axis example](https://i.imgur.com/z3Kyc1e.gif)

## Setup

Import the Unitypackage in the latest release, or clone this repo into the Assets folder of your Unity project.

## Usage

Add `using VrDebugPlugin` to your script, then add one of the following anywhere:
* `VrDebug.DrawPoint(Vector3 pos)` Add a point to the scene at this position.
* `VrDebug.DrawAxis(Vector3 pos, Quaternion rot)` Add an axis to the scene at this position and rotation.
* `VrDebug.DrawAxis(Transform transform)` Add an axis to the scene matching the referenced transform.
* `VrDebug.DrawLine(Vector3 startPos, Vector3 endPos)` Add a line.

By default, these will only be added to your scene in the editor. Add `VRDEBUG_PROD` to your script defines to display the debug prefabs in a build as well.
