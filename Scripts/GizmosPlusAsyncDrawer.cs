using System;
using System.Collections;
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

        private void Awake() {
            StartCoroutine(ClearDrawQueues());
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

        /// <summary>
        /// Clears the draw queues at the end of each frame.
        /// </summary>
        ///
        /// <remarks>
        /// In the editor the draw queues are emptied automatically in <see cref="OnDrawGizmos"/>.
        /// However, if there is no scene view window in the editor then <see cref="OnDrawGizmos"/>
        /// is never called. This is also the case for the built player. To avoid accumulating
        /// draw calls forever in these cases (which would eventually lead to out of memory
        /// errors), we clear the draw queues at the end of every frame. This is specifically
        /// setup as a coroutine because <see cref="WaitForEndOfFrame"/> is the latest script
        /// lifecycle event that happens in the update loop, meaning that we can reliably clear
        /// the draw queues at that point without "losing" any drawn gizmos.
        /// </remarks>
        private IEnumerator ClearDrawQueues() {
            while (true) {
                yield return new WaitForEndOfFrame();
                drawQueue.Clear();
                drawSelectedQueue.Clear();
            }
        }
    }
}
