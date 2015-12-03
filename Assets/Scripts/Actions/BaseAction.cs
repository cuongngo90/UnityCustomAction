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
using System;

/// <summary>
/// This is the base class for all actions.
/// </summary>
public class BaseAction
{
	public virtual void Play(GameObject target)
	{

	}

	public virtual void Reset()
	{

	}

	public virtual void Replay(GameObject target)
	{
		Reset();

		Play(target);
	}

	public virtual void Stop(bool forceEnd = false)
	{

	}

	// Check if action finished
	public virtual bool IsFinished()
	{
		return true;
	}
	
	// Update action, return true if action finished
	public virtual bool Update(float deltaTime)
	{
		return true;
	}
}

public static class ActionHelper
{
	public static void Play(this GameObject go, BaseAction action, Action callback = null, bool isSelfDestroy = true)
	{
		// Add action script
		ActionScript actionScript = go.AddComponent<ActionScript>();

		// Play action
		actionScript.Play(action, callback, isSelfDestroy);
	}

	public static void ReplayAction(this GameObject go)
	{
		ActionScript actionScript = go.GetComponent<ActionScript>();
		
		if (actionScript != null)
		{
			actionScript.Replay();
		}
	}
	
	public static void StopAction(this GameObject go, bool forceEnd = false)
	{
		ActionScript actionScript = go.GetComponent<ActionScript>();
		
		if (actionScript != null)
		{
			actionScript.Stop(forceEnd);
		}
	}

//	#region AnchoredMove
//
//	public static void AnchoredMoveTo(this GameObject go, Vector2 end, float duration, Easer easer = null, LerpDirection direction = LerpDirection.Forward, Action callback = null)
//	{
//		go.Play(AnchoredMoveAction.MoveTo(end, duration, easer, direction), callback);
//	}
//	
//	public static void AnchoredMoveBy(this GameObject go, Vector2 end, float duration, Easer easer = null, LerpDirection direction = LerpDirection.Forward, Action callback = null)
//	{
//		go.Play(AnchoredMoveAction.MoveBy(end, duration, easer, direction), callback);
//	}
//
//	#endregion
//	
//	#region Move
//	
//	public static void MoveTo(this GameObject go, Vector3 end, float duration, Easer easer = null, LerpDirection direction = LerpDirection.Forward, Action callback = null)
//	{
//		go.Play(MoveAction.MoveTo(end, duration, easer, direction), callback);
//	}
//	
//	public static void MoveBy(this GameObject go, Vector3 end, float duration, Easer easer = null, LerpDirection direction = LerpDirection.Forward, Action callback = null)
//	{
//		go.Play(MoveAction.MoveBy(end, duration, easer, direction), callback);
//	}
//	
//	public static void MoveLocalTo(this GameObject go, Vector3 end, float duration, Easer easer = null, LerpDirection direction = LerpDirection.Forward, Action callback = null)
//	{
//		go.Play(MoveAction.MoveLocalTo(end, duration, easer, direction), callback);
//	}
//	
//	public static void MoveLocalBy(this GameObject go, Vector3 end, float duration, Easer easer = null, LerpDirection direction = LerpDirection.Forward, Action callback = null)
//	{
//		go.Play(MoveAction.MoveLocalBy(end, duration, easer, direction), callback);
//	}
//
//	#endregion
//	
//	#region Scale
//	
//	public static void ScaleTo(this GameObject go, Vector3 end, float duration, Easer easer = null, LerpDirection direction = LerpDirection.Forward, Action callback = null)
//	{
//		go.Play(ScaleAction.ScaleTo(end, duration, easer, direction), callback);
//	}
//	
//	public static void ScaleBy(this GameObject go, Vector3 end, float duration, Easer easer = null, LerpDirection direction = LerpDirection.Forward, Action callback = null)
//	{
//		go.Play(ScaleAction.ScaleBy(end, duration, easer, direction), callback);
//	}
//
//	#endregion
//	
//	#region Rotate
//	
//	public static void RotateTo(this GameObject go, float end, float duration, Easer easer = null, LerpDirection direction = LerpDirection.Forward, Action callback = null)
//	{
//		go.Play(RotateAction.RotateTo(end, duration, easer, direction), callback);
//	}
//	
//	public static void RotateBy(this GameObject go, float end, float duration, Easer easer = null, LerpDirection direction = LerpDirection.Forward, Action callback = null)
//	{
//		go.Play(RotateAction.RotateBy(end, duration, easer, direction), callback);
//	}
//	
//	#endregion
//	
//	#region Fade
//	
//	public static void FadeTo(this GameObject go, float end, float duration, Easer easer = null, LerpDirection direction = LerpDirection.Forward, Action callback = null)
//	{
//		go.Play(FadeAction.FadeTo(end, duration, easer, direction), callback);
//	}
//	
//	public static void FadeBy(this GameObject go, float end, float duration, Easer easer = null, LerpDirection direction = LerpDirection.Forward, Action callback = null)
//	{
//		go.Play(FadeAction.FadeBy(end, duration, easer, direction), callback);
//	}
//	
//	#endregion
//	
//	#region Tint
//	
//	public static void TintTo(this GameObject go, Vector3 end, float duration, Easer easer = null, LerpDirection direction = LerpDirection.Forward, Action callback = null)
//	{
//		go.Play(TintAction.TintTo(end, duration, easer, direction), callback);
//	}
//	
//	public static void TintBy(this GameObject go, Vector3 end, float duration, Easer easer = null, LerpDirection direction = LerpDirection.Forward, Action callback = null)
//	{
//		go.Play(TintAction.TintBy(end, duration, easer, direction), callback);
//	}
//	
//	#endregion
}
