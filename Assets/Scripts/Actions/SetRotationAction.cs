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

public class SetRotationAction : BaseAction
{
	// The rotation
	private float _rotation;

	// The variance
	private float _variance;

	public SetRotationAction(float rotation, float variance)
	{
		// Set rotation
		_rotation = rotation;

		// Set variance
		_variance = variance;
	}
	
	public static SetRotationAction Create(float rotation, float variance = 0)
	{
		return new SetRotationAction(rotation, variance);
	}

	public override void Play(GameObject target)
	{
		target.transform.localRotation = Quaternion.Euler(new Vector3(0, 0, _rotation.Variance(_variance)));
	}
}
