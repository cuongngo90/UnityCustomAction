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

/// <summary>
/// Play the specified actions in parallel.
/// </summary>
public class ParallelAction : BaseAction
{
	// The array of actions
	private BaseAction[] _actions;

	// Is all?
	private bool _isAll = true;

	// The target
	private GameObject _target;
	
	// True if action finished
	private bool _isFinished;

	public ParallelAction(BaseAction[] actions, bool isAll)
	{
		// Set actions
		_actions = actions;
		
		// Set one or all
		_isAll = isAll;
	}

	public static ParallelAction Create(BaseAction[] actions, bool isAll)
	{
		return new ParallelAction(actions, isAll);
	}

	public static ParallelAction ParallelOne(params BaseAction[] actions)
	{
		return Create(actions, false);
	}

	public static ParallelAction ParallelAll(params BaseAction[] actions)
	{
		return Create(actions, true);
	}

	public override void Play(GameObject target)
	{
		// Set target
		_target = target;
		
		if (_isAll)
		{
			bool finished = true;
			
			// Play all actions
			for (int i = 0; i < _actions.Length; i++)
			{
				// Play current action
				_actions[i].Play(_target);
				
				if (finished)
				{
					if (!_actions[i].IsFinished())
					{
						finished = false;
					}
				}
			}
			
			// Set finished
			_isFinished = finished;
		}
		else
		{
			bool finished = false;
			
			// Play all actions
			for (int i = 0; i < _actions.Length; i++)
			{
				// Play current action
				_actions[i].Play(_target);
				
				if (!finished)
				{
					if (_actions[i].IsFinished())
					{
						finished = true;
					}
				}
			}
			
			// Set finished
			_isFinished = finished;
		}
	}

	public override void Reset()
	{
		// Reset all actions
		for (int i = _actions.Length - 1; i >= 0; i--)
		{
			_actions[i].Reset();
		}
	}
	
	public override void Stop(bool forceEnd = false)
	{
		if (!_isFinished)
		{
			// Stop all actions
			for (int i = 0; i < _actions.Length; i++)
			{
				_actions[i].Stop(forceEnd);
			}
			
			// Set finished
			_isFinished = true;
		}
	}
	
	public override bool IsFinished()
	{
		return _isFinished;
	}

	public override bool Update(float deltaTime)
	{
		// Check if action finished
		if (_isFinished)
		{
			return true;
		}
		
		if (_isAll)
		{
			bool finished = true;
			
			// Update all actions
			for (int i = 0; i < _actions.Length; i++)
			{
				// Update current action
				_actions[i].Update(deltaTime);
				
				if (finished)
				{
					if (!_actions[i].IsFinished())
					{
						finished = false;
					}
				}
			}
			
			// Set finished
			_isFinished = finished;
		}
		else
		{
			bool finished = false;
			
			// Update all actions
			for (int i = 0; i < _actions.Length; i++)
			{
				// Update current action
				_actions[i].Update(deltaTime);
				
				if (!finished)
				{
					if (_actions[i].IsFinished())
					{
						finished = true;
					}
				}
			}
			
			// Set finished
			_isFinished = finished;
		}
		
		return _isFinished;
	}
}
