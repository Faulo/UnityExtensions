using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Slothsoft.UnityExtensions {
    public static class TransformExtensions {
        public static void SetX(this Transform transform, float value) {
            transform.position = new Vector3(value, transform.position.y, transform.position.z);
        }
        public static void SetY(this Transform transform, float value) {
            transform.position = new Vector3(transform.position.x, value, transform.position.z);
        }
        public static void SetZ(this Transform transform, float value) {
            transform.position = new Vector3(transform.position.x, transform.position.y, value);
        }
    }
}