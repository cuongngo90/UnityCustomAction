using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;
using System.Text;
using System;
using Random = UnityEngine.Random;

public static class Extensions
{
    private static Vector3 DefaultPosition = Vector3.zero;
    private static Vector3 DefaultScale = Vector3.one;
    private static Quaternion DefaultRotation = Quaternion.identity;
    private static Vector2 DefaultDeltaSize = Vector2.zero;

    private static readonly ASCIIEncoding asciiEncoding = new ASCIIEncoding();

    #region Transform

    // Set position x
    public static void SetPositionX(this Transform transform, float x)
    {
        Vector3 position = transform.position;
        position.x = x;
        transform.position = position;
    }

    // Set position y
    public static void SetPositionY(this Transform transform, float y)
    {
        Vector3 position = transform.position;
        position.y = y;
        transform.position = position;
    }

    // Set local position x
    public static void SetLocalPositionX(this Transform transform, float x)
    {
        Vector3 position = transform.localPosition;
        position.x = x;
        transform.localPosition = position;
    }

    // Set local position y
    public static void SetLocalPositionY(this Transform transform, float y)
    {
        Vector3 position = transform.localPosition;
        position.y = y;
        transform.localPosition = position;
    }

    // Translate along x-axis
    public static void TranslateX(this Transform transform, float deltaX)
    {
        Vector3 position = transform.position;
        position.x += deltaX;
        transform.position = position;
    }

    // Translate along y-axis
    public static void TranslateY(this Transform transform, float deltaY)
    {
        Vector3 position = transform.position;
        position.y += deltaY;
        transform.position = position;
    }

    // Set scale x
    public static void SetScaleX(this Transform transform, float scaleX)
    {
        Vector3 scale = transform.localScale;
        scale.x = scaleX;
        transform.localScale = scale;
    }

    // Set scale y
    public static void SetScaleY(this Transform transform, float scaleY)
    {
        Vector3 scale = transform.localScale;
        scale.y = scaleY;
        transform.localScale = scale;
    }
	
	// Set scale
	public static void SetScale(this Transform transform, float scale)
	{
		transform.localScale = new Vector3(scale, scale, scale);
	}

    // Horizontal flip
    public static void HorizontalFlip(this Transform transform)
    {
        Vector3 scale = transform.localScale;
        scale.x *= -1.0f;
        transform.localScale = scale;
    }

    // Vertical flip
    public static void VerticalFlip(this Transform transform)
    {
        Vector3 scale = transform.localScale;
        scale.y *= -1.0f;
        transform.localScale = scale;
    }

    // Get children of this transform
    public static Transform[] GetChildren(this Transform transform)
    {
        int childCount = transform.childCount;

        Transform[] children = new Transform[childCount];

        for (int i = 0; i < childCount; i++)
        {
            children[i] = transform.GetChild(i);
        }

        return children;
    }

    // Get all children of this transform (recursively)
    public static Transform[] GetAllChildren(this Transform transform)
    {
        List<Transform> children = new List<Transform>();

        // Get number of children
        int childCount = transform.childCount;

        for (int i = 0; i < childCount; i++)
        {
            // Get current child
            Transform child = transform.GetChild(i);

            // Add child to list
            children.Add(child);

            // Add children to list recursively
            child.GetAllChildrenRecursively(children);
        }

        return children.ToArray();
    }

    static void GetAllChildrenRecursively(this Transform transform, List<Transform> children)
    {
        // Get number of children
        int childCount = transform.childCount;

        // Loop through all children
        for (int i = 0; i < childCount; i++)
        {
            // Get current child
            Transform child = transform.GetChild(i);

            // Add child to list
            children.Add(child);

            // Add children to list recursively
            child.GetAllChildrenRecursively(children);
        }
    }

    public static Transform FindInChildren(this Transform transform, string name)
    {
        // Get number of children
        int childCount = transform.childCount;

        // Breadth-first search
        for (int i = 0; i < childCount; i++)
        {
            // Get current child
            Transform child = transform.GetChild(i);

            if (child.name == name)
            {
                return child;
            }
        }

        // Depth-first search
        for (int i = 0; i < childCount; i++)
        {
            Transform child = transform.GetChild(i).FindInChildren(name);

            if (child != null)
            {
                return child;
            }
        }

        return null;
    }

    public static T GetComponentInChildren<T>(this Transform transform, string name)
    {
        Transform childTransform = transform.FindInChildren(name);

        if (childTransform != null)
        {
            return childTransform.GetComponent<T>();
        }

        return default(T);
    }

