using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class PlanetEditorHelper : EditorWindow {

	[MenuItem ("Window/Planet Editor Helper")]

	public static void  ShowWindow () {
		EditorWindow.GetWindow(typeof(PlanetEditorHelper));
	}

	void OnGUI () {
		// The actual window code goes here
		if (GUILayout.Button("Align Object To Planet")){
			GameObject objToRotate = Selection.activeGameObject;
			Vector3 dir = objToRotate.transform.localPosition.normalized;

//			Debug.Log (planet.transform.position);

			objToRotate.transform.rotation = Quaternion.AngleAxis (Mathf.Atan2 (dir.y, dir.x) * Mathf.Rad2Deg - 90, Vector3.forward);
			//objToRotate.transform.LookAt(planet.transform,Vector3.back);
		}
			
	}
}
