using UnityEngine;

namespace GizmosPlus {
    public static class Draw {
        ///<summary>
        ///Creates three intersecting lines on the X,Y,Z axis crossing at a
        ///point.
        ///</summary>
        ///<param name="position">Location in the game world to place the cross.</param>
        ///<param name="size">Size of the cross.</param>
        public static void Cross(Vector3 position, float size) {
            float d = size/2.0f;
            Vector3 p = position;
            Gizmos.DrawLine(p + Vector3.left*d, p + Vector3.right*d);
            Gizmos.DrawLine(p + Vector3.up*d, p + Vector3.down*d);
            Gizmos.DrawLine(p + Vector3.back*d, p + Vector3.forward*d);
        }

        ///<summary>
        ///Creates a wireframe octahedron at a point in space.
        ///</summary>
        ///<param name="position">Location in the game world to place the octahedron.</param>
        ///<param name="size">Size of the octahedron.</param>
        public static void WireOcto(Vector3 position, float size) {
            Vector3 up = Vector3.up;
            Vector3 left = Vector3.left;
            Vector3 forward = Vector3.forward;
            size = size/2.0f;
            // Top half
            Gizmos.DrawLine(position + left * size, position + up * size);
            Gizmos.DrawLine(position + forward * size, position + up * size);
            Gizmos.DrawLine(position - left * size, position + up * size);
            Gizmos.DrawLine(position - forward * size, position + up * size);
            // "Equator"
            Gizmos.DrawLine(position + left * size, position + forward * size);
            Gizmos.DrawLine(position + forward * size, position - left * size);
            Gizmos.DrawLine(position - left * size, position - forward * size);
            Gizmos.DrawLine(position - forward * size, position + left * size);
            // Bottom half
            Gizmos.DrawLine(position + left * size, position - up * size);
            Gizmos.DrawLine(position + forward * size, position - up * size);
            Gizmos.DrawLine(position - left * size, position - up * size);
            Gizmos.DrawLine(position - forward * size, position - up * size);
        }

        ///<summary>
        ///Creates an arrow originating from a point and pointing in a certian
        ///direction with a certain length.
        ///</summary>
        ///<param name="origin">The point the arrow's base is at.</param>
        ///<param name="dirMagnitude">
        ///A vector representing both the direction and length of the arrow.
        ///</param>
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

        ///<summary>
        ///Construct a square in space
        ///</summary>
        ///<param name="origin">The center point of the square</param>
        ///<param name="normalSize">
        ///The normal and size of the square, each side of the square will have
        ///a length equivalent to this vector's magnitude.
        ///</param>
        ///<param name="crossed">
        ///Optional, if true a cross will be drawn through opposite borders of
        ///the square.
        ///</param>
        public static void Square(Vector3 origin, Vector3 normalSize,
                                     bool crossed=false) {
            float size = normalSize.magnitude;
            Rectangle(origin, normalSize, size, size, crossed);
        }

        ///<summary>
        ///Construct a rectangle in space
        ///</summary>
        ///<param name="origin">The center point of the rectangle</param>
        ///<param name="normal"> The normal of the rectangle.</param>
        ///<param name="width">The width of the rectangle.</param>
        ///<param name="height">The height of the rectangle.</param>
        ///<param name="crossed">
        ///Optional, if true a cross will be drawn through opposite borders of
        ///the rectangle.
        ///</param>
        public static void Rectangle(Vector3 origin, Vector3 normal,
                                     float width, float height,
                                     bool crossed=false) {
            Vector3 left = Vector3.Cross(normal, Vector3.up).normalized;
            Vector3 up = Vector3.Cross(left, normal).normalized;

            Gizmos.DrawLine(origin + up * height + left * width, origin + up * height - left * width);
            Gizmos.DrawLine(origin + up * height - left * width, origin - up * height - left * width);
            Gizmos.DrawLine(origin - up * height - left * width, origin - up * height + left * width);
            Gizmos.DrawLine(origin - up * height + left * width, origin + up * height + left * width);

            if (crossed) {
                Gizmos.DrawLine(origin - up * height - left * width, origin + up * height + left * width);
                Gizmos.DrawLine(origin - up * height + left * width, origin + up * height - left * width);
            }
        }

        ///<summary>
        ///Construct a circle in space
        ///</summary>
        ///<param name="origin">The center point of the circle</param>
        ///<param name="normalRadius">
        ///The normal and radius of the circle, the redius of the circle will be
        ///equivalent to this vector's magnitude.
        ///</param>
        ///<param name="segments">
        ///Optional, the number of segments to construct the circle out of.
        ///Defaults to 32.
        ///</param>
        public static void Circle(Vector3 origin, Vector3 normalRadius,
                                  int segments=32) {

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

        ///<summary>
        ///Draw a series of connected line segments.
        ///</summary>
        ///<param name="points">The points to draw the segments through</param>
        public static void Lines(Vector3[] points) {
            for (int i = 0; i < points.Length - 1; i++) {
                Gizmos.DrawLine(points[i], points[i+1]);
            }
        }
    }
}