    // Traverse all children of this transform (recursively)
    public static void TraverseChildren<T>(this Transform transform, Func<Transform, T, bool> func, T arg)
    {
        int childCount = transform.childCount;

        for (int i = 0; i < childCount; i++)
        {
            if (!transform.GetChild(i).TraverseChildrenRecursively<T>(func, arg))
            {
                break;
            }
        }
    }

    static bool TraverseChildrenRecursively<T>(this Transform transform, Func<Transform, T, bool> func, T arg)
    {
        if (!func(transform, arg))
        {
            return false;
        }

        int childCount = transform.childCount;

        for (int i = 0; i < childCount; i++)
        {
            if (!transform.GetChild(i).TraverseChildrenRecursively<T>(func, arg))
            {
                return false;
            }
        }

        return true;
    }

    public static void DestroyChildren(this Transform transform)
    {
        for (int i = transform.childCount - 1; i >= 0; i--)
        {
            GameObject.Destroy(transform.GetChild(i).gameObject);
        }
    }

    public static void DestroyImmediateChildren(this Transform transform)
    {
        for (int i = transform.childCount - 1; i >= 0; i--)
        {
            GameObject.DestroyImmediate(transform.GetChild(i).gameObject);
        }
    }

    public static void DestroyImmediateChildren(this Transform transform, string tag)
    {
        for (int i = transform.childCount - 1; i >= 0; i--)
        {
            GameObject child = transform.GetChild(i).gameObject;

            if (child.CompareTag(tag))
            {
                GameObject.DestroyImmediate(child);
            }
        }
    }

    public static GameObject SpawnChild(this Transform transform, GameObject prefab, Vector2 position)
    {
        GameObject child = GameObject.Instantiate(prefab, DefaultPosition, DefaultRotation) as GameObject;
        child.transform.SetParent(transform);
        child.transform.localScale = DefaultScale;

        // Get rect transform
        RectTransform rectTransform = child.GetComponent<RectTransform>();

        if (rectTransform != null)
        {
            rectTransform.sizeDelta = DefaultDeltaSize;
            rectTransform.anchoredPosition = position;
        }

        return child;
    }

    public static void AddSortingOrder(this Transform transform, int orderOffset)
    {
        // Get renderer component
        Renderer renderer = transform.GetComponent<Renderer>();

        if (renderer != null)
        {
            renderer.sortingOrder += orderOffset;
        }

        // Get number of children
        int childCount = transform.childCount;

        // Loop through all children
        for (int i = 0; i < childCount; i++)
        {
            transform.GetChild(i).AddSortingOrder(orderOffset);
        }
    }

    #endregion

    #region RectTransform

    // Get height
    public static float GetHeight(this RectTransform transform)
    {
        return transform.sizeDelta.y;
    }

    // Set height
    public static void SetHeight(this RectTransform transform, float height)
    {
        transform.sizeDelta = new Vector2(transform.sizeDelta.x, height);
    }

    #endregion

    #region Renderer

    public static bool IsVisibleFrom(this Renderer renderer, Camera camera)
    {
        Plane[] planes = GeometryUtility.CalculateFrustumPlanes(camera);
        return GeometryUtility.TestPlanesAABB(planes, renderer.bounds);
    }

    public static float GetSpriteWidth(this SpriteRenderer renderer)
    {
        return renderer.sprite.bounds.size.x;
    }

    public static float GetSpriteHeight(this SpriteRenderer renderer)
    {
        return renderer.sprite.bounds.size.y;
    }

    public static Vector2 GetSize(this SpriteRenderer renderer)
    {
        Vector3 size = renderer.sprite.bounds.size;

        return new Vector2(size.x, size.y);
    }

    public static float GetWidth(this Sprite sprite)
    {
        return sprite.bounds.size.x;
    }

    public static float GetHeight(this Sprite sprite)
    {
        return sprite.bounds.size.y;
    }
	
	public static float GetWidthInPixels(this Sprite sprite)
	{
		return sprite.bounds.size.x * sprite.pixelsPerUnit;
	}
	
	public static float GetHeightInPixels(this Sprite sprite)
	{
		return sprite.bounds.size.y * sprite.pixelsPerUnit;
	}

	public static float GetLeftExtend(this Sprite sprite)
	{
		return sprite.pivot.x / sprite.pixelsPerUnit;
	}
	
	public static float GetRightExtend(this Sprite sprite)
	{
		return sprite.bounds.size.x - sprite.pivot.x / sprite.pixelsPerUnit;
	}

