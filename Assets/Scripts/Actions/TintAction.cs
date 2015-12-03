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
using UnityEngine.UI;

public class TintAction : BaseAction
{
	// The helper
	private LerpVector3Helper _helper = new LerpVector3Helper();
	
	// Relative or absolute
	private bool _isRelative;
	
	// Is recursive?
	private bool _isRecursive;

	// The color adapter
	private ColorAdapter _colorAdapter;

	public TintAction(Vector3 end, bool isRelative, bool isRecursive, float duration, Easer easer, LerpDirection direction)
	{
		_helper.Construct(end, duration, easer, direction);

		_isRelative  = isRelative;
		_isRecursive = isRecursive;
	}

	public static TintAction Create(Vector3 end, bool isRelative, bool isRecursive, float duration, Easer easer = null, LerpDirection direction = LerpDirection.Forward)
	{
		return new TintAction(end, isRelative, isRecursive, duration, easer, direction);
	}

	public static TintAction TintTo(Vector3 end, float duration, Easer easer = null, LerpDirection direction = LerpDirection.Forward)
	{
		return Create(end, false, false, duration, easer, direction);
	}
	
	public static TintAction TintBy(Vector3 delta, float duration, Easer easer = null, LerpDirection direction = LerpDirection.Forward)
	{
		return Create(delta, true, false, duration, easer, direction);
	}
	
	public static TintAction RecursiveTintTo(Vector3 end, float duration, Easer easer = null, LerpDirection direction = LerpDirection.Forward)
	{
		return Create(end, false, true, duration, easer, direction);
	}
	
	public static TintAction RecursiveTintBy(Vector3 delta, float duration, Easer easer = null, LerpDirection direction = LerpDirection.Forward)
	{
		return Create(delta, true, true, duration, easer, direction);
	}

	public override void Play(GameObject target)
	{
		// Set color adapter
		_colorAdapter = ColorAdapter.Get(target);

		// Set start
		_helper.Start = _colorAdapter.GetRGB();
		
		if (_isRelative)
		{
			_helper.End += _helper.Start;
		}

		_helper.Play();
	}
	
	public override void Reset()
	{
		_colorAdapter.SetRGB(_helper.Start, _isRecursive);
	}
	
	public override void Stop(bool forceEnd = false)
	{
		if (!_helper.IsFinished())
		{
			_helper.Stop();

			if (forceEnd)
			{
				_colorAdapter.SetRGB(_helper.End, _isRecursive);
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
			_colorAdapter.SetRGB(_helper.Update(deltaTime), _isRecursive);
		}

		return _helper.IsFinished();
	}
}
