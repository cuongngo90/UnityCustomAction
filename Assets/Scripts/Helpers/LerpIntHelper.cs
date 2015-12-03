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

public class LerpIntHelper : LerpHelper<int>
{
	public LerpIntHelper()
	{
		
	}
	
	public LerpIntHelper(int value)
	{
		_value = value;
	}

	protected override int Add(int a, int b)
	{
		return a + b;
	}

	protected override int Subtract(int a, int b)
	{
		return a - b;
	}

	protected override void Lerp(float t)
	{
		_value = (int)(_start + _delta * t);
	}
}
