///////////////////////////////////////////////////////////////////////////////
// Copyright (C) 2015 AsNet Co., Ltd.
// All Rights Reserved. These instructions, statements, computer
// programs, and/or related material (collectively, the "Source")
// contain unpublished information proprietary to AsNet Co., Ltd
// which is protected by US federal copyright law and by
// international treaties. This Source may NOT be disclosed to
// third parties, or be copied or duplicated, in whole or in
// part, without the written consent of AsNet Co., Ltd.
///////////////////////////////////////////////////////////////////////////////

using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(CustomAction)), CanEditMultipleObjects]
public class CustomActionEditor : Editor
{
	private static GUIContent addButtonContent = new GUIContent("+", "add");
	private static GUIContent moveUpButtonContent = new GUIContent("^", "move up");
	private static GUIContent moveDownButtonContent = new GUIContent("v", "move down");
	private static GUIContent duplicateButtonContent = new GUIContent("+", "duplicate");
	private static GUIContent deleteButtonContent = new GUIContent("-", "delete");
	
//	private static GUILayoutOption miniButtonWidth = GUILayout.Width(20f);

	public override void OnInspectorGUI()
	{
		serializedObject.Update();

		SerializedProperty actions = serializedObject.FindProperty("actions");

		if (!actions.isArray)
		{
			EditorGUILayout.HelpBox(actions.name + " is neither an array nor a list!", MessageType.Error);
			return;
		}

		SerializedProperty size = actions.FindPropertyRelative("Array.size");

		if (size.hasMultipleDifferentValues)
		{
			EditorGUILayout.HelpBox("Not showing actions with different sizes.", MessageType.Info);
			return;
		}

		EditorGUILayout.PropertyField(actions);

		if (actions.isExpanded)
		{
			EditorGUI.indentLevel++;

//			EditorGUILayout.PropertyField(actions.FindPropertyRelative("Array.size"));

			if (actions.arraySize == 0)
			{
				if (GUILayout.Button(addButtonContent))
				{
					actions.arraySize++;
				}
			}
			else
			{
				for (int i = 0; i < actions.arraySize; i++)
				{
					EditorGUILayout.PropertyField(actions.GetArrayElementAtIndex(i));

					GUILayout.BeginHorizontal();

//					GUILayout.Button(moveUpButtonContent, EditorStyles.miniButtonLeft, miniButtonWidth);
//					GUILayout.Button(moveDownButtonContent, EditorStyles.miniButtonMid, miniButtonWidth);
//					GUILayout.Button(addButtonContent, EditorStyles.miniButtonMid, miniButtonWidth);
//					GUILayout.Button(deleteButtonContent, EditorStyles.miniButtonRight, miniButtonWidth);

//					if (i > 0)
					{
						GUI.enabled = (i > 0);
						if (GUILayout.Button(moveUpButtonContent))
						{
							actions.MoveArrayElement(i, i - 1);
						}
						GUI.enabled = true;
					}

//					if (i < actions.arraySize - 1)
					{
						GUI.enabled = (i < actions.arraySize - 1);
						if (GUILayout.Button(moveDownButtonContent))
						{
							actions.MoveArrayElement(i, i + 1);
						}
						GUI.enabled = true;
					}

					if (GUILayout.Button(duplicateButtonContent))
					{
						actions.InsertArrayElementAtIndex(i);
					}

					if (GUILayout.Button(deleteButtonContent))
					{
						int oldSize = actions.arraySize;

						actions.DeleteArrayElementAtIndex(i);

						if (oldSize == actions.arraySize)
						{
							actions.DeleteArrayElementAtIndex(i);
						}
					}

					GUILayout.EndHorizontal();

					GUILayout.Space(5);
				}
			}

			EditorGUI.indentLevel--;
		}
		
		EditorGUILayout.PropertyField(serializedObject.FindProperty("delay"));
		EditorGUILayout.PropertyField(serializedObject.FindProperty("repeat"));

		if (GUI.changed)
		{
			serializedObject.ApplyModifiedProperties();
		}
	}
}
