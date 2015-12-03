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

public class FillAction : BaseAction
{
	// The helper
	private LerpFloatHelper _helper = new LerpFloatHelper();
	
	// Relative or absolute
	private bool _isRelative;

	// The image
	private Image _image;
	
	public FillAction(float end, bool isRelative, float duration, Easer easer, LerpDirection direction)
	{
		_helper.Construct(end, duration, easer, direction);

		_isRelative = isRelative;
	}

	public static FillAction Create(float end, bool isRelative, float duration, Easer easer = null, LerpDirection direction = LerpDirection.Forward)
	{
		return new FillAction(end, isRelative, duration, easer, direction);
	}

	public static FillAction FillTo(float end, float duration, Easer easer = null, LerpDirection direction = LerpDirection.Forward)
	{
		return Create(end, false, duration, easer, direction);
	}
	
	public static FillAction FillBy(float delta, float duration, Easer easer = null, LerpDirection direction = LerpDirection.Forward)
	{
		return Create(delta, true, duration, easer, direction);
	}

	public override void Play(GameObject target)
	{
		// Get image
		_image = target.GetComponent<Image>();
		
		if (_image != null)
		{
			// Set start
			_helper.Start = _image.fillAmount;
			
			if (_isRelative)
			{
				_helper.End += _helper.Start;
			}

			_helper.Play();
		}
		else
		{
			Debug.LogWarning("Image required!");

			_helper.Stop();
		}
	}
	
	public override void Reset()
	{
		if (_image != null)
		{
			_image.fillAmount = _helper.Start;
		}
	}
	
	public override void Stop(bool forceEnd = false)
	{
		if (!_helper.IsFinished())
		{
			_helper.Stop();

			if (forceEnd)
			{
				if (_image != null)
				{
					_image.fillAmount = _helper.End;
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
			if (_image != null)
			{
				_image.fillAmount = _helper.Update(deltaTime);
			}
		}

		return _helper.IsFinished();
	}
}
