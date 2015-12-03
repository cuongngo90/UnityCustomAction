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

public class TypeAction : BaseAction
{
	// The text
	private string _text;

	// The helper
	private LerpIntHelper _helper = new LerpIntHelper();

	// The target
	private Text _target;

	// The current position
	private int _position;
	
	public TypeAction(string text, float duration, Easer easer, LerpDirection direction)
	{
		// Set text
		_text = text;

		_helper.Construct(_text.Length, duration, easer, direction);
	}

	public static TypeAction Create(string text, float duration, Easer easer = null, LerpDirection direction = LerpDirection.Forward)
	{
		return new TypeAction(text, duration, easer, direction);
	}

	public override void Play(GameObject target)
	{
		// Get text
		_target = target.GetComponent<Text>();
		
		if (_target != null)
		{
			// Clear text
			_target.text = "";

			// Set current position
			_position = 0;

			// Set start
			_helper.Start = 0;

			_helper.Play();
		}
		else
		{
			Debug.LogWarning("Text required!");

			_helper.Stop();
		}
	}

	public override void Stop(bool forceEnd = false)
	{
		if (!_helper.IsFinished())
		{
			_helper.Stop();

			if (forceEnd)
			{
				if (_target != null)
				{
					_target.text = _text;
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
			if (_target != null)
			{
				// Update position
				int position = _helper.Update(Time.deltaTime);

				if (_helper.IsFinished())
				{
					// Set position
					_position = position;

					// Update text
					_target.text = _text;
				}
				else
				{
					if (position != _position)
					{
						// Set position
						_position = position;
						
						// Update text
						_target.text = _text.Substring(0, _position);
					}
				}
			}
		}

		return _helper.IsFinished();
	}
}
