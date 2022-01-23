using UnityEngine;

namespace Model
{
    public static class Util
    {
        public static int IntLerp(int a, int b, float t)
        {
            return Mathf.RoundToInt((b - a) * t + a);
        }
    }
}