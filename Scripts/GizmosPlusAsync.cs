
using System;
using UnityEngine;

namespace Zchfvy.Plus {
    /// <summary>
    /// Class for drawing Gizmos outside of OnDrawGizmos
    /// </summary>
    public static class GizmosPlusAsync {
        private static GizmosPlusAsyncDrawer drawer;
        /// <summary>
        /// Allows drawing of Gizmos outside of OnDrawGizmos flow
        /// </summary>
        /// <param name="drawFunc">
        /// A lambda expression containing relevant drawing code
        /// </param>
        /// <example>
        /// In some Main Thread function other than OnDrawGizmos
        /// <code>
        /// GizmosPlusAsync.DrawAsync(() => {
        ///     Gizmos.DrawLine(...);
        ///     GizmosPlus.DrawCross(...);
        /// });
        /// </code>
        /// </example>
        public static void DrawAsync(Action drawFunc) {
            if (drawer == null) {
                var go = new GameObject("_GizmosPlusAsyncDrawer");
                drawer = go.AddComponent<GizmosPlusAsyncDrawer>();
            }
            drawer.Enqueue(drawFunc);
        }
    }
}
