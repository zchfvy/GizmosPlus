using System;
using System.Collections.Generic;
using UnityEngine;

namespace Zchfvy.Plus {
    /// <summary>
    /// Internal class for use with GizmosPlusAsync
    /// <see="GizmosPlusAsync">
    /// </summary>
    public class GizmosPlusAsyncDrawer : MonoBehaviour {
        private Queue<Action> drawQueue = new Queue<Action>();

        public void Enqueue(Action newItem) {
            drawQueue.Enqueue(newItem);
        }

        void OnDrawGizmos() {
            while (drawQueue.Count > 0) {
                var act = drawQueue.Dequeue();
                act();
            }
        }
    }
}
