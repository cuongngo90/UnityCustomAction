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

public abstract class ColorAdapter
{
	// Get RGB
	public abstract Vector3 GetRGB();

	// Set RGB
	public abstract void SetRGB(Vector3 rgb, bool isRecursive);

	// Get alpha
	public abstract float GetAlpha();

	// Set alpha
	public abstract void SetAlpha(float a, bool isRecursive);

	public static ColorAdapter Get(GameObject go)
	{
		// Get sprite renderer
		SpriteRenderer spriteRenderer = go.GetComponent<SpriteRenderer>();
		
		if (spriteRenderer != null)
		{
			return new SpriteRendererColor(spriteRenderer);
		}

		// Get image
		Image image = go.GetComponent<Image>();
		
		if (image != null)
		{
			return new ImageColor(image);
		}

		// Get text
		Text text = go.GetComponent<Text>();
		
		if (text != null)
		{
			return new TextColor(text);
		}

		return NullColorAdapter.Instance;
	}
}

#region SpriteRenderer

public class SpriteRendererColor : ColorAdapter
{
	// The sprite renderer
	private SpriteRenderer _spriteRenderer;

	public SpriteRendererColor(SpriteRenderer spriteRenderer)
	{
		// Set sprite renderer
		_spriteRenderer = spriteRenderer;
	}

	public override Vector3 GetRGB()
	{
		return _spriteRenderer.color.RGB();
	}

	public override void SetRGB(Vector3 rgb, bool isRecursive)
	{
		_spriteRenderer.SetRGB(rgb);

		if (isRecursive)
		{
			_spriteRenderer.gameObject.SetRGBInChildren(rgb);
		}
	}

	public override float GetAlpha()
	{
		return _spriteRenderer.color.a;
	}

	public override void SetAlpha(float a, bool isRecursive)
	{
		_spriteRenderer.SetAlpha(a);

		if (isRecursive)
		{
			_spriteRenderer.gameObject.SetAlphaInChildren(a);
		}
	}
}

#endregion // SpriteRenderer

#region Image

public class ImageColor : ColorAdapter
{
	// The image
	private Image _image;
	
	public ImageColor(Image image)
	{
		// Set image
		_image = image;
	}
	
	public override Vector3 GetRGB()
	{
		return _image.color.RGB();
	}
	
	public override void SetRGB(Vector3 rgb, bool isRecursive)
	{
		_image.SetRGB(rgb);

		if (isRecursive)
		{
			_image.gameObject.SetRGBInChildren(rgb);
		}
	}
	
	public override float GetAlpha()
	{
		return _image.color.a;
	}
	
	public override void SetAlpha(float a, bool isRecursive)
	{
		_image.SetAlpha(a);

		if (isRecursive)
		{
			_image.gameObject.SetAlphaInChildren(a);
		}
	}
}

#endregion // Image

#region Text

public class TextColor : ColorAdapter
{
	// The text
	private Text _text;
	
	public TextColor(Text text)
	{
		// Set text
		_text = text;
	}
	
	public override Vector3 GetRGB()
	{
		return _text.color.RGB();
	}
	
	public override void SetRGB(Vector3 rgb, bool isRecursive)
	{
		_text.SetRGB(rgb);
	}
	
	public override float GetAlpha()
	{
		return _text.color.a;
	}
	
	public override void SetAlpha(float a, bool isRecursive)
	{
		_text.SetAlpha(a);
	}
}

#endregion // Text

#region Null

public class NullColorAdapter : ColorAdapter
{
	// Null Object
	private static NullColorAdapter _instance = new NullColorAdapter();

	public static NullColorAdapter Instance
	{
		get
		{
			return _instance;
		}
	}

	public override Vector3 GetRGB()
	{
		return Vector3.one;
	}
	
	public override void SetRGB(Vector3 rgb, bool isRecursive)
	{

	}
	
	public override float GetAlpha()
	{
		return 1.0f;
	}
	
	public override void SetAlpha(float a, bool isRecursive)
	{

	}
}

#endregion // Null
