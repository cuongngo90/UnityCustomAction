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
using System.Collections;

public class AnimateActionScript : MonoBehaviour
{
	/// <summary>
	/// The sprites.
	/// </summary>
	public Sprite[] sprites;

	/// <summary>
	/// The frame rate.
	/// </summary>
	public float frameRate = 10f;

	void Start()
	{
		gameObject.Play(AnimateAction.Create(sprites, frameRate));
		
		// Self-destroy
		Destroy(this);
	}
}
