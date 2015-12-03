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

public class SetAlphaAction : BaseAction
{
	// The alpha
	private float _a;

	// The variance
	private float _variance;

	// Is recursive?
	private bool _isRecursive;

	public SetAlphaAction(float a, float variance, bool isRecursive)
	{
		// Set alpha
		_a = a;

		// Set variance
		_variance = variance;

		// Set recursive
		_isRecursive = isRecursive;
	}
	
	public static SetAlphaAction Create(float a, float variance = 0, bool isRecursive = false)
	{
		return new SetAlphaAction(a, variance, isRecursive);
	}

	public override void Play(GameObject target)
	{
		target.SetAlpha(Mathf.Clamp01(_a.Variance(_variance)), _isRecursive);
	}
}
