using System.Collections;
using System.Diagnostics;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;

namespace Slothsoft.UnityExtensions.Tests.PlayMode {
    public class WaitTests {
        [UnityTest]
        public IEnumerator TestWaitForEndOfFrame() {
            var stopWatch = new Stopwatch();
            stopWatch.Start();
            yield return Wait.forEndOfFrame;
            stopWatch.Stop();
            Assert.Less(stopWatch.Elapsed.TotalSeconds, Time.fixedDeltaTime);
        }
        [UnityTest]
        public IEnumerator TestWaitFixedUpdate() {
            var stopWatch = new Stopwatch();
            stopWatch.Start();
            yield return Wait.forFixedUpdate;
            stopWatch.Stop();
            Assert.Less(stopWatch.Elapsed.TotalSeconds, Time.fixedDeltaTime);
        }
        [UnityTest]
        public IEnumerator TestWaitForSeconds() {
            var stopWatch = new Stopwatch();
            stopWatch.Start();
            yield return Wait.forSeconds[0.1f];
            stopWatch.Stop();
            Assert.Greater(stopWatch.Elapsed.TotalSeconds, 0.1f);
        }
        [UnityTest]
        public IEnumerator TestWaitForSecondsRealtime() {
            var stopWatch = new Stopwatch();
            stopWatch.Start();
            yield return Wait.forSecondsRealtime[0.1f];
            stopWatch.Stop();
            Assert.Greater(stopWatch.Elapsed.TotalSeconds, 0.1f);
        }
    }
}