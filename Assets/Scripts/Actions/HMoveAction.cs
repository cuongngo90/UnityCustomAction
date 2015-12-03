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

public class HMoveAction : BaseAction
{
	// The helper
	private LerpFloatHelper _helper = new LerpFloatHelper();

	// Relative or absolute
	private bool _isRelative;

	// Local or world
	private bool _isLocal;

	// The transform
	private Transform _transform;

	public HMoveAction(float end, bool isRelative, bool isLocal, float duration, Easer easer, LerpDirection direction)
	{
		_helper.Construct(end, duration, easer, direction);

		_isRelative = isRelative;
		_isLocal    = isLocal;
	}

	public static HMoveAction Create(float end, bool isRelative, bool isLocal, float duration, Easer easer = null, LerpDirection direction = LerpDirection.Forward)
	{
		return new HMoveAction(end, isRelative, isLocal, duration, easer, direction);
	}

	public static HMoveAction MoveTo(float end, float duration, Easer easer = null, LerpDirection direction = LerpDirection.Forward)
	{
		return Create(end, false, false, duration, easer, direction);
	}
	
	public static HMoveAction MoveBy(float end, float duration, Easer easer = null, LerpDirection direction = LerpDirection.Forward)
	{
		return Create(end, true, false, duration, easer, direction);
	}
	
	public static HMoveAction MoveLocalTo(float end, float duration, Easer easer = null, LerpDirection direction = LerpDirection.Forward)
	{
		return Create(end, false, true, duration, easer, direction);
	}
	
	public static HMoveAction MoveLocalBy(float end, float duration, Easer easer = null, LerpDirection direction = LerpDirection.Forward)
	{
		return Create(end, true, true, duration, easer, direction);
	}

	public override void Play(GameObject target)
	{
		// Get transform
		_transform = target.transform;

		// Get start
		_helper.Start = _isLocal ? _transform.localPosition.x : _transform.position.x;

		if (_isRelative)
		{
			_helper.End += _helper.Start;
		}

		_helper.Play();
	}
	
	public override void Reset()
	{
		if (_isLocal)
		{
			_transform.SetLocalPositionX(_helper.Start);
		}
		else
		{
			_transform.SetPositionX(_helper.Start);
		}
	}
	
	public override void Stop(bool forceEnd = false)
	{
		if (!_helper.IsFinished())
		{
			_helper.Stop();

			if (forceEnd)
			{
				if (_isLocal)
				{
					_transform.SetLocalPositionX(_helper.End);
				}
				else
				{
					_transform.SetPositionX(_helper.End);
				}
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
			if (_isLocal)
			{
				_transform.SetLocalPositionX(_helper.Update(deltaTime));
			}
			else
			{
				_transform.SetPositionX(_helper.Update(deltaTime));
			}
		}

		return _helper.IsFinished();
	}
}
