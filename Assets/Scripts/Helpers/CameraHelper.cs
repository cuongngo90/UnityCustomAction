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

public static class CameraHelper
{
	public static float GetWidth(this Camera camera)
	{
		return camera.orthographicSize * camera.aspect * 2;
	}

	public static float GetHeight(this Camera camera)
	{
		return camera.orthographicSize * 2;
	}
	
	public static float GetOrthographicWidth(this Camera camera)
	{
		return camera.orthographicSize * camera.aspect;
	}

	public static Mesh GetMesh(this Camera camera)
	{
		float halfHeight = camera.orthographicSize;
		float halfWidth  = halfHeight * camera.aspect;
		
		Mesh mesh = new Mesh();
		
		mesh.vertices = new Vector3[]
		{
			new Vector3(-halfWidth, -halfHeight, 0),
			new Vector3(-halfWidth,  halfHeight, 0),
			new Vector3( halfWidth, -halfHeight, 0),
			new Vector3( halfWidth,  halfHeight, 0)
		};
		
		mesh.uv = new Vector2[]
		{
			new Vector2(0, 0),
			new Vector2(0, 1),
			new Vector2(1, 0),
			new Vector2(1, 1)
		};
		
		mesh.triangles = new int[] { 0, 1, 2, 3, 2, 1 };
		
		return mesh;
	}
}
