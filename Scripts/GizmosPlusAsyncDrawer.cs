using System;
using System.Collections.Generic;
using UnityEngine;

namespace Zchfvy.Plus {
    /// <summary>
    /// Internal class for use with GizmosPlusAsync
    /// See <see cref="GizmosPlusAsync">GizmosPlusAsync</see> for functionality
    /// </summary>
    public class GizmosPlusAsyncDrawer : MonoBehaviour {
        private Queue<Action> drawQueue = new Queue<Action>();
        private Queue<(GameObject, Action)> drawSelectedQueue = new Queue<(GameObject, Action)>();

        public void Enqueue(Action newItem) {
            drawQueue.Enqueue(newItem);
        }

        public void EnqueueSelected(GameObject gameObject, Action newItem) {
            drawSelectedQueue.Enqueue((gameObject, newItem));
        }

        void OnDrawGizmos() {
            while (drawQueue.Count > 0) {
                var act = drawQueue.Dequeue();
                act();
            }

            while (drawSelectedQueue.Count > 0) {
                var (gameObject, act) = drawSelectedQueue.Dequeue();

#if UNITY_EDITOR
                if (UnityEditor.Selection.Contains(gameObject.GetInstanceID())) {
                    act();
                }
#endif
            }
        }
    }
}