    #endregion

    #region GameObject

    public static GameObject FindInChildren(this GameObject go, string name)
    {
        Transform transform = go.transform.FindInChildren(name);

        return (transform != null) ? transform.gameObject : null;
    }

    public static T GetComponentInChildren<T>(this GameObject go, string name)
    {
        return go.transform.GetComponentInChildren<T>(name);
    }

    public static T[] FindChildrenOfType<T>(this GameObject go)
    {
        // Get transform
        Transform transform = go.transform;

        // Get number of children
        int childCount = transform.childCount;

        List<T> children = new List<T>(childCount);

        // Loop through all children
        for (int i = 0; i < childCount; i++)
        {
            T child = transform.GetChild(i).GetComponent<T>();

            if (child != null)
            {
                children.Add(child);
            }
        }

        return children.ToArray();
    }

    // Set sprite
    public static void SetSprite(this GameObject go, Sprite sprite)
    {
        // Get renderer component
        SpriteRenderer renderer = go.GetComponent<SpriteRenderer>();

        if (renderer != null)
        {
            renderer.sprite = sprite;
        }
    }

    // Set sprite color
    public static void SetSpriteColor(this GameObject go, Color color)
    {
        // Get renderer component
        SpriteRenderer renderer = go.GetComponent<SpriteRenderer>();

        if (renderer != null)
        {
            renderer.color = color;
        }
    }

	public static T GetOrAddComponent<T>(this GameObject gameObject) where T : Component
	{
		T component = gameObject.GetComponent<T>();
		
		if (component == null)
		{
			component = gameObject.AddComponent<T>();
		}
		
		return component;
	}

    #endregion

    #region Dictionary

    public static string ToStringExt(this Dictionary<string, object> dict)
    {
        StringBuilder sb = new StringBuilder("{");
        bool first = true;

        foreach (var key in dict.Keys)
        {
            var value = dict[key];

            if (value.GetType() == typeof(Dictionary<string, object>))
            {
                if (first)
                {
                    sb.Append(string.Format(" {0} = {1}", key, ((Dictionary<string, object>)value).ToStringExt()));
                    first = false;
                }
                else
                {
                    sb.Append(string.Format(", {0} = {1}", key, ((Dictionary<string, object>)value).ToStringExt()));
                }
            }
            else
            {
                if (first)
                {
                    sb.Append(string.Format(" {0} = {1}", key, value));
                    first = false;
                }
                else
                {
                    sb.Append(string.Format(", {0} = {1}", key, value));
                }
            }
        }

        sb.Append(" }");

        return sb.ToString();
    }

    #endregion

    #region String

    public static string SubString(this string s, int maxLength)
    {
        return s.Length <= maxLength ? s : s.Substring(0, maxLength);
    }

    public static string[] SplitLines(this string s)
    {
        return s.Split(new char[] { '\n' }, StringSplitOptions.RemoveEmptyEntries);
    }

    public static int ToInt(this string s)
    {
        int value = 0;
        int.TryParse(s, out value);

        return value;
    }

    public static float ToFloat(this string s)
    {
        float value = 0f;
        float.TryParse(s, out value);

        return value;
    }

    public static float ToFloatData(this string s)
    {
        float value = 0f;
        float.TryParse(s, out value);

        return value * 0.001f;
    }

    // Get ASCII string
    public static string ToAscii(this string s)
    {
        return asciiEncoding.GetString(asciiEncoding.GetBytes(s));
    }

    #endregion

    #region Time

    public static int ToTimeData(this float playTime)
    {
        return Mathf.RoundToInt(playTime * 100);
    }

    public static float ToPlayTime(this int timeData)
    {
        return timeData * 0.01f;
    }

    public static float ToPlayTime(this float playTime)
    {
        return Mathf.RoundToInt(playTime * 100) * 0.01f;
    }

    public static string ToTimeString(this float time)
    {
        // Calculate seconds
        int seconds = Mathf.FloorToInt(time);

        // Calculate centiseconds
        int centiseconds = Mathf.RoundToInt((time - seconds) * 100);

        // Calculate minutes
        int minutes = 0;

        if (seconds >= 60)
        {
            // Calculate minutes
            minutes = Mathf.FloorToInt(seconds / 60f);

            // Decrease seconds
            seconds -= minutes * 60;

            //TODO
            if (minutes > 99)
            {
                minutes = 99;
            }
        }

        return string.Format("{0:00}:{1:00}.{2:00}", minutes, seconds, centiseconds);
    }

