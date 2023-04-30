using System;
using System.Collections.Generic;
using Reflex.Enums;
using UnityEngine;

namespace Mirzipan.Framed.Extensions
{
    public static class ComponentExtensions
    {
        public static IEnumerable<MonoBehaviour> GetInjectables<T>(this T component, MonoInjectionMode injectionMode)
            where T : Component
        {
            return injectionMode switch
            {
                MonoInjectionMode.Single => component.GetComponent<MonoBehaviour>().Yield(),
                MonoInjectionMode.Object => component.GetComponents<MonoBehaviour>(),
                MonoInjectionMode.Recursive => component.GetComponentsInChildren<MonoBehaviour>(true),
                _ => throw new ArgumentOutOfRangeException(nameof(injectionMode), injectionMode, null)
            };
        }
    }
}