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

public class LerpFloatHelper : LerpHelper<float>
{
	public LerpFloatHelper()
	{

	}

	public LerpFloatHelper(float value)
	{
		_value = value;
	}

	protected override float Add(float a, float b)
	{
		return a + b;
	}

	protected override float Subtract(float a, float b)
	{
		return a - b;
	}

	protected override void Lerp(float t)
	{
		_value = _start + _delta * t;
	}
}
