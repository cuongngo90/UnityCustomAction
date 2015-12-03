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
using System.Runtime.Serialization.Formatters.Binary;
using System.Xml;
using System.Xml.Serialization;
using System.IO;

public static class Helper
{
	// Get resource path of the specified file name
	public static string GetResourcePath(string fileName)
	{
#if UNITY_EDITOR || UNITY_STANDALONE
		return string.Format("{0}/Resources/{1}", Application.dataPath, fileName);
#else
		return string.Format("{0}/{1}", Application.persistentDataPath, fileName);
#endif
	}

	// Save data to binary file
	public static void SaveBinary<T>(T data, string fileName)
	{
		string path = GetResourcePath(fileName);

		var formatter = new BinaryFormatter();
		var file = File.Create(path);
		formatter.Serialize(file, data);
		file.Close();
	}

	// Load data from binary file
	public static T LoadBinary<T>(string fileName)
	{
		string path = GetResourcePath(fileName);

		if (File.Exists(path))
		{
			var formatter = new BinaryFormatter();
			var file = File.Open(path, FileMode.Open);
			T data = (T)formatter.Deserialize(file);
			file.Close();

			return data;
		}

		return default(T);
	}

	// Save data to xml file
	public static void SaveXml<T>(T data, string fileName)
	{
		string path = GetResourcePath(fileName);
		
		var serializer = new XmlSerializer(typeof(T));
		var stream = new FileStream(path, FileMode.Create);
		serializer.Serialize(stream, data);
		stream.Close();
	}
	
	// Load data from xml file
	public static T LoadXml<T>(string fileName)
	{
		string path = GetResourcePath(fileName);
		
		if (File.Exists(path))
		{
			var serializer = new XmlSerializer(typeof(T));
			var stream = new FileStream(path, FileMode.Open);
			T data = (T)serializer.Deserialize(stream);
			stream.Close();
			
			return data;
		}
		
		return default(T);
	}
	
	// Load data from xml text
	public static T LoadXmlFromText<T>(string text)
	{
		var serializer = new XmlSerializer(typeof(T));
		return (T)serializer.Deserialize(new StringReader(text));
	}

	// Check if online
	public static bool IsOnline()
	{
		return Application.internetReachability != NetworkReachability.NotReachable;
	}

#if UNITY_EDITOR
	public static UnityEditor.EditorWindow GetMainGameView()
	{
		System.Type T = System.Type.GetType("UnityEditor.GameView,UnityEditor");
		System.Reflection.MethodInfo GetMainGameView = T.GetMethod("GetMainGameView", System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Static);
		System.Object Res = GetMainGameView.Invoke(null, null);
		
		return (UnityEditor.EditorWindow)Res;
	}
	
	public static Vector2 GetMainGameViewSize()
	{
		System.Type T = System.Type.GetType("UnityEditor.GameView,UnityEditor");
		System.Reflection.MethodInfo GetSizeOfMainGameView = T.GetMethod("GetSizeOfMainGameView", System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Static);
		System.Object Res = GetSizeOfMainGameView.Invoke(null, null);
		
		return (Vector2)Res;
	}
	
	public static Vector2 GetMainGameViewPosition()
	{
		Rect rect = GetMainGameView().position;
		Vector2 size = GetMainGameViewSize();
		
		return new Vector2((rect.size.x - size.x) * 0.5f, (rect.size.y - 12 - size.y) * 0.5f);
	}
#endif

	#region Array

	// Get random indices
	public static int[] GetRandomIndices<T>(this T[] a, int count)
	{
		int length = a.Length;

		int[] tmp = new int[length];

		for (int i = 0; i < length; i++)
		{
			tmp[i] = i;
		}

		tmp.Swap();

		if (count < 0 || count >= length)
		{
			return tmp;
		}

		int[] ret = new int[count];

		for (int i = 0; i < count; i++)
		{
			ret[i] = tmp[i];
		}

		return ret;
	}

	#endregion
}
