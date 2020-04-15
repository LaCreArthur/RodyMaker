using System;
using UnityEditor;
using UnityEngine;

#if UNITY_EDITOR
using Sirenix.OdinInspector.Editor;
#endif

namespace UnityReusables.Utils.LayerTagDropdown
{
    [AttributeUsage(AttributeTargets.Field | AttributeTargets.Property)]
    public class TagDropdownAttribute : Attribute
    {
    }

#if UNITY_EDITOR

    public sealed class TagDropdownAttributeDrawer : OdinAttributeDrawer<TagDropdownAttribute, string>
    {
        [Obsolete]
        protected override void DrawPropertyLayout(IPropertyValueEntry<string> entry, TagDropdownAttribute attribute,
            GUIContent label)
        {
            entry.SmartValue = EditorGUILayout.TagField(label != null ? label : new GUIContent(""), entry.SmartValue);
        }
    }

#endif
}