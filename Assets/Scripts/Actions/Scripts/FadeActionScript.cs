﻿///////////////////////////////////////////////////////////////////////////////
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

public class FadeActionScript : MonoBehaviour
{
	/// <summary>
	/// The end alpha.
	/// </summary>
	public float end = 1.0f;
	
	/// <summary>
	/// Is relative.
	/// </summary>
	public bool isRelative = false;

	/// <summary>
	/// Is recursive?
	/// </summary>
	public bool isRecursive = false;

	/// <summary>
	/// The duration.
	/// </summary>
	public float duration = 1.0f;
	
	/// <summary>
	/// The type of ease.
	/// </summary>
	public EaseType easeType = EaseType.Linear;
	
	/// <summary>
	/// The direction.
	/// </summary>
	public LerpDirection direction = LerpDirection.Forward;
	
	void Start()
	{
		gameObject.Play(FadeAction.Create(end, isRelative, isRecursive, duration, Ease.FromType(easeType), direction));
		
		// Self-destroy
		Destroy(this);
	}
}
