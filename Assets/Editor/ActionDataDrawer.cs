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

[CustomPropertyDrawer(typeof(ActionData))]
public class ActionDataDrawer : PropertyDrawer
{
	private static readonly GUIContent endContent 	   	= new GUIContent("End");
	private static readonly GUIContent positionContent 	= new GUIContent("Position");
	private static readonly GUIContent scaleContent    	= new GUIContent("Scale");
	private static readonly GUIContent rotationContent 	= new GUIContent("Rotation");
	private static readonly GUIContent aContent 	   	= new GUIContent("A");
	private static readonly GUIContent rgbContent 	   	= new GUIContent("RGB");
	private static readonly GUIContent relativeContent 	= new GUIContent("Relative");
	private static readonly GUIContent localContent    	= new GUIContent("Local");
	private static readonly GUIContent recursiveContent	= new GUIContent("Recursive");
	private static readonly GUIContent triggerContent  	= new GUIContent("Trigger");
	private static readonly GUIContent delayContent    	= new GUIContent("Delay");
	private static readonly GUIContent varianceContent 	= new GUIContent("Variance");
	private static readonly GUIContent eventContent 	= new GUIContent("Event");
	private static readonly GUIContent withChildrenContent   = new GUIContent("With Children");
	private static readonly GUIContent preserveAspectContent = new GUIContent("Preserve Aspect");

	private static readonly float lineHeight = 18.0f;

	private static readonly int[] lines =
	{
		8,	// Move
		8,	// HorizontalMove
		8,	// VerticalMove
		7,	// Scale
		7,	// Rotate
		8,	// Fade
		8,	// Tint
		5,	// SetPosition
		4,	// SetScale
		3,	// SetRotation
		4,	// SetAlpha
		4,	// SetRGB
		3,	// Delay
		2,	// AnimatorTrigger
		4,	// PlaySound
		2 + 4,	// CallEvent
		2,	// PlayParticleSystem
	};

