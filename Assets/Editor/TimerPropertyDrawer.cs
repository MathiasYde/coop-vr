using UnityEditor;
using UnityEngine;

namespace Editor {
    [CustomPropertyDrawer(typeof(Timer))]
    public class TimerPropertyDrawer : PropertyDrawer {
        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label) {
            SerializedProperty timeoutProperty = property.FindPropertyRelative("timeout");

            EditorGUI.PropertyField(position, timeoutProperty, label);
        }
    }
}