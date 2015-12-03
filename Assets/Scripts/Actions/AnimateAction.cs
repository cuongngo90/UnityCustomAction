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

public class AnimateAction : BaseAction
{
	// The sprites
	private Sprite[] _sprites;

	// The frame duration
	private float _frameDuration = 0.1f;

	// The sprite renderer
	private SpriteRenderer _spriteRenderer;

	// The image
	private Image _image;

	// The current frame
	private int _currentFrame;

	// The accumulative time
	private float _time;

	// True if finished
	private bool _isFinished;

	public AnimateAction(Sprite[] sprites, float frameRate)
	{
		// Set sprites
		_sprites = sprites;

		// Set frame duration
		_frameDuration = 1.0f / frameRate;
	}

	public static AnimateAction Create(Sprite[] sprites, float frameRate)
	{
		return new AnimateAction(sprites, frameRate);
	}

	public override void Play(GameObject target)
	{
		// Set current frame
		_currentFrame = 0;

		// Not finished
		_isFinished = false;

		// Get sprite renderer
		_spriteRenderer = target.GetComponent<SpriteRenderer>();

		if (_spriteRenderer != null)
		{
			_spriteRenderer.sprite = _sprites[_currentFrame];
		}
		else
		{
			// Get image
			_image = target.GetComponent<Image>();

			if (_image != null)
			{
				_image.sprite = _sprites[_currentFrame];
			}
			else
			{
				_isFinished = true;
			}
		}

		// Clear time
		_time = 0;
	}

	public override void Stop(bool forceEnd = false)
	{
		if (!_isFinished)
		{
			if (forceEnd)
			{
				if (_spriteRenderer != null)
				{
					_spriteRenderer.sprite = _sprites[_sprites.Length - 1];
				}
				else if (_image != null)
				{
					_image.sprite = _sprites[_sprites.Length - 1];
				}
			}

			_isFinished = true;
		}
	}

	public override bool IsFinished()
	{
		return _isFinished;
	}

	public override bool Update(float deltaTime)
	{
		if (!_isFinished)
		{
			// Update time
			_time += Time.deltaTime;
			
			if (_time >= _frameDuration)
			{
				do
				{
					// Increase frame
					_currentFrame++;
					
					// Decrease time
					_time -= _frameDuration;
				}
				while (_time >= _frameDuration);
				
				while (_currentFrame >= _sprites.Length)
				{
					_currentFrame -= _sprites.Length;
				}
				
				// Set sprite
				if (_spriteRenderer != null)
				{
					_spriteRenderer.sprite = _sprites[_currentFrame];
				}
				else if (_image != null)
				{
					_image.sprite = _sprites[_currentFrame];
				}
			}
		}

		return _isFinished;
	}
}
