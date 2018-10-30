using UnityEngine;

namespace LaureusUtils.GameObject
{   
	public static class GameObjectUtils
	{
		public static T TransformChildAddComponent<T>(this GameObject obj, string path) where T : Component
		{
			return obj.transform.Find(path).gameObject.AddComponent<T>();
		}

		public static T TransformChildGetComponent<T>(this GameObject obj, string path) where T : Component
		{
			return obj.transform.Find(path).gameObject.GetComponent<T>();
		}
	}
}