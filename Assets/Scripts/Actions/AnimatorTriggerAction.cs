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

public class AnimatorTriggerAction : BaseAction
{
	// The trigger name
	private string _name;
	
	public AnimatorTriggerAction(string name)
	{
		// Set name
		_name = name;
	}
	
	public static AnimatorTriggerAction Create(string name)
	{
		return new AnimatorTriggerAction(name);
	}

	public override void Play(GameObject target)
	{
		Animator animator = target.GetComponent<Animator>();

		if (animator != null)
		{
			animator.SetTrigger(_name);
		}
	}
}
