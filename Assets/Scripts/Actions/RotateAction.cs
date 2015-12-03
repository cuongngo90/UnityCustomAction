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

public class RotateAction : BaseAction
{
	// The helper
	private LerpFloatHelper _helper = new LerpFloatHelper();

	// Relative or absolute
	private bool _isRelative;

	// The transform
	private Transform _transform;

	// The rotation
	private Vector3 _rotation = Vector3.zero;

	public RotateAction(float end, bool isRelative, float duration, Easer easer, LerpDirection direction)
	{
		_helper.Construct(end, duration, easer, direction);

		_isRelative = isRelative;
	}

	public static RotateAction Create(float end, bool isRelative, float duration, Easer easer = null, LerpDirection direction = LerpDirection.Forward)
	{
		return new RotateAction(end, isRelative, duration, easer, direction);
	}

	public static RotateAction RotateTo(float end, float duration, Easer easer = null, LerpDirection direction = LerpDirection.Forward)
	{
		return Create(end, false, duration, easer, direction);
	}
	
	public static RotateAction RotateBy(float end, float duration, Easer easer = null, LerpDirection direction = LerpDirection.Forward)
	{
		return Create(end, true, duration, easer, direction);
	}

	public override void Play(GameObject target)
	{
		// Get transform
		_transform = target.transform;

		// Set start
		_helper.Start = Quaternion.Angle(_transform.localRotation, Quaternion.identity);

		if (_isRelative)
		{
			_helper.End += _helper.Start;
		}

		_rotation.z = _helper.Start;

		_helper.Play();
	}
	
	public override void Reset()
	{
		_rotation.z = _helper.Start;
		_transform.localRotation = Quaternion.Euler(_rotation);
	}
	
	public override void Stop(bool forceEnd = false)
	{
		if (!_helper.IsFinished())
		{
			_helper.Stop();

			if (forceEnd)
			{
				_rotation.z = _helper.End;
				_transform.localRotation = Quaternion.Euler(_rotation);
			}
		}
	}
	
	public override bool IsFinished()
	{
		return _helper.IsFinished();
	}
	
	public override bool Update(float deltaTime)
	{
		if (!_helper.IsFinished())
		{
			_rotation.z = _helper.Update(Time.deltaTime);
			_transform.localRotation = Quaternion.Euler(_rotation);
		}

		return _helper.IsFinished();
	}
}
