using UnityEditor;
using UnityEngine;

namespace Slothsoft.UnityExtensions.Editor {
    static class AnimationClipExtensions {
        static EditorCurveBinding spriteBinding = EditorCurveBinding
            .PPtrCurve("", typeof(SpriteRenderer), "m_Sprite");

        public static AnimationClipSettings GetSettings(this AnimationClip clip) {
            return AnimationUtility.GetAnimationClipSettings(clip);
        }
        public static void SetSettings(this AnimationClip clip, AnimationClipSettings settings) {
            AnimationUtility.SetAnimationClipSettings(clip, settings);
        }

        public static ObjectReferenceKeyframe[] GetSpriteKeyframes(this AnimationClip clip) {
            return AnimationUtility.GetObjectReferenceCurve(clip, spriteBinding);
        }
        public static void SetSpriteKeyframes(this AnimationClip clip, ObjectReferenceKeyframe[] frames) {
            AnimationUtility.SetObjectReferenceCurve(clip, spriteBinding, frames);
        }
    }
}
