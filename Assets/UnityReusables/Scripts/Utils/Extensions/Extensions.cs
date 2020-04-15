using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using Sirenix.Utilities;
using UnityEngine;
using Random = UnityEngine.Random;

namespace UnityReusables.Utils.Extensions
{
    public static class Extensions
    {
        public static T GetRandom<T>(this T[] array)
        {
            return array[Random.Range(0, array.Length)];
        }

        public static T GetRandom<T>(this List<T> list)
        {
            return list[Random.Range(0, list.Count)];
        }

        public static T Ref<T>(this T o) where T : UnityEngine.Object
        {
            return o == null ? null : o;
        }

        public static float RandomInside(this Vector2 v)
        {
            return Random.Range(v.x, v.y);
        }

        public static IEnumerator SetActive(this GameObject gameObject, bool active, float delay)
        {
            yield return new WaitForSeconds(delay);
            gameObject.SetActive(active);
        }

        public static List<Transform> GetChildrenByTag(this Transform transform, string tag)
        {
            List<Transform> taggedChildren = new List<Transform>();
            for (int i = 0; i < transform.childCount; i++)
            {
                Transform child = transform.GetChild(i);
                if (child.CompareTag(tag))
                {
                    taggedChildren.Add(child);
                }
            }

            return taggedChildren;
        }

        public static List<Transform> GetChildrenByComponent(this Transform transform, string componentTypeName)
        {
            List<Transform> childrenWithComponent = new List<Transform>();
            // get type from string
            Type type = Assembly.GetExecutingAssembly().GetType(componentTypeName);
            // get components from type
            var components = transform.GetComponentsInChildren(type);
            // get transforms from components
            components.ForEach(c => childrenWithComponent.Add(c.transform));
            return childrenWithComponent;
        }
    }
}