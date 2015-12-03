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

public class TimelineFloatHelper : TimelineHelper<float>
{
	public TimelineFloatHelper()
	{
		
	}
	
	public TimelineFloatHelper(float[] times, float[] values)
	{
		Construct(times, values, true);
	}

	protected override void Lerp(float start, float end, float t)
	{
		_value = start + (end - start) * t;
	}
}