	public override void OnGUI(Rect position, SerializedProperty action, GUIContent label)
	{
//		int oldIndentLevel = EditorGUI.indentLevel;

		label = EditorGUI.BeginProperty(position, new GUIContent(string.Format("Action {0}", label.text.Substring(7).ToInt() + 1)), action);
		EditorGUI.PrefixLabel(position, label);

		Rect contentPosition = position;
		contentPosition.height = lineHeight;

		EditorGUI.indentLevel++;

		SerializedProperty actionType = action.FindPropertyRelative("actionType");

		contentPosition.y += lineHeight;
		EditorGUI.PropertyField(contentPosition, actionType);
		
		switch ((ActionType)actionType.enumValueIndex)
		{
			case ActionType.Move:
			{
				ActionField(action, contentPosition, "v31", true, localContent);
				break;
			}
			
			case ActionType.HorizontalMove:
			{
				ActionField(action, contentPosition, "f1", false, localContent);
				break;
			}
				
			case ActionType.VerticalMove:
			{
				ActionField(action, contentPosition, "f1", false, localContent);
				break;
			}

			case ActionType.Scale:
			{
				ActionField(action, contentPosition, "v31", true);
				break;
			}
			
			case ActionType.Rotate:
			{
				ActionField(action, contentPosition, "f1");
				break;
			}
			
			case ActionType.Fade:
			{
				ActionField(action, contentPosition, "f1", false, recursiveContent);
				break;
			}
			
			case ActionType.Tint:
			{
				ActionField(action, contentPosition, "c1", false, recursiveContent);
				break;
			}
			
			case ActionType.SetPosition:
			{
				contentPosition.y += lineHeight;
				EditorGUI.PropertyField(contentPosition, action.FindPropertyRelative("v31"), positionContent);
				contentPosition.y += lineHeight;
				EditorGUI.PropertyField(contentPosition, action.FindPropertyRelative("v32"), varianceContent);
				contentPosition.y += lineHeight;
				EditorGUI.PropertyField(contentPosition, action.FindPropertyRelative("b1"), relativeContent);
				contentPosition.y += lineHeight;
				EditorGUI.PropertyField(contentPosition, action.FindPropertyRelative("b2"), localContent);
				break;
			}
			
			case ActionType.SetScale:
			{
				contentPosition.y += lineHeight;
				EditorGUI.PropertyField(contentPosition, action.FindPropertyRelative("v31"), scaleContent);
				contentPosition.y += lineHeight;
				EditorGUI.PropertyField(contentPosition, action.FindPropertyRelative("v32"), varianceContent);
				contentPosition.y += lineHeight;
				EditorGUI.PropertyField(contentPosition, action.FindPropertyRelative("b1"), preserveAspectContent);
				break;
			}
			
			case ActionType.SetRotation:
			{
				contentPosition.y += lineHeight;
				EditorGUI.PropertyField(contentPosition, action.FindPropertyRelative("f1"), rotationContent);
				contentPosition.y += lineHeight;
				EditorGUI.PropertyField(contentPosition, action.FindPropertyRelative("f2"), varianceContent);
				break;
			}
			
			case ActionType.SetAlpha:
			{
				contentPosition.y += lineHeight;
				EditorGUI.PropertyField(contentPosition, action.FindPropertyRelative("f1"), aContent);
				contentPosition.y += lineHeight;
				EditorGUI.PropertyField(contentPosition, action.FindPropertyRelative("f2"), varianceContent);
				contentPosition.y += lineHeight;
				EditorGUI.PropertyField(contentPosition, action.FindPropertyRelative("b1"), recursiveContent);
				break;
			}
			
			case ActionType.SetRGB:
			{
				contentPosition.y += lineHeight;
				EditorGUI.PropertyField(contentPosition, action.FindPropertyRelative("c1"), rgbContent);
				contentPosition.y += lineHeight;
				EditorGUI.PropertyField(contentPosition, action.FindPropertyRelative("c2"), varianceContent);
				contentPosition.y += lineHeight;
				EditorGUI.PropertyField(contentPosition, action.FindPropertyRelative("b1"), recursiveContent);
				break;
			}

			case ActionType.Delay:
			{
				contentPosition.y += lineHeight;
				EditorGUI.PropertyField(contentPosition, action.FindPropertyRelative("duration"));
				contentPosition.y += lineHeight;
				EditorGUI.PropertyField(contentPosition, action.FindPropertyRelative("f2"), varianceContent);
				break;
			}
			
			case ActionType.AnimatorTrigger:
			{
				contentPosition.y += lineHeight;
				EditorGUI.PropertyField(contentPosition, action.FindPropertyRelative("s1"), triggerContent);
				break;
			}

			case ActionType.PlaySound:
			{
				contentPosition.y += lineHeight;
				EditorGUI.PropertyField(contentPosition, action.FindPropertyRelative("soundId"));
				contentPosition.y += lineHeight;
				EditorGUI.PropertyField(contentPosition, action.FindPropertyRelative("soundType"));
				contentPosition.y += lineHeight;
				EditorGUI.PropertyField(contentPosition, action.FindPropertyRelative("f1"), delayContent);
				break;
			}
			
			case ActionType.CallEvent:
			{
				contentPosition.y += lineHeight;
				EditorGUI.PropertyField(contentPosition, action.FindPropertyRelative("e1"), eventContent);
				break;
			}
			
			case ActionType.PlayParticleSystem:
			{
				contentPosition.y += lineHeight;
				EditorGUI.PropertyField(contentPosition, action.FindPropertyRelative("b1"), withChildrenContent);
				break;
			}
		}

		EditorGUI.indentLevel--;

		EditorGUI.EndProperty();

//		EditorGUI.indentLevel = oldIndentLevel;
	}

	public override float GetPropertyHeight(SerializedProperty property, GUIContent label)
	{
		ActionType actionType = (ActionType)property.FindPropertyRelative("actionType").enumValueIndex;

		int lineCount = lines[(int)actionType] + 1;

		if (actionType == ActionType.Move || actionType == ActionType.Scale)
		{
			if (IsMultipleLines())
			{
				lineCount++;
			}
		}

		return lineCount * lineHeight;
	}

	void ActionField(SerializedProperty action, Rect contentPosition, string endProperty, bool multipleLines = false, GUIContent b2Content = null)
	{
		contentPosition.y += lineHeight;
		EditorGUI.PropertyField(contentPosition, action.FindPropertyRelative(endProperty), endContent);

		if (multipleLines && IsMultipleLines())
		{
			contentPosition.y += lineHeight;
		}

		contentPosition.y += lineHeight;
		EditorGUI.PropertyField(contentPosition, action.FindPropertyRelative("variance"));
		contentPosition.y += lineHeight;
		EditorGUI.PropertyField(contentPosition, action.FindPropertyRelative("b1"), relativeContent);

		if (b2Content != null)
		{
			contentPosition.y += lineHeight;
			EditorGUI.PropertyField(contentPosition, action.FindPropertyRelative("b2"), b2Content);
		}

		contentPosition.y += lineHeight;
		EditorGUI.PropertyField(contentPosition, action.FindPropertyRelative("duration"));
		contentPosition.y += lineHeight;
		EditorGUI.PropertyField(contentPosition, action.FindPropertyRelative("easeType"));
		contentPosition.y += lineHeight;
		EditorGUI.PropertyField(contentPosition, action.FindPropertyRelative("direction"));
	}

	bool IsMultipleLines()
	{
		return Screen.width < 333;
	}
}
