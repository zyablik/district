using UnityEngine;
using System.Collections;
using UnityEditor;

[CustomEditor(typeof(DaytimeController))]
public class DaytimeControllerEditor : Editor {
	public override void OnInspectorGUI() {
		base.OnInspectorGUI();
        ((DaytimeController)target).applyLight();

//		myTarget.daytime = EditorGUILayout.ObjectField("Experience", myTarget.experience);

//		EditorGUILayout.LabelField("Level", myTarget.Level.ToString());
//		RenderSettings.ambientLight = Color.white;
	}
}