    public static int ToIntData(this float timer)
    {
        return Mathf.RoundToInt(timer * 1000);
    }

    #endregion

    #region AudioSource

    public static void Copy(this AudioSource audioSource, AudioSource other)
    {
        if (other == null) return;

        // Audio clip
        audioSource.clip = other.clip;

        // Output
        audioSource.outputAudioMixerGroup = other.outputAudioMixerGroup;

        // Mute
        audioSource.mute = other.mute;

        // Bypass effects
        audioSource.bypassEffects = other.bypassEffects;

        // Bypass listener effect
        audioSource.bypassListenerEffects = other.bypassListenerEffects;

        // Bypass reverb zones
        audioSource.bypassReverbZones = other.bypassReverbZones;

        // Play on awake
        audioSource.playOnAwake = other.playOnAwake;

        // Loop
        audioSource.loop = other.loop;

        // Priority
        audioSource.priority = other.priority;

        // Volume
        audioSource.volume = other.volume;

        // Pitch
        audioSource.pitch = other.pitch;

        // Stereo pan
        audioSource.panStereo = other.panStereo;

        // Spatial blend
        audioSource.spatialBlend = other.spatialBlend;

        // Reverb zone mix
        audioSource.reverbZoneMix = other.reverbZoneMix;

        // Doppler level
        audioSource.dopplerLevel = other.dopplerLevel;

        // Volume rolloff
        audioSource.rolloffMode = other.rolloffMode;

        // Min distance
        audioSource.minDistance = other.minDistance;

        // Spread
        audioSource.spread = other.spread;

        // Max distance
        audioSource.maxDistance = other.maxDistance;
    }

    #endregion

    #region WWW

    public static string[] ToColumns(this WWW url)
    {
        return url.text.Split('\t');
    }

    public static string[] ToRows(this WWW url)
    {
        return url.text.Split(new char[] { '\n' }, StringSplitOptions.RemoveEmptyEntries);
    }

    #endregion

    #region Draw

    public static void DebugDrawRect(float bottom, float left, float top, float right, Color? color = null, float duration = 0f)
    {
        Color colour = color ?? Color.blue;

        Vector3 p1 = new Vector3(left, bottom, 0);
        Vector3 p2 = new Vector3(left, top, 0);
        Vector3 p3 = new Vector3(right, top, 0);
        Vector3 p4 = new Vector3(right, bottom, 0);

        Debug.DrawLine(p1, p2, colour, duration);
        Debug.DrawLine(p2, p3, colour, duration);
        Debug.DrawLine(p3, p4, colour, duration);
        Debug.DrawLine(p4, p1, colour, duration);
    }

    public static void DebugDrawRect(Vector2 center, float width, float height, Color? color = null, float duration = 0f)
    {
        float left = center.x - width * 0.5f;
        float right = left + width;
        float bottom = center.y - height * 0.5f;
        float top = bottom + height;

        DebugDrawRect(bottom, left, top, right, color, duration);
    }

    public static void GizmosDrawRect(float bottom, float left, float top, float right, Color? color = null)
    {
        Gizmos.color = color ?? Color.blue;

        Vector3 p1 = new Vector3(left, bottom, 0);
        Vector3 p2 = new Vector3(left, top, 0);
        Vector3 p3 = new Vector3(right, top, 0);
        Vector3 p4 = new Vector3(right, bottom, 0);

        Gizmos.DrawLine(p1, p2);
        Gizmos.DrawLine(p2, p3);
        Gizmos.DrawLine(p3, p4);
        Gizmos.DrawLine(p4, p1);
    }

    public static void GizmosDrawRect(Vector3 center, float width, float height, Color? color = null)
    {
        float left = center.x - width * 0.5f;
        float right = left + width;
        float bottom = center.y - height * 0.5f;
        float top = bottom + height;

        GizmosDrawRect(bottom, left, top, right, color);
    }

    public static void GizmosFillRect(float bottom, float left, float top, float right, Color? color = null)
    {
        Gizmos.color = color ?? Color.blue;

        Vector3 from = new Vector3(left, 0, 0);
        Vector3 to = new Vector3(right, 0, 0);

        for (float y = bottom; y < top; y += 1)
        {
            from.y = to.y = y;

            Gizmos.DrawLine(from, to);
        }

        from.y = to.y = top;

        Gizmos.DrawLine(from, to);
    }

