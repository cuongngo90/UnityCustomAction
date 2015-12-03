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

public class SetRGBAction : BaseAction
{
	// The rgb
	private Vector3 _rgb;

	// The variance
	private Vector3 _variance;
	
	// Is recursive?
	private bool _isRecursive;

	public SetRGBAction(Vector3 rgb, Vector3 variance, bool isRecursive)
	{
		// Set RGB
		_rgb = rgb;

		// Set variance
		_variance = variance;

		// Set recursive
		_isRecursive = isRecursive;
	}
	
	public static SetRGBAction Create(Vector3 rgb, Vector3? variance = null, bool isRecursive = false)
	{
		return new SetRGBAction(rgb, variance ?? Vector3.zero, isRecursive);
	}

	public override void Play(GameObject target)
	{
		target.SetRGB(_rgb.Variance(_variance).Clamp01(), _isRecursive);
	}
}
