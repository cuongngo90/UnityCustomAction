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

public class LerpColorHelper : LerpHelper<Color>
{
	public LerpColorHelper()
	{

	}

	public LerpColorHelper(Color color)
	{
		_value = color;
	}

	protected override Color Add(Color a, Color b)
	{
		return a + b;
	}

	protected override Color Subtract(Color a, Color b)
	{
		return a - b;
	}

	protected override void Lerp(float t)
	{
		_value.r = _start.r + _delta.r * t;
		_value.g = _start.g + _delta.g * t;
		_value.b = _start.b + _delta.b * t;
	}
}
