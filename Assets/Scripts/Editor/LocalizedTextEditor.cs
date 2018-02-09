using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using UnityEngine.UI;

[CustomEditor(typeof(LocalizedText), true)]
[CanEditMultipleObjects]
public class LocalizedTextEditor : UnityEditor.UI.TextEditor
{
	private SerializedProperty stringID;
	private SerializedProperty preText;
	private SerializedProperty postText;

	public override void OnInspectorGUI()
	{
		preText = serializedObject.FindProperty("preText");
		postText = serializedObject.FindProperty("postText");
		stringID = serializedObject.FindProperty("stringID");
		EditorGUILayout.PropertyField(preText, true);
		EditorGUILayout.PropertyField(postText, true);
		EditorGUILayout.PropertyField(stringID, true);
		base.OnInspectorGUI();
	}
}
