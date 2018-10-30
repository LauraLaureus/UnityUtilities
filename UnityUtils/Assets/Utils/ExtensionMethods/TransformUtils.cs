using UnityEngine;

namespace LaureusUtils.Transform
{
    public static class TransformUtils
    {
		public static Transform FindDeepChild(this Transform aParent, string aName)
        {
            var result = aParent.Find(aName);
            if (result != null) return result;
            foreach (Transform child in aParent)
            {
                result = FindDeepChild(child, aName);
                if (result != null) return result;
            }
            return null;
        }
    }
}
