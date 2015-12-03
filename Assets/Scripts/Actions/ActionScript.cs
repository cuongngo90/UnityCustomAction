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
using System.Collections;
using System;

public class ActionScript : MonoBehaviour
{
	// The current action
	private BaseAction _action = NullAction.Instance;

	// The callback
	private Action _callback;

	// Self-destroy when action finished or not
	private bool _isSelfDestroy;

	// Play the specified action
	public void Play(BaseAction action, Action callback = null, bool isSelfDestroy = true)
	{
		bool isFinished = _action.IsFinished();

		// Set current action
		_action = action;

		// Set callback
		_callback = callback;

		// Set self-destroy
		_isSelfDestroy = isSelfDestroy;

		// Play action
		_action.Play(gameObject);
		
		// Check if action finished
		if (_action.IsFinished())
		{
			if (isSelfDestroy)
			{
				if (_callback != null)
				{
					_callback();
				}

				Destroy(this);
			}
			else
			{
				if (!isFinished)
				{
					StopCoroutine("UpdateAction");
				}

				if (_callback != null)
				{
					_callback();
				}
			}
		}
		else
		{
			if (isFinished)
			{
				StartCoroutine("UpdateAction");
			}
		}
	}
	
	// Replay action
	public void Replay()
	{
		bool isFinished = _action.IsFinished();

		_action.Replay(gameObject);

		if (isFinished)
		{
			StartCoroutine("UpdateAction");
		}
	}

	// Stop action
	public void Stop(bool forceEnd = false)
	{
		_action.Stop(forceEnd);
	}

	IEnumerator UpdateAction()
	{
		while (!_action.Update(Time.deltaTime))
		{
			yield return null;
		}

		if (_callback != null)
		{
			_callback();
		}

		if (_isSelfDestroy)
		{
			Destroy(this);
		}
	}
}
