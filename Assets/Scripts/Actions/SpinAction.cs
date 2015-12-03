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

public class SpinAction : BaseAction
{
	// The speed (degrees per second)
	private float _speed;
	
	// The transform
	private Transform _transform;

	// The old rotation
	private Quaternion _oldRotation;

	// The current rotation
	private Vector3 _rotation;

	// True if action finished
	private bool _isFinished;

	public SpinAction(float speed)
	{
		// Set speed
		_speed = speed;
	}

	public static SpinAction Create(float speed)
	{
		return new SpinAction(speed);
	}

	public override void Play(GameObject target)
	{
		// Get transform
		_transform = target.transform;

		// Save rotation
		_oldRotation = _transform.localRotation;

		// Set current rotation
		_rotation = _oldRotation.eulerAngles;

		// Not finished
		_isFinished = false;
	}

	public override void Reset()
	{
		_transform.localRotation = _oldRotation;
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
		
		// Update rotation
		_rotation.z += _speed * Time.deltaTime;
		
		if (_rotation.z < 0)
		{
			do
			{
				_rotation.z += 360;
			}
			while (_rotation.z < 0);
		}
		else if (_rotation.z >= 360)
		{
			do
			{
				_rotation.z -= 360;
			}
			while (_rotation.z >= 360);
		}
		
		// Set rotation
		_transform.localRotation = Quaternion.Euler(_rotation);
		
		return false;
	}
}
