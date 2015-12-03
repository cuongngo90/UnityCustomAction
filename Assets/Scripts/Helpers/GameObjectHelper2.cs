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

public static class GameObjectHelper2
{
	public static void Show(this GameObject go)
	{
		go.SetActive(true);
	}

	public static void Hide(this GameObject go)
	{
		go.SetActive(false);
	}

	public static void SetRGB(this GameObject go, Vector3 rgb, bool isRecursive = false)
	{
		// Get sprite renderer
		SpriteRenderer spriteRenderer = go.GetComponent<SpriteRenderer>();
		
		if (spriteRenderer != null)
		{
			spriteRenderer.SetRGB(rgb);
		}
		else
		{
			// Get image
			Image image = go.GetComponent<Image>();
			
			if (image != null)
			{
				image.SetRGB(rgb);
			}
			else
			{
				// Get text
				Text text = go.GetComponent<Text>();
				
				if (text != null)
				{
					text.SetRGB(rgb);
				}
			}
		}

		if (isRecursive)
		{
			Transform transform = go.transform;
			
			for (int i = 0; i < transform.childCount; i++)
			{
				transform.GetChild(i).gameObject.SetRGB(rgb, true);
			}
		}
	}
	
	public static void SetAlpha(this GameObject go, float a, bool isRecursive = false)
	{
		// Get sprite renderer
		SpriteRenderer spriteRenderer = go.GetComponent<SpriteRenderer>();
		
		if (spriteRenderer != null)
		{
			spriteRenderer.SetAlpha(a);
		}
		else
		{
			// Get image
			Image image = go.GetComponent<Image>();
			
			if (image != null)
			{
				image.SetAlpha(a);
			}
			else
			{
				// Get text
				Text text = go.GetComponent<Text>();

				if (text != null)
				{
					text.SetAlpha(a);
				}
			}
		}

		if (isRecursive)
		{
			Transform transform = go.transform;

			for (int i = 0; i < transform.childCount; i++)
			{
				transform.GetChild(i).gameObject.SetAlpha(a, true);
			}
		}
	}
	
	public static void SetRGBInChildren(this GameObject go, Vector3 rgb)
	{
		Transform transform = go.transform;
		
		for (int i = 0; i < transform.childCount; i++)
		{
			GameObject gameObject = transform.GetChild(i).gameObject;
			
			// Get sprite renderer
			SpriteRenderer spriteRenderer = gameObject.GetComponent<SpriteRenderer>();
			
			if (spriteRenderer != null)
			{
				spriteRenderer.SetRGB(rgb);
			}
			else
			{
				// Get image
				Image image = gameObject.GetComponent<Image>();
				
				if (image != null)
				{
					image.SetRGB(rgb);
				}
				else
				{
					// Get text
					Text text = gameObject.GetComponent<Text>();
					
					if (text != null)
					{
						text.SetRGB(rgb);
					}
				}
			}
			
			gameObject.SetRGBInChildren(rgb);
		}
	}
	
	public static void SetAlphaInChildren(this GameObject go, float a)
	{
		Transform transform = go.transform;
		
		for (int i = 0; i < transform.childCount; i++)
		{
			GameObject gameObject = transform.GetChild(i).gameObject;
			
			// Get sprite renderer
			SpriteRenderer spriteRenderer = gameObject.GetComponent<SpriteRenderer>();
			
			if (spriteRenderer != null)
			{
				spriteRenderer.SetAlpha(a);
			}
			else
			{
				// Get image
				Image image = gameObject.GetComponent<Image>();
				
				if (image != null)
				{
					image.SetAlpha(a);
				}
				else
				{
					// Get text
					Text text = gameObject.GetComponent<Text>();
					
					if (text != null)
					{
						text.SetAlpha(a);
					}
				}
			}
			
			gameObject.SetAlphaInChildren(a);
		}
	}

	public static void RemoveRaycastTarget(this GameObject go)
	{
		// Image
		Image image = go.GetComponent<Image>();
		if (image != null)
		{
			Button button = go.GetComponent<Button>();

			if (button == null)
			{
				image.raycastTarget = false;
			}
		}

		// Text
		Text text = go.GetComponent<Text>();
		if (text != null)
		{
			text.raycastTarget = false;
		}

		// Children
		int childCount = go.transform.childCount;

		for (int i = 0; i < childCount; i++)
		{
			go.transform.GetChild(i).gameObject.RemoveRaycastTarget();
		}
	}

#region Image
	
	public static float GetImageWidth(this GameObject go)
	{
		Image image = go.GetComponent<Image>();
		
		if (image != null)
		{
			return image.sprite.GetWidthInPixels();
		}

		return 0;
	}
	
	public static float GetImageHeight(this GameObject go)
	{
		Image image = go.GetComponent<Image>();
		
		if (image != null)
		{
			return image.sprite.GetHeightInPixels();
		}
		
		return 0;
	}

	public static void SetImageSprite(this GameObject go, Sprite sprite)
	{
		Image image = go.GetComponent<Image>();

		if (image != null)
		{
			image.sprite = sprite;
		}
	}
	
	public static void SetImageAlpha(this GameObject go, float a)
	{
		Image image = go.GetComponent<Image>();
		
		if (image != null)
		{
			image.SetAlpha(a);
		}
	}
	
