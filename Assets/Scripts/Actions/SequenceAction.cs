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
/// Play the specified actions in sequence.
/// </summary>
public class SequenceAction : BaseAction
{
	// The array of actions
	private BaseAction[] _actions;
	
	// The length
	private int _length;
	
	// The current index
	private int _currentIdx;

	// The target
	private GameObject _target;

	// True if action finished
	private bool _isFinished;

	public SequenceAction(BaseAction[] actions)
	{
		// Set actions
		_actions = actions;

		// Set length
		_length = _actions.Length;
	}

	public static SequenceAction Create(params BaseAction[] actions)
	{
		return new SequenceAction(actions);
	}

	public override void Play(GameObject target)
	{
		// Set target
		_target = target;

		// Set current index
		_currentIdx = 0;
		
		// Set not finished
		_isFinished = false;
		
		// Play current action
		_actions[_currentIdx].Play(_target);
		
		while (_actions[_currentIdx].IsFinished())
		{
			// Go to next action
			_currentIdx++;
			
			if (_currentIdx == _length)
			{
				// Set action finished
				_isFinished = true;
				
				break;
			}
			else
			{
				// Play current action
				_actions[_currentIdx].Play(_target);
			}
		}
	}
	
	public override void Reset()
	{
		// Reset all played actions
		for (int i = _currentIdx < _length ? _currentIdx : _length - 1; i >= 0; i--)
		{
			_actions[i].Reset();
		}
	}

	public override void Stop(bool forceEnd = false)
	{
		if (!_isFinished)
		{
			// Stop current action
			_actions[_currentIdx].Stop(forceEnd);
			
			if (forceEnd)
			{
				for (_currentIdx++; _currentIdx < _length; _currentIdx++)
				{
					_actions[_currentIdx].Stop(true);
				}
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
		
		// Update current action
		if (_actions[_currentIdx].Update(deltaTime))
		{
			do
			{
				// Go to next action
				_currentIdx++;
				
				if (_currentIdx < _length)
				{
					// Play current action
					_actions[_currentIdx].Play(_target);
				}
				else
				{
					// Set action finished
					_isFinished = true;
					
					break;
				}
			}
			while (_actions[_currentIdx].IsFinished());
		}
		
		return _isFinished;
	}
}
