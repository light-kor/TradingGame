using UnityEngine;

namespace Core
{
    public static class RandomUtils
    {
        public static bool IsLong()
        {
            int value = Random.Range(0, 2);
            return value == 0;
        }
    }
}