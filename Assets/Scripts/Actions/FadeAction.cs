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

public class FadeAction : BaseAction
{
	// The helper
	private LerpFloatHelper _helper = new LerpFloatHelper();
	
	// Relative or absolute
	private bool _isRelative;

	// Is recursive?
	private bool _isRecursive;

	// The color adapter
	private ColorAdapter _colorAdapter;
	
	public FadeAction(float end, bool isRelative, bool isRecursive, float duration, Easer easer, LerpDirection direction)
	{
		_helper.Construct(end, duration, easer, direction);

		_isRelative  = isRelative;
		_isRecursive = isRecursive;
	}

	public static FadeAction Create(float end, bool isRelative, bool isRecursive, float duration, Easer easer = null, LerpDirection direction = LerpDirection.Forward)
	{
		return new FadeAction(end, isRelative, isRecursive, duration, easer, direction);
	}

	public static FadeAction FadeTo(float end, float duration, Easer easer = null, LerpDirection direction = LerpDirection.Forward)
	{
		return Create(end, false, false, duration, easer, direction);
	}
	
	public static FadeAction FadeBy(float delta, float duration, Easer easer = null, LerpDirection direction = LerpDirection.Forward)
	{
		return Create(delta, true, false, duration, easer, direction);
	}

	public static FadeAction FadeIn(float duration, Easer easer = null, LerpDirection direction = LerpDirection.Forward)
	{
		return Create(1.0f, false, false, duration, easer, direction);
	}

	public static FadeAction FadeOut(float duration, Easer easer = null, LerpDirection direction = LerpDirection.Forward)
	{
		return Create(0.0f, false, false, duration, easer, direction);
	}
	
	public static FadeAction RecursiveFadeTo(float end, float duration, Easer easer = null, LerpDirection direction = LerpDirection.Forward)
	{
		return Create(end, false, true, duration, easer, direction);
	}
	
	public static FadeAction RecursiveFadeBy(float delta, float duration, Easer easer = null, LerpDirection direction = LerpDirection.Forward)
	{
		return Create(delta, true, true, duration, easer, direction);
	}
	
	public static FadeAction RecursiveFadeIn(float duration, Easer easer = null, LerpDirection direction = LerpDirection.Forward)
	{
		return Create(1.0f, false, true, duration, easer, direction);
	}
	
	public static FadeAction RecursiveFadeOut(float duration, Easer easer = null, LerpDirection direction = LerpDirection.Forward)
	{
		return Create(0.0f, false, true, duration, easer, direction);
	}

	public override void Play(GameObject target)
	{
		// Set color adapter
		_colorAdapter = ColorAdapter.Get(target);

		// Set start
		_helper.Start = _colorAdapter.GetAlpha();
		
		if (_isRelative)
		{
			_helper.End += _helper.Start;
		}
		
		_helper.Play();
	}
	
	public override void Reset()
	{
		_colorAdapter.SetAlpha(_helper.Start, _isRecursive);
	}
	
	public override void Stop(bool forceEnd = false)
	{
		if (!_helper.IsFinished())
		{
			_helper.Stop();

			if (forceEnd)
			{
				_colorAdapter.SetAlpha(_helper.End, _isRecursive);
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
			_colorAdapter.SetAlpha(_helper.Update(deltaTime), _isRecursive);
		}

		return _helper.IsFinished();
	}
}
