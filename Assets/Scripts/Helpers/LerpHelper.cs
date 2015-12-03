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

public abstract class LerpHelper<T>
{
	// The start value
	protected T _start;

	// The end value
	protected T _end;

	// The delta value
	protected T _delta;
	
	// The current value
	protected T _value;

	// The duration
	private float _duration;
	
	// The easer
	private Easer _easer;
	
	// The direction
	private LerpDirection _direction;

	// The accumulative time
	private float _time;
	
	// True if finished
	private bool _isFinished = true;

	protected abstract T Add(T a, T b);
	protected abstract T Subtract(T a, T b);
	protected abstract void Lerp(float t);

	public T Start
	{
		get
		{
			return _start;
		}
		set
		{
			_start = value;
		}
	}
	
	public T End
	{
		get
		{
			return _end;
		}
		set
		{
			_end = value;
		}
	}

//	public LerpHelper()
//	{
//
//	}
//	
//	public LerpHelper(T value)
//	{
//		_value = value;
//	}
//
//	public LerpHelper(T start, T end, float duration, Easer easer = null, LerpDirection direction = LerpDirection.Forward)
//	{
//		Construct(start, end, duration, easer, direction);
//	}

	public void Construct(T start, T end, float duration, Easer easer = null, LerpDirection direction = LerpDirection.Forward)
	{
		// Set start value
		_start = start;

		// Set end value
		_end = end;

		// Set delta value
		_delta = Subtract(end, start);
		
		// Set current value
		_value = (direction == LerpDirection.Backward) ? end : start;

		// Set duration
		_duration = duration;
		
		// Set easer
		_easer = easer ?? Ease.Linear;
		
		// Set direction
		_direction = direction;
		
		// Clear time
		_time = 0;
		
		// Not finished
		_isFinished = false;
	}

	public void Construct(T end, float duration, Easer easer = null, LerpDirection direction = LerpDirection.Forward)
	{
		// Set end value
		_end = end;
		
		// Set duration
		_duration = duration;
		
		// Set easer
		_easer = easer ?? Ease.Linear;
		
		// Set direction
		_direction = direction;
	}

	public void Play()
	{
		// Set delta value
		_delta = Subtract(_end, _start);
		
		// Set current value
		_value = (_direction == LerpDirection.Backward) ? _end : _start;

		// Clear time
		_time = 0;
		
		// Not finished
		_isFinished = false;
	}

	public void Stop()
	{
		_isFinished = true;
	}

	public T Get()
	{
		return _value;
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
		
		// Forward
		if (_direction == LerpDirection.Forward)
		{
			// Increase time
			_time += deltaTime;
			
			if (_time < _duration)
			{
				Lerp(_easer(_time / _duration));
			}
			else
			{
				// Set value to end
				_value = _end;

				// Set finished
				_isFinished = true;
			}
		}
		// Backward
		else if (_direction == LerpDirection.Backward)
		{
			// Increase time
			_time += deltaTime;
			
			if (_time < _duration)
			{
				Lerp(_easer(1.0f - _time / _duration));
			}
			else
			{
				// Set value to start
				_value = _start;
				
				// Set finished
				_isFinished = true;
			}
		}
		// Ping-pong
		else
		{
			// Forward
			if (_time >= 0)
			{
				// Increase time
				_time += deltaTime;
				
				if (_time < _duration)
				{
					Lerp(_easer(_time / _duration));
				}
				else
				{
					// Set value to end
					_value = _end;

					// Switch time
					_time = (_time == _duration) ? -Mathf.Epsilon : _duration - _time;
				}
			}
			// Backward
			else
			{
				// Decrease time
				_time -= deltaTime;
				
				if (-_time < _duration)
				{
					Lerp(1.0f - _easer(-_time / _duration));
				}
				else
				{
					// Set value to start
					_value = _start;
					
					if (_direction == LerpDirection.PingPong)
					{
						// Set finished
						_isFinished = true;
					}
					else
					{
						// Switch time
						_time = -_time - _duration;
					}
				}
			}
		}
		
		return _value;
	}
}
