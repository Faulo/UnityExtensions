using UnityEngine;

namespace Slothsoft.UnityExtensions {
    public static class TransformExtensions {
        /// <summary>
        /// Retrieves all objects directly beneath <paramref name="parent"/> in the hierarchy.
        /// </summary>
        /// <param name="parent"></param>
        /// <returns></returns>
        public static Transform[] GetChildren(this Transform parent) {
            var children = new Transform[parent.childCount];
            for (int i = 0; i < parent.childCount; i++) {
                children[i] = parent.GetChild(i);
            }
            return children;
        }

        /// <summary>
        /// Deletes all children of <paramref name="parent"/>.
        /// </summary>
        /// <param name="parent"></param>
        public static void Clear(this Transform parent) {
            foreach (var child in parent.GetChildren()) {
                if (Application.isPlaying) {
                    Object.Destroy(child.gameObject);
                } else {
                    Object.DestroyImmediate(child.gameObject);
                }
            }
        }

        /// <summary>
        /// Attempts to locate a component in the hierarchy. First searches <paramref name="context"/>, then its ancestors.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="context"></param>
        /// <param name="target"></param>
        /// <returns></returns>
        public static bool TryGetComponentInParent<T>(this Transform context, out T target)
            where T : class {
            for (var ancestor = context; ancestor; ancestor = ancestor.parent) {
                if (ancestor.TryGetComponent(out target)) {
                    return true;
                }
            }
            target = default;
            return false;
        }

        /// <summary>
        /// Attempts to locate a component in the hierarchy. First searches <paramref name="context"/>, then its descendants (depth-first).
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="context"></param>
        /// <param name="target"></param>
        /// <returns></returns>
        public static bool TryGetComponentInChildren<T>(this Transform context, out T target)
            where T : class {
            if (context.TryGetComponent(out target)) {
                return true;
            }
            for (int i = 0; i < context.childCount; i++) {
                if (context.GetChild(i).TryGetComponentInChildren(out target)) {
                    return true;
                }
            }
            return false;
        }

        /// <summary>
        /// Attempts to locate a component in the hierarchy. First searches <paramref name="context"/>, then its ancestors, then its descendants (depth-first).
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="context"></param>
        /// <param name="target"></param>
        /// <returns></returns>
        public static bool TryGetComponentInHierarchy<T>(this Transform context, out T target)
            where T : class {
            for (var ancestor = context; ancestor; ancestor = ancestor.parent) {
                if (ancestor.TryGetComponent(out target)) {
                    return true;
                }
            }
            for (int i = 0; i < context.childCount; i++) {
                if (context.GetChild(i).TryGetComponentInChildren(out target)) {
                    return true;
                }
            }
            target = default;
            return false;
        }
    }
}