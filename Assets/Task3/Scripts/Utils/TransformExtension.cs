using UnityEngine;

namespace Task3.Scripts
{
    public static class TransformExtension
    {
        public static void ToWorld(this Transform transform)
        {
            transform.SetParent(null);
        }
    }
}