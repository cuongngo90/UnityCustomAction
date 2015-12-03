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

public class SetAnchoredPositionAction : BaseAction
{
	// The position
	private Vector2 _position;

	public SetAnchoredPositionAction(Vector2 position)
	{
		// Set position
		_position = position;
	}
	
	public static SetAnchoredPositionAction Create(Vector2 position)
	{
		return new SetAnchoredPositionAction(position);
	}

	public override void Play(GameObject target)
	{
		// Get rect transform
		RectTransform rectTransform = target.GetComponent<RectTransform>();

		if (rectTransform != null)
		{
			rectTransform.anchoredPosition = _position;
		}
		else
		{
			Debug.LogWarning("Rect Transform required!");
		}
	}
}