	public static void SetImageType(this GameObject go, Image.Type type)
	{
		Image image = go.GetComponent<Image>();
		
		if (image != null)
		{
			image.type = type;
		}
	}
	
	public static void SetImageFilledLeft(this GameObject go)
	{
		Image image = go.GetComponent<Image>();
		
		if (image != null)
		{
			image.type = Image.Type.Filled;
			image.fillMethod = Image.FillMethod.Horizontal;
			image.fillOrigin = (int)Image.OriginHorizontal.Left;
		}
	}
	
	public static void SetImageFillAmount(this GameObject go, float fillAmount)
	{
		Image image = go.GetComponent<Image>();
		
		if (image != null)
		{
			image.fillAmount = fillAmount;
		}
	}

#endregion

#region Text

	public static void SetText(this GameObject go, string text)
	{
		Text textComp = go.GetComponent<Text>();
		
		if (textComp != null)
		{
			textComp.text = text;
		}
	}
	
	public static void SetTextInChildren(this GameObject go, string name, string text)
	{
		GameObject child = go.FindInChildren(name);

		if (child != null)
		{
			Text textComp = child.GetComponent<Text>();
			
			if (textComp != null)
			{
				textComp.text = text;
			}
			else
			{
				Debug.Log(string.Format("{0} have not Text component!", name));
			}
		}
		else
		{
			Debug.Log(string.Format("Not found {0}!", name));
		}
	}

#endregion

#region RectTransform

	public static Vector2 GetAnchoredPosition(this GameObject go)
	{
		RectTransform rectTransform = go.GetComponent<RectTransform>();
		
		if (rectTransform != null)
		{
			return rectTransform.anchoredPosition;
		}

		return Vector2.zero;
	}

	public static void SetAnchoredPosition(this GameObject go, Vector2 position)
	{
		RectTransform rectTransform = go.GetComponent<RectTransform>();

		if (rectTransform != null)
		{
			rectTransform.anchoredPosition = position;
		}
	}
	
	public static float GetAnchoredPositionX(this GameObject go)
	{
		RectTransform rectTransform = go.GetComponent<RectTransform>();
		
		if (rectTransform != null)
		{
			return rectTransform.anchoredPosition.x;
		}
		
		return 0;
	}
	
	public static void SetAnchoredPositionX(this GameObject go, float x)
	{
		RectTransform rectTransform = go.GetComponent<RectTransform>();
		
		if (rectTransform != null)
		{
			Vector2 anchoredPosition = rectTransform.anchoredPosition;
			anchoredPosition.x = x;
			rectTransform.anchoredPosition = anchoredPosition;
		}
	}

	public static float GetAnchoredPositionY(this GameObject go)
	{
		RectTransform rectTransform = go.GetComponent<RectTransform>();
		
		if (rectTransform != null)
		{
			return rectTransform.anchoredPosition.y;
		}

		return 0;
	}

	public static void SetAnchoredPositionY(this GameObject go, float y)
	{
		RectTransform rectTransform = go.GetComponent<RectTransform>();
		
		if (rectTransform != null)
		{
			Vector2 anchoredPosition = rectTransform.anchoredPosition;
			anchoredPosition.y = y;
			rectTransform.anchoredPosition = anchoredPosition;
		}
	}
	
	public static void TranslateAnchoredX(this GameObject go, float deltaX)
	{
		RectTransform rectTransform = go.GetComponent<RectTransform>();
		
		if (rectTransform != null)
		{
			Vector2 anchoredPosition = rectTransform.anchoredPosition;
			anchoredPosition.x += deltaX;
			rectTransform.anchoredPosition = anchoredPosition;
		}
	}
	
	public static void TranslateAnchoredY(this GameObject go, float deltaY)
	{
		RectTransform rectTransform = go.GetComponent<RectTransform>();
		
		if (rectTransform != null)
		{
			Vector2 anchoredPosition = rectTransform.anchoredPosition;
			anchoredPosition.y += deltaY;
			rectTransform.anchoredPosition = anchoredPosition;
		}
	}

	public static float GetWidthDelta(this GameObject go)
	{
		RectTransform rectTransform = go.GetComponent<RectTransform>();
		
		if (rectTransform != null)
		{
			return rectTransform.sizeDelta.x;
		}

		return 0;
	}

	public static void SetWidthDelta(this GameObject go, float width)
	{
		RectTransform rectTransform = go.GetComponent<RectTransform>();
		
		if (rectTransform != null)
		{
			Vector2 sizeDelta = rectTransform.sizeDelta;
			sizeDelta.x = width;
			rectTransform.sizeDelta = sizeDelta;
		}
	}
	
	public static float GetHeightDelta(this GameObject go)
	{
		RectTransform rectTransform = go.GetComponent<RectTransform>();
		
		if (rectTransform != null)
		{
			return rectTransform.sizeDelta.y;
		}
		
		return 0;
	}

	public static void SetHeightDelta(this GameObject go, float height)
	{
		RectTransform rectTransform = go.GetComponent<RectTransform>();
		
		if (rectTransform != null)
		{
			Vector2 sizeDelta = rectTransform.sizeDelta;
			sizeDelta.y = height;
			rectTransform.sizeDelta = sizeDelta;
		}
	}

#endregion
}
