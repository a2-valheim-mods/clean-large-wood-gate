using System;
using System.Linq;
using UnityEngine;

namespace A2.CleanLargeWoodGate.Unity
{
    internal static class GameObjectExtensions
    {
        public static bool SetChildrenInactive(this GameObject prefab, params string[] names)
        {
            if (prefab is null) return false;

            var updated = 0;
            var children = prefab.GetComponentsInChildren<Transform>(false);
            foreach (var child in children)
            {
                if (child is not null && names.Contains(child.name, StringComparer.InvariantCultureIgnoreCase))
                {
                    var gameObject = child.gameObject;
                    if (gameObject is not null)
                    {
                        gameObject.SetActive(false);
                        updated++;
                    }
                }
            }
            return updated > 0;
        }
        public static bool SetSubChildrenInactive(this GameObject prefab, string subParent, params string[] names)
        {
            if (prefab is null) return false;

            var subParentPrefab = prefab.GetComponentsInChildren<Transform>(true)?.FirstOrDefault(x => string.Compare(x.name, subParent, StringComparison.InvariantCultureIgnoreCase) == 0);
            if (subParentPrefab is null) return false;

            var updated = 0;
            var children = subParentPrefab.GetComponentsInChildren<Transform>(false);
            foreach (var child in children)
            {
                if (child is not null && names.Contains(child.name, StringComparer.InvariantCultureIgnoreCase))
                {
                    var gameObject = child.gameObject;
                    if (gameObject is not null)
                    {
                        gameObject.SetActive(false);
                        updated++;
                    }
                }
            }
            return updated > 0;
        }
        public static bool SetChildrenActive(this GameObject prefab, params string[] names)
        {
            if (prefab is null) return false;

            var updated = 0;
            var children = prefab.GetComponentsInChildren<Transform>(true);
            foreach (var child in children)
            {
                if (child is not null && names.Contains(child.name, StringComparer.InvariantCultureIgnoreCase))
                {
                    var gameObject = child.gameObject;
                    if (gameObject is not null)
                    {
                        gameObject.SetActive(true);
                        updated++;
                    }
                }
            }
            return updated > 0;
        }
        public static bool SetSubChildrenActive(this GameObject prefab, string subParent, params string[] names)
        {
            if (prefab is null) return false;

            var subParentPrefab = prefab.GetComponentsInChildren<Transform>(true)?.FirstOrDefault(x => string.Compare(x.name, subParent, StringComparison.InvariantCultureIgnoreCase) == 0);
            if (subParentPrefab is null) return false;

            var updated = 0;
            var children = subParentPrefab.GetComponentsInChildren<Transform>(true);
            foreach (var child in children)
            {
                if (child is not null && names.Contains(child.name, StringComparer.InvariantCultureIgnoreCase))
                {
                    var gameObject = child.gameObject;
                    if (gameObject is not null)
                    {
                        gameObject.SetActive(true);
                        updated++;
                    }
                }
            }
            return updated > 0;
        }
        public static bool DisableShaderKeyword(this GameObject prefab, string rendererName, string keyword)
        {
            if (prefab is null) return false;

            var renderer = prefab?.GetComponentsInChildren<Renderer>()?.FirstOrDefault(x => StringComparer.InvariantCultureIgnoreCase.Compare(x.name, rendererName) == 0);
            if (renderer is null) return false;

            var material = renderer.sharedMaterial;
            if (material is null) return false;

            material.DisableKeyword(keyword);
            return true;
        }
        public static bool EnableShaderKeyword(this GameObject prefab, string rendererName, string keyword)
        {
            if (prefab is null) return false;

            var renderer = prefab?.GetComponentsInChildren<Renderer>()?.FirstOrDefault(x => StringComparer.InvariantCultureIgnoreCase.Compare(x.name, rendererName) == 0);
            if (renderer is null) return false;

            var material = renderer.sharedMaterial;
            if (material is null) return false;

            material.EnableKeyword(keyword);
            return true;
        }
    }
}
