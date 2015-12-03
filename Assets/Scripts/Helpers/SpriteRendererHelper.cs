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

public static class SpriteHelper
{
	public static void SetRGB(this SpriteRenderer renderer, Vector3 rgb)
	{
		Color color = renderer.color;
		color.r = rgb.x;
		color.g = rgb.y;
		color.b = rgb.z;
		
		renderer.color = color;
	}

	public static void SetAlpha(this SpriteRenderer renderer, float alpha)
	{
		Color color = renderer.color;
		color.a = alpha;

		renderer.color = color;
	}
}
