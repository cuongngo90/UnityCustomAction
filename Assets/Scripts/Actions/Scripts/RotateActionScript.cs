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

public class RotateActionScript : MonoBehaviour
{
	/// <summary>
	/// The end angle.
	/// </summary>
	public float end = 360.0f;

	/// <summary>
	/// The variance.
	/// </summary>
	public float variance = 0f;

	/// <summary>
	/// Is relative.
	/// </summary>
	public bool isRelative = false;

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
		gameObject.Play(RotateAction.Create(end.Variance(variance), isRelative, duration, Ease.FromType(easeType), direction), 
                        () => {
                            Debug.Log("Finish Rotate ACtion Scripts");
                        }
        );
		
		// Self-destroy
		Destroy(this);
	}
}
