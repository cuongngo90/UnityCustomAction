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
using UnityEngine.Events;
using System;

public enum ActionType
{
	Move,
	HorizontalMove,
	VerticalMove,
	Scale,
	Rotate,
	Fade,
	Tint,
	SetPosition,
	SetScale,
	SetRotation,
	SetAlpha,
	SetRGB,
	Delay,
	AnimatorTrigger,
	PlaySound,
	CallEvent,
	PlayParticleSystem
}

[Serializable]
public class ActionData
{
	// The action type
	public ActionType actionType;

	// The temporary variables
	public float f1;
	public float f2;
	public bool b1;
	public bool b2;
	public Vector3 v31;
	public Vector3 v32;
	public Color c1 = Color.white;
	public Color c2;
	public string s1;
    public SoundID soundId;
    public SoundType soundType;
    public UnityEvent e1;

	// The variance
	public float variance = 0.0f;

	// The duration
	public float duration = 1.0f;
	
	// The ease type
	public EaseType easeType;
	
	// The direction
	public LerpDirection direction;
	
	public BaseAction GetAction()
	{
		if (actionType == ActionType.Move)
		{
			return MoveAction.Create(v31.VarianceXY(variance), b1, b2, duration, Ease.FromType(easeType), direction);
		}
		
		if (actionType == ActionType.HorizontalMove)
		{
			return HMoveAction.Create(f1.Variance(variance), b1, b2, duration, Ease.FromType(easeType), direction);
		}
		
		if (actionType == ActionType.VerticalMove)
		{
			return VMoveAction.Create(f1.Variance(variance), b1, b2, duration, Ease.FromType(easeType), direction);
		}

		if (actionType == ActionType.Scale)
		{
			return ScaleAction.Create(v31.VarianceXY(variance), b1, duration, Ease.FromType(easeType), direction);
		}

		if (actionType == ActionType.Rotate)
		{
			return RotateAction.Create(f1.Variance(variance), b1, duration, Ease.FromType(easeType), direction);
		}

		if (actionType == ActionType.Fade)
		{
			return FadeAction.Create(f1.Variance(variance), b1, b2, duration, Ease.FromType(easeType), direction);
		}

		if (actionType == ActionType.Tint)
		{
			return TintAction.Create(c1.RGB().Variance(variance), b1, b2, duration, Ease.FromType(easeType), direction);
		}
		
		if (actionType == ActionType.SetPosition)
		{
			return SetPositionAction.Create(v31, v32, b1, b2);
		}
		
		if (actionType == ActionType.SetScale)
		{
			return SetScaleAction.Create(v31, v32, b1);
		}
		
		if (actionType == ActionType.SetRotation)
		{
			return SetRotationAction.Create(f1, f2);
		}
		
		if (actionType == ActionType.SetAlpha)
		{
			return SetAlphaAction.Create(f1, f2, b1);
		}
		
		if (actionType == ActionType.SetRGB)
		{
			return SetRGBAction.Create(c1.RGB(), c2.RGB(), b1);
		}

		if (actionType == ActionType.Delay)
		{
			return DelayAction.Create(duration, f2);
		}

		if (actionType == ActionType.AnimatorTrigger)
		{
			return AnimatorTriggerAction.Create(s1);
		}
		
		if (actionType == ActionType.PlaySound)
		{
			return PlaySoundAction.Create(soundId, soundType, f1);
		}
		
		if (actionType == ActionType.CallEvent)
		{
			return CallEventAction.Create(e1);
		}
		
		if (actionType == ActionType.PlayParticleSystem)
		{
			return PlayPSAction.Create(b1);
		}

		return null;
	}
}

public class CustomAction : MonoBehaviour
{
	/// <summary>
	/// The actions.
	/// </summary>
	public ActionData[] actions;

	/// <summary>
	/// The delay time.
	/// </summary>
	public float delay;

	/// <summary>
	/// The repeat count (-1 = infinite).
	/// </summary>
	public int repeat;

	void Start()
	{
		if (actions != null)
		{
			int count = actions.Length;

			if (count > 0)
			{
				BaseAction action;

				if (count == 1)
				{
					action = actions[0].GetAction();
				}
				else
				{
					BaseAction[] baseActions = new BaseAction[count];

					for (int i = 0; i < count; i++)
					{
						baseActions[i] = actions[i].GetAction();
					}

					action = SequenceAction.Create(baseActions);
				}

				if (repeat != 0)
				{
					if (delay > 0)
					{
						gameObject.Play(SequenceAction.Create(DelayAction.Create(delay), RepeatAction.Create(action, repeat)));
					}
					else
					{
						gameObject.Play(RepeatAction.Create(action, repeat));
					}
				}
				else
				{
					if (delay > 0)
					{
						gameObject.Play(SequenceAction.Create(DelayAction.Create(delay), action));
					}
					else
					{
						gameObject.Play(action);
					}
				}
			}
		}

		Destroy(this);
	}
}
