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

public class LerpVector2Helper : LerpHelper<Vector2>
{
	public LerpVector2Helper()
	{
		
	}
	
	public LerpVector2Helper(Vector2 value)
	{
		_value = value;
	}

	protected override Vector2 Add(Vector2 a, Vector2 b)
	{
		return a + b;
	}

	protected override Vector2 Subtract(Vector2 a, Vector2 b)
	{
		return a - b;
	}

	protected override void Lerp(float t)
	{
		_value.x = _start.x + _delta.x * t;
		_value.y = _start.y + _delta.y * t;
	}
}
