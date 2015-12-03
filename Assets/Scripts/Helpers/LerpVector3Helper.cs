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

public class LerpVector3Helper : LerpHelper<Vector3>
{
	public LerpVector3Helper()
	{
		
	}
	
	public LerpVector3Helper(Vector3 value)
	{
		_value = value;
	}

	protected override Vector3 Add(Vector3 a, Vector3 b)
	{
		return a + b;
	}

	protected override Vector3 Subtract(Vector3 a, Vector3 b)
	{
		return a - b;
	}

	protected override void Lerp(float t)
	{
		_value.x = _start.x + _delta.x * t;
		_value.y = _start.y + _delta.y * t;
		_value.z = _start.z + _delta.z * t;
	}
}
