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

public class SetScaleAction : BaseAction
{
	// The scale
	private Vector3 _scale;

	// The variance
	private Vector3 _variance;

	// Preserve aspect
	private bool _preserveAspect;

	public SetScaleAction(Vector3 scale, Vector3 variance, bool preserveAspect)
	{
		// Set scale
		_scale = scale;

		// Set variance
		_variance = variance;

		// Set preserve aspect
		_preserveAspect = preserveAspect;
	}
	
	public static SetScaleAction Create(Vector3 scale, Vector3? variance = null, bool preserveAspect = true)
	{
		return new SetScaleAction(scale, variance ?? Vector3.zero, preserveAspect);
	}

	public override void Play(GameObject target)
	{
		Vector3 scale = _scale.Variance(_variance);

		if (_preserveAspect)
		{
			scale.x = scale.y = scale.z = (scale.x + scale.y + scale.z) / 3;
		}

		target.transform.localScale = scale;
	}
}
