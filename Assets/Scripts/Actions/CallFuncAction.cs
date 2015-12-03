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
using System;

public class CallFuncAction : BaseAction
{
	// The callback
	private Action _callback;
	
	public CallFuncAction(Action callback)
	{
		// Set callback
		_callback = callback;
	}
	
	public static CallFuncAction Create(Action callback)
	{
		return new CallFuncAction(callback);
	}

	public override void Play(GameObject target)
	{
		if (_callback != null)
		{
			_callback();
		}
	}
}
