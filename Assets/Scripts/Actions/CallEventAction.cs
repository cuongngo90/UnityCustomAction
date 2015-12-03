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
using UnityEngine.Events;

public class CallEventAction : BaseAction
{
	// The event
	UnityEvent _event;
	
	public CallEventAction(UnityEvent evt)
	{
		// Set event
		_event = evt;
	}
	
	public static CallEventAction Create(UnityEvent evt)
	{
		return new CallEventAction(evt);
	}

	public override void Play(GameObject target)
	{
		_event.Invoke();
	}
}
