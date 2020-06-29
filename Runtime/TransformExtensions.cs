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
        public static void SetScaleX(this Transform transform, float value) {
            transform.localScale = new Vector3(value, transform.localScale.y, transform.localScale.z);
        }
        public static void SetScaleY(this Transform transform, float value) {
            transform.localScale = new Vector3(transform.localScale.x, value, transform.localScale.z);
        }
        public static void SetScaleZ(this Transform transform, float value) {
            transform.localScale = new Vector3(transform.localScale.x, transform.localScale.y, value);
        }
    }
}