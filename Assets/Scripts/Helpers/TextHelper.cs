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
using UnityEngine.UI;
using System;

public static class TextHelper
{
	public static void SetRGB(this Text text, Vector3 rgb)
	{
		Color color = text.color;
		color.r = rgb.x;
		color.g = rgb.y;
		color.b = rgb.z;

		text.color = color;
	}

	public static void SetAlpha(this Text text, float alpha)
	{
		Color color = text.color;
		color.a = alpha;

		text.color = color;
	}
}
