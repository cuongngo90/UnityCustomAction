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

public class SlideAction : BaseAction
{
	// The speed (units per second)
	private Vector3 _speed;
	
	// Local or world
	private bool _isLocal;

	// The transform
	private Transform _transform;

	// The old position
	private Vector3 _oldPosition;

	// The current position
	private Vector3 _position;

	// True if action finished
	private bool _isFinished;

	public SlideAction(Vector3 speed, bool isLocal)
	{
		// Set speed
		_speed = speed;

		// Set local
		_isLocal = isLocal;
	}

	public static SlideAction Create(Vector3 speed, bool isLocal = false)
	{
		return new SlideAction(speed, isLocal);
	}

	public override void Play(GameObject target)
	{
		// Get transform
		_transform = target.transform;

		// Save position
		_oldPosition = _isLocal ? _transform.localPosition : _transform.position;

		// Set current position
		_position = _oldPosition;

		// Not finished
		_isFinished = false;
	}

	public override void Reset()
	{
		if (_isLocal)
		{
			_transform.localPosition = _oldPosition;
		}
		else
		{
			_transform.position = _oldPosition;
		}
	}

	public override void Stop(bool forceEnd = false)
	{
		_isFinished = true;
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
		
		// Update position
		_position.x += _speed.x * Time.deltaTime;
		_position.y += _speed.y * Time.deltaTime;
		_position.z += _speed.z * Time.deltaTime;

		// Set position
		if (_isLocal)
		{
			_transform.localPosition = _position;
		}
		else
		{
			_transform.position = _position;
		}
		
		return false;
	}
}
