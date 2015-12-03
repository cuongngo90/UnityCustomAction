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

public class SlideActionScript : MonoBehaviour
{
	/// <summary>
	/// The movement speed (units per second).
	/// </summary>
	public Vector3 speed = new Vector3(1.0f, 0.0f, 0.0f);
	
	/// <summary>
	/// Local or world.
	/// </summary>
	public bool isLocal = false;
	
	void Start()
	{
		gameObject.Play(SlideAction.Create(speed, isLocal));
		
		// Self-destroy
		Destroy(this);
	}
}
