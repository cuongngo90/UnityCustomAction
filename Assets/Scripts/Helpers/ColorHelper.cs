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

public static class ColorHelper
{
	public static Vector3 RGB(this Color color)
	{
		return new Vector3(color.r, color.g, color.b);
	}

	public static Color Reverse(this Color color)
	{
		return new Color(1.0f - color.r, 1.0f - color.g, 1.0f - color.b, color.a);
	}
}
