using UnityEngine;

namespace GizmosPlus {
    public static class Draw {
        public static void Cross(Vector3 position, float size) {
            float d = size/2.0f;
            Vector3 p = position;
            Gizmos.DrawLine(p + Vector3.left*d, p + Vector3.right*d);
            Gizmos.DrawLine(p + Vector3.up*d, p + Vector3.down*d);
            Gizmos.DrawLine(p + Vector3.back*d, p + Vector3.forward*d);
        }

        public static void Arrow(Vector3 origin, Vector3 dirMagnitude) {
                
            float headSize = 0.1f;
            Vector3 end = origin + dirMagnitude;
            Vector3 arrowBase = origin + dirMagnitude * (1 - headSize);
            Vector3 left = Vector3.Cross(dirMagnitude, Vector3.up).normalized;
            Vector3 up = Vector3.Cross(left, dirMagnitude).normalized;
            // Shaft
            Gizmos.DrawLine(origin, origin + dirMagnitude);
            // 4 Arrowhead sides
            Gizmos.DrawLine(end, arrowBase + left * headSize);
            Gizmos.DrawLine(end, arrowBase + up * headSize);
            Gizmos.DrawLine(end, arrowBase - left * headSize);
            Gizmos.DrawLine(end, arrowBase - up * headSize);
            // 2 Arrowhed bases
            Gizmos.DrawLine(arrowBase + left * headSize, arrowBase - left * headSize);
            Gizmos.DrawLine(arrowBase + up * headSize, arrowBase - up * headSize);
        }

        public static void Square(Vector3 origin, Vector3 normalSize,
                                  bool crossed=false) {
            Vector3 left = Vector3.Cross(normalSize, Vector3.up).normalized;
            Vector3 up = Vector3.Cross(left, normalSize).normalized;
            float size = normalSize.magnitude / 2f;

            Gizmos.DrawLine(origin + up * size + left * size, origin + up * size - left * size);
            Gizmos.DrawLine(origin + up * size - left * size, origin - up * size - left * size);
            Gizmos.DrawLine(origin - up * size - left * size, origin - up * size + left * size);
            Gizmos.DrawLine(origin - up * size + left * size, origin + up * size + left * size);

            if (crossed) {
                Gizmos.DrawLine(origin - up * size - left * size, origin + up * size + left * size);
                Gizmos.DrawLine(origin - up * size + left * size, origin + up * size - left * size);
            }
        }

        public static void Circle(Vector3 origin, Vector3 normalRadius) {
            int segments = 32;

            Vector3 left = Vector3.Cross(normalRadius, Vector3.up).normalized;
            Vector3 up = Vector3.Cross(left, normalRadius).normalized;
            float radius = normalRadius.magnitude;

            for (int i = 0; i < segments; i++) {
                float theta0 = 2f * Mathf.PI * (float) i / segments;
                float theta1 = 2f * Mathf.PI * (float) (i+1) / segments;

                float x0 = radius * Mathf.Cos(theta0);
                float y0 = radius * Mathf.Sin(theta0);
                float x1 = radius * Mathf.Cos(theta1);
                float y1 = radius * Mathf.Sin(theta1);

                Gizmos.DrawLine(
                        origin + left * x0 + up * y0,
                        origin + left * x1 + up * y1);
            }
        }

        public static void Octahedron(Vector3 origin, float size) {
            Vector3 up = Vector3.up;
            Vector3 left = Vector3.left;
            Vector3 forward = Vector3.forward;
            size = size/2.0f;
            // Top half
            Gizmos.DrawLine(origin + left * size, origin + up * size);
            Gizmos.DrawLine(origin + forward * size, origin + up * size);
            Gizmos.DrawLine(origin - left * size, origin + up * size);
            Gizmos.DrawLine(origin - forward * size, origin + up * size);
            // "Equator"
            Gizmos.DrawLine(origin + left * size, origin + forward * size);
            Gizmos.DrawLine(origin + forward * size, origin - left * size);
            Gizmos.DrawLine(origin - left * size, origin - forward * size);
            Gizmos.DrawLine(origin - forward * size, origin + left * size);
            // Bottom half
            Gizmos.DrawLine(origin + left * size, origin - up * size);
            Gizmos.DrawLine(origin + forward * size, origin - up * size);
            Gizmos.DrawLine(origin - left * size, origin - up * size);
            Gizmos.DrawLine(origin - forward * size, origin - up * size);
        }

        public static void Lines(params Vector3[] args) {
            for (int i = 0; i < args.Length - 1; i++) {
                Gizmos.DrawLine(args[i], args[i+1]);
            }
        }
    }
}
