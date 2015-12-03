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

public class PlayPSAction : BaseAction
{
	// The particle system
	private ParticleSystem _particleSystem;

	private bool _withChildren;

	public PlayPSAction(bool withChildren)
	{
		_withChildren = withChildren;
	}

	public static PlayPSAction Create(bool withChildren)
	{
		return new PlayPSAction(withChildren);
	}

	public override void Play(GameObject target)
	{
		// Get particle system
		_particleSystem = target.GetComponent<ParticleSystem>();

		_particleSystem.time = 0;
		_particleSystem.Play();
	}

	public override void Stop(bool forceEnd = false)
	{
		_particleSystem.Stop(_withChildren);
	}

	public override bool IsFinished()
	{
		return !_particleSystem.IsAlive(_withChildren);
	}

	public override bool Update(float deltaTime)
	{
		return !_particleSystem.IsAlive(_withChildren);
	}
}
