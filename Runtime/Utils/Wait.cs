using System.Collections.Generic;
using UnityEngine;

namespace Slothsoft.UnityExtensions {
    public static class Wait {
        public class WaitForSecondsCache {
            Dictionary<float, WaitForSeconds> cache = new();
            public WaitForSeconds this[float time] => cache.TryGetValue(time, out var wait)
                ? wait
                : cache[time] = new WaitForSeconds(time);
        }
        public class WaitForSecondsRealtimeCache {
            Dictionary<float, WaitForSecondsRealtime> cache = new();
            public WaitForSecondsRealtime this[float time] => cache.TryGetValue(time, out var wait)
                ? wait
                : cache[time] = new WaitForSecondsRealtime(time);
        }

        public static readonly WaitForFixedUpdate forFixedUpdate = new();
        public static readonly WaitForEndOfFrame forEndOfFrame = new();
        public static readonly WaitForSecondsCache forSeconds = new();
        public static readonly WaitForSecondsRealtimeCache forSecondsRealtime = new();
    }
}