    public static void GizmosFillRect(Vector2 center, float width, float height, Color? color = null)
    {
        float left = center.x - width * 0.5f;
        float right = left + width;
        float bottom = center.y - height * 0.5f;
        float top = bottom + height;

        GizmosFillRect(bottom, left, top, right, color);
    }

    #endregion

    #region int

    public static int[] RandomIndices(this int n)
    {
        int[] a = new int[n];

        for (int i = 0; i < n; i++)
        {
            a[i] = i;
        }

        a.Swap();

        return a;
    }

    public static void Swap(this int[] a)
    {
        int length = a.Length;
        int tmp;

        if (length < 2)
        {
            return;
        }

        if (length == 2)
        {
            if (Random.value > 0.5f)
            {
                tmp = a[0];
                a[0] = a[1];
                a[1] = tmp;
            }

            return;
        }

        for (int i = 0; i < length; i++)
        {
            int j = Random.Range(0, length);

            if (i != j)
            {
                tmp = a[i];
                a[i] = a[j];
                a[j] = tmp;
            }
        }
    }

    public static string ToStringExt(this int[] a)
    {
        int length = a.Length;

        if (length < 1)
        {
            return "[]";
        }

        if (length == 1)
        {
            return string.Format("[{0}]", a[0]);
        }

        StringBuilder sb = new StringBuilder();

        sb.Append("[" + a[0]);

        for (int i = 1; i < length; i++)
        {
            sb.Append(", " + a[i]);
        }

        sb.Append("]");

        return sb.ToString();
    }

    #endregion

    #region Rect

    public static void Multi(this Rect rect, Rect rect2)
    {
        rect.xMin *= rect2.xMin;
        rect.xMax = rect2.xMax;
        rect.yMax *= rect2.yMax;
        rect.yMin *= rect2.yMin;
    }

    public static Rect Multi(this Rect rect, float width, float height)
    {
        Rect rectMulti = new Rect();
        rectMulti.xMin = rect.xMin * width;
        rectMulti.xMax = rect.xMax * width;
        rectMulti.yMin = rect.yMin * height;
        rectMulti.yMax = rect.yMax * height;
        return rectMulti;
    }

    public static Rect RatioWithScreen(this Rect rect)
    {        
        rect.xMin *= Screen.width;
        rect.xMax *= Screen.width;
        rect.yMin *= Screen.height;
        rect.yMax *= Screen.height;
        return rect;
    }
    #endregion        

	#region Float

	public static float Variance(this float f, float variance)
	{
		return variance != 0 ? f + Random.Range(-variance, variance) : f;
	}

	#endregion

    #region Vector2

    public static Vector2 Multi(this Vector2 vec, Vector2 other)
    {
        return new Vector2(vec.x * other.x, vec.y * other.y);
    }

	public static Vector2 Variance(this Vector2 v2, float variance)
	{
		return variance != 0 ? new Vector2(v2.x + Random.Range(-variance, variance), v2.y + Random.Range(-variance, variance)) : v2;
	}

    #endregion
	
	#region Vector3

	public static Vector3 Clamp01(this Vector3 v3)
	{
		return new Vector3(Mathf.Clamp01(v3.x), Mathf.Clamp01(v3.y), Mathf.Clamp01(v3.z));
	}

	public static Vector3 Variance(this Vector3 v3, float variance)
	{
		return variance != 0 ? new Vector3(v3.x + Random.Range(-variance, variance), v3.y + Random.Range(-variance, variance), v3.z + Random.Range(-variance, variance)) : v3;
	}
	
	public static Vector3 Variance(this Vector3 v3, Vector3 variance)
	{
		return new Vector3(variance.x != 0 ? v3.x + Random.Range(-variance.x, variance.x) : v3.x,
		                   variance.y != 0 ? v3.y + Random.Range(-variance.y, variance.y) : v3.y,
		                   variance.z != 0 ? v3.z + Random.Range(-variance.z, variance.z) : v3.z);
	}

	public static Vector3 VarianceX(this Vector3 v3, float variance)
	{
		return variance != 0 ? new Vector3(v3.x + Random.Range(-variance, variance), v3.y, v3.z) : v3;
	}
	
	public static Vector3 VarianceY(this Vector3 v3, float variance)
	{
		return variance != 0 ? new Vector3(v3.x, v3.y + Random.Range(-variance, variance), v3.z) : v3;
	}
	
	public static Vector3 VarianceXY(this Vector3 v3, float variance)
	{
		return variance != 0 ? new Vector3(v3.x + Random.Range(-variance, variance), v3.y + Random.Range(-variance, variance), v3.z) : v3;
	}

	#endregion
}
