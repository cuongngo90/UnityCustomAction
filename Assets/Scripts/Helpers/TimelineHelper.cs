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

public abstract class TimelineHelper<T>
{
	// The array of times
	private float[] _times;
	
	// The array of values
	private T[] _values;

	// The current value
	protected T _value;

	// The array length
	private int _length;

	// The maximum time
	private float _maxTime;

	// The current key
	private int _key;

	// The time
	private float _time;
	
	// True if finished
	private bool _isFinished = true;
	
	protected abstract void Lerp(T start, T end, float t);

	public void Construct(float[] times, T[] values, bool isFinished = false)
	{
		Debug.Assert(times.Length == values.Length && times.Length > 1);
		
		// Set times
		_times = times;
		
		// Set values
		_values = values;

		// Set length
		_length = _times.Length;
		
		// Set current value
		_value = isFinished ? _values[_length - 1] : _values[0];

		// Set max time
		_maxTime = _times[_length - 1];
		
		// Set current key
		_key = 0;
		
		// Set time
		_time = isFinished ? _maxTime : _times[0];

		// Set finished
		_isFinished = isFinished;
	}
	
	public void Play()
	{
		// Set current key
		_key = 0;
		
		// Set time
		_time = _times[0];

		// Not finished
		_isFinished = false;
	}

	public bool IsFinished()
	{
		return _isFinished;
	}

	public T Update(float deltaTime)
	{
		if (_isFinished)
		{
			return _value;
		}

		// Increase time
		_time += deltaTime;

		if (_time < _maxTime)
		{
			while (_time >= _times[_key + 1])
			{
				_key++;
			}

			Lerp(_values[_key], _values[_key + 1], (_time - _times[_key]) / (_times[_key + 1] - _times[_key]));
		}
		else
		{
			// Set value to end
			_value = _values[_length - 1];

			// Set finished
			_isFinished = true;
		}

		return _value;
	}
}
