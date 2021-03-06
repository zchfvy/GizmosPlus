using System.Linq;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

namespace Zchfvy.Plus {
    /// <summary>
    /// Base class for drawing gizmos.
    /// </summary>
    public static class GizmosPlus {
        /// <summary>
        /// Creates three intersecting lines on the X,Y,Z axis crossing at a
        /// point.
        /// @image html cross.png
        /// </summary>
        /// <param name="position">Location in the game world to place the cross.</param>
        /// <param name="size">Size of the cross.</param>
        public static void Cross(Vector3 position, float size) {
            float d = size/2.0f;
            Vector3 p = position;
            Gizmos.DrawLine(p + Vector3.left*d, p + Vector3.right*d);
            Gizmos.DrawLine(p + Vector3.up*d, p + Vector3.down*d);
            Gizmos.DrawLine(p + Vector3.back*d, p + Vector3.forward*d);
        }

        /// <summary>
        /// Creates a wireframe octahedron at a point in space.
        /// @image html wireocto.png
        /// </summary>
        /// <param name="position">Location in the game world to place the octahedron.</param>
        /// <param name="size">Size of the octahedron.</param>
        public static void WireOcto(Vector3 position, float size) {
            Gizmos.DrawWireMesh(octahedron, position, Quaternion.identity, Vector3.one * size);
            return;
        }

        /// <summary>
        /// Creates a solid octahedron at a point in space.
        /// @image html octo.png
        /// </summary>
        /// <param name="position">Location in the game world to place the octahedron.</param>
        /// <param name="size">Size of the octahedron.</param>
        public static void Octo(Vector3 position, float size) {
            Gizmos.DrawMesh(octahedron, position, Quaternion.identity, Vector3.one * size);
            return;
        }

        /// <summary>
        /// Creates an arrow originating from a point and pointing in a certian
        /// direction with a certain length.
        /// @image html arrow.png
        /// </summary>
        /// <param name="origin">The point the arrow's base is at.</param>
        /// <param name="dirMagnitude">
        /// A vector representing both the direction and length of the arrow.
        /// </param>
        /// <param name="headSize">
        /// Arrowhead size, as a fraction of the arrow total size, or as an
        /// absolute size if absHeadSize is set to true.
        /// Optional. Default 0.1.
        /// </param>
        /// <param name="absHeadSize">
        /// If set to true, then headSize is treated as an absolute size, if
        /// false then headSize is a fraction of the toal arrow size.
        /// </param>
        public static void Arrow(Vector3 origin, Vector3 dirMagnitude, 
                                 float headSize = 0.1f, bool absHeadSize = false) {
            if (!absHeadSize) {
                headSize *= dirMagnitude.magnitude;
            }

            Vector3 end = origin + dirMagnitude;
            var (left, up) = GetComponentsFromNormal(dirMagnitude);
            // Shaft
            Gizmos.DrawLine(origin, end);
            // 4 Arrowhead sides
            Vector3 arrowBase = end - dirMagnitude.normalized * headSize;
            Gizmos.DrawLine(end, arrowBase + left * headSize);
            Gizmos.DrawLine(end, arrowBase + up * headSize);
            Gizmos.DrawLine(end, arrowBase - left * headSize);
            Gizmos.DrawLine(end, arrowBase - up * headSize);
            // 2 Arrowhed bases
            Gizmos.DrawLine(arrowBase + left * headSize, arrowBase - left * headSize);
            Gizmos.DrawLine(arrowBase + up * headSize, arrowBase - up * headSize);
        }

        /// <summary>
        /// Construct a square in space
        /// @image html square.png
        /// </summary>
        /// <param name="origin">The center point of the square</param>
        /// <param name="normalSize">
        /// The normal and size of the square, each side of the square will have
        /// a length equivalent to this vector's magnitude.
        /// </param>
        /// <param name="crossed">
        /// Optional, if true a cross will be drawn through opposite borders of
        /// the square.
        /// </param>
        public static void Square(Vector3 origin, Vector3 normalSize,
                                     bool crossed=false) {
            float size = normalSize.magnitude;
            Rectangle(origin, normalSize, size, size, crossed);
        }

        /// <summary>
        /// Construct a rectangle in space
        /// @image html rectangle.png
        /// </summary>
        /// <param name="origin">The center point of the rectangle</param>
        /// <param name="normal"> The normal of the rectangle.</param>
        /// <param name="width">The width of the rectangle.</param>
        /// <param name="height">The height of the rectangle.</param>
        /// <param name="crossed">
        /// Optional, if true a cross will be drawn through opposite borders of
        /// the rectangle.
        /// </param>
        public static void Rectangle(Vector3 origin, Vector3 normal,
                                     float width, float height,
                                     bool crossed=false) {
            var (left, up) = GetComponentsFromNormal(normal);

            Gizmos.DrawLine(origin + up * height + left * width, origin + up * height - left * width);
            Gizmos.DrawLine(origin + up * height - left * width, origin - up * height - left * width);
            Gizmos.DrawLine(origin - up * height - left * width, origin - up * height + left * width);
            Gizmos.DrawLine(origin - up * height + left * width, origin + up * height + left * width);

            if (crossed) {
                Gizmos.DrawLine(origin - up * height - left * width, origin + up * height + left * width);
                Gizmos.DrawLine(origin - up * height + left * width, origin + up * height - left * width);
            }
        }

        /// <summary>
        /// Construct a circle in space
        /// @image html circle.png
        /// </summary>
        /// <param name="origin">The center point of the circle</param>
        /// <param name="normalRadius">
        /// The normal and radius of the circle, the radius of the circle will be
        /// equivalent to this vector's magnitude.
        /// </param>
        /// <param name="segments">
        /// Optional, the number of segments to construct the circle out of.
        /// Defaults to 32.
        /// </param>
        public static void Circle(Vector3 origin, Vector3 normalRadius,
                                  int segments=32) {
            var (left, up) = GetComponentsFromNormal(normalRadius);
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

        /// <summary>
        /// Construct a cylinder in space
        /// @image html cylinder.png
        /// </summary>
        /// <param name="origin">The center point of the cylinder</param>
        /// <param name="radius">The radius of the cylinder.</param>
        /// <param name="height">The height of the cylinder.</param>
        /// <param name="segments">
        /// Optional, the number of segments to construct the cylinder out of.
        /// Defaults to 16.
        /// </param>
        public static void WireCylinder(Vector3 origin, float radius, float height,
                                        int segments=16) {
            // Construct by hand each time instead of from mesh because we want
            // to use a non-tessalated mesh.
            Vector3 top = origin + Vector3.up * height / 2f;
            Vector3 bot = origin - Vector3.up * height / 2f;

            for (int i = 0; i < segments; i++) {
                float theta0 = 2f * Mathf.PI * (float) i / segments;
                float theta1 = 2f * Mathf.PI * (float) (i+1) / segments;

                float x0 = radius * Mathf.Cos(theta0);
                float y0 = radius * Mathf.Sin(theta0);
                float x1 = radius * Mathf.Cos(theta1);
                float y1 = radius * Mathf.Sin(theta1);

                Vector3 left = Vector3.left;
                Vector3 fore = Vector3.forward;
                // Top circle
                Gizmos.DrawLine(
                        top + left * x0 + fore * y0,
                        top + left * x1 + fore * y1);
                // Bottom circle
                Gizmos.DrawLine(
                        bot + left * x0 + fore * y0,
                        bot + left * x1 + fore * y1);
                // Sides
                Gizmos.DrawLine(
                        top + left * x0 + fore * y0,
                        bot + left * x0 + fore * y0);
            }
        }

        /// <summary>
        /// Draw a series of connected line segments.
        /// @image html lines.png
        /// </summary>
        /// <param name="points">The points to draw the segments through</param>
        public static void Lines(Vector3[] points) {
            for (int i = 0; i < points.Length - 1; i++) {
                Gizmos.DrawLine(points[i], points[i+1]);
            }
        }

        /// <summary>
        /// Draw a shaded convex polygon
        /// @image html polygon.png
        /// </summary>
        /// <param name="points">The points of the polygon</param>
        public static void Polygon(Vector3[] points) {
            _polygonInner(points);
            _polygonInner(points.Reverse().ToArray());
        }
        private static void _polygonInner(Vector3[] points) {
            Mesh m = new Mesh();
            m.SetVertices(points.ToList()); 
            m.SetNormals(points.Select(p=> Vector3.up).ToList());

            var triList = new System.Collections.Generic.List<int[]>();
            for (int i = 2; i < points.Length; i++) {
                triList.Add(new int[]{0, i-1, i});
            }

            m.SetTriangles(
                    triList.SelectMany(tl => tl.ToList()).ToArray(),
                    0);

            Gizmos.DrawMesh(m, Vector3.zero, Quaternion.identity, Vector3.one);
        }

        /// <summary>
        /// Calculates the component left and up vectors in the plane perpendicular to the
        /// specified normal.
        /// </summary>
        ///
        /// <param name="normal">A normalized vector perpendicular to the desired plane.</param>
        ///
        /// <returns>
        /// A pair of vectors perpendicular to each other and contained within the plain
        /// that is perpendicular to the given normal vector.
        /// </returns>
        private static (Vector3 left, Vector3 up) GetComponentsFromNormal(Vector3 normal) {
            Vector3 left = Vector3.Cross(normal, Vector3.up).normalized;
            Vector3 up = Vector3.Cross(left, normal).normalized;

            // Handle the case where the normal is directly up or down. In that case the
            // cross product used to calculate the left and up vectors will be 0-length,
            // causing the circle to not draw correctly. To avoid this, we manually
            // specify the left and up vectors so that the circle will draw correctly in
            // the X-Z plane.
            if (Mathf.Approximately(left.sqrMagnitude, 0f))
            {
                left = Vector3.left;
                up = Vector3.forward;
            }

            return (left, up);
        }

        public enum TextAnchor {
            Left,
            Center,
            Right
        }
        /// <summary>
        /// Draw text in space
        /// </summary>
        /// <param name="origin">The point to draw the text from</param>
        /// <param name="text">The text to draw</param>
        /// <param name="anchor">How to align the text, default center</param>
        public static void Text ( Vector3 origin, string text, TextAnchor anchor=TextAnchor.Center) {
            textViaIcon(origin, text, anchor);
        }

        // const int TEXT_ATLAS_WIDTH = 15;
        // const int TEXT_ATLAS_HEIGHT = 7;
        // const int TEXT_ATLAS_PIXELS = 512;
        // const int TEXT_CHARACTER_WIDTH = 33;
        // const int TEXT_CHARACTER_HEIGHT = 72;
        // const float TEXT_ASPECT_RATIO = 7f / 15f;
        // const string TEXT_TEXTURE_PATH = "Packages/com.zchfvy.GizmosPlus/Textures/TextAtlas.png";
        // private static Texture textTexture;
        // private static void textViaGuiTexture(Vector3 origin, string text, float size) {
        //     // load texture
        //     if (textTexture == null) {
        //         // TODO : load in static constructor
        //         textTexture = AssetDatabase.LoadAssetAtPath(TEXT_TEXTURE_PATH, typeof(Texture2D)) as Texture2D;
        //     }

        //     var point = Camera.current.WorldToScreenPoint(origin);
        //     float x = point.x;
        //     float y = point.y;
        //     for (int i = 0; i < text.Length; i++) {
        //         int c = (int)text[i];
        //         if (c < 32 || c > 126) {
        //             // We have an unprintable character, set it to
        //             // The "REPLACEMENT CHARACTER"
        //             c = 127;
        //         }
        //         int val = c-31; // ASCII offset for skipping unprintables
        //         int c_ox = val % TEXT_ATLAS_WIDTH;
        //         int c_oy = (int)(val / TEXT_ATLAS_WIDTH);

        //         int tx_ox = c_ox * TEXT_CHARACTER_WIDTH;
        //         int tx_oy = c_oy * TEXT_CHARACTER_HEIGHT;

        //         Gizmos.DrawGUITexture(
        //                 new Rect(x, y, size*TEXT_ASPECT_RATIO, size),
        //                 textTexture,
        //                 tx_ox,
        //                 tx_oy,
        //                 TEXT_ATLAS_PIXELS + TEXT_CHARACTER_WIDTH - tx_ox,
        //                 TEXT_ATLAS_PIXELS + TEXT_CHARACTER_HEIGHT - tx_oy);


        //     }
        // }

        const int CHAR_WORLD_OFFSET = 16;
        const string TEXT_ICONS_ROOT = "Packages/com.zchfvy.GizmosPlus/Textures/";
        private static Dictionary<int,string> textureIcons;
        private const string VALID_CHARS = " !\"#$%&'()*+,-./0123456789:;<=>?@ABCDEFGHIJKLMNOPQRSTUVWXYZ[\\]^_`abcdefghijklmnopqrstuvwxyz{|}~";
        private static void textViaIcon(Vector3 origin, string text, TextAnchor anchor) {
            // load texture
            if (textureIcons == null) {
                textureIcons = new Dictionary<int, string>();
                int idx = 0;
                foreach (char c in VALID_CHARS) {
                    textureIcons.Add(idx, TEXT_ICONS_ROOT + idx + ".png");
                    idx++;
                }
            }

            var screenBase = Camera.current.WorldToScreenPoint(origin);
            float offset = 0;
            if (anchor == TextAnchor.Center) {
                offset = (float)text.Length * -0.5f;
            }
            else if (anchor == TextAnchor.Right) {
                offset = (float)text.Length * -1.0f;
            }
            for (int i = 0; i < text.Length; i++) {
                int c = (int)text[i];
                if (c < 32 || c > 126) {
                    // We have an unprintable character, set it to
                    // The "REPLACEMENT CHARACTER"
                    c = 127;
                }
                int val = c-32; // ASCII offset for skipping unprintables
                var screenPoint = screenBase + Vector3.right * offset * CHAR_WORLD_OFFSET;
                var worldPoint = Camera.current.ScreenToWorldPoint(screenPoint);
                Gizmos.DrawIcon(worldPoint, textureIcons[val], false);
                offset += 1;
            }
        }

        private static Mesh _octahedron = null;
        private static Mesh octahedron {
            get {
                if (_octahedron == null) {
                    Mesh m = new Mesh();

                    m.SetVertices(new System.Collections.Generic.List<Vector3> {
                            // F1 : 0 - 2
                            new Vector3( 0,  1,  0),
                            new Vector3( 1,  0,  0),
                            new Vector3( 0,  0,  1),
                            // F2 : 3 - 5
                            new Vector3( 0,  1,  0),
                            new Vector3( 0,  0,  1),
                            new Vector3(-1,  0,  0),
                            // F3 : 6 - 8
                            new Vector3( 0,  1,  0),
                            new Vector3(-1,  0,  0),
                            new Vector3( 0,  0, -1),
                            // F4 : 9 - 11
                            new Vector3( 0,  1,  0),
                            new Vector3( 0,  0, -1),
                            new Vector3( 1,  0,  0),
                            // F5 : 12 - 14
                            new Vector3( 0, -1,  0),
                            new Vector3( 1,  0,  0),
                            new Vector3( 0,  0,  1),
                            // F6 : 15 - 17
                            new Vector3( 0, -1,  0),
                            new Vector3( 0,  0,  1),
                            new Vector3(-1,  0,  0),
                            // F7 : 18 - 20
                            new Vector3( 0, -1,  0),
                            new Vector3(-1,  0,  0),
                            new Vector3( 0,  0, -1),
                            // F8 : 21 - 23
                            new Vector3( 0, -1,  0),
                            new Vector3( 0,  0, -1),
                            new Vector3( 1,  0,  0),
                            }); 

                    m.SetNormals(new System.Collections.Generic.List<Vector3> {
                            // F1 : 0 - 2
                            new Vector3( 1,  1,  1),
                            new Vector3( 1,  1,  1),
                            new Vector3( 1,  1,  1),
                            // F2 : 3 - 5
                            new Vector3(-1,  1,  1),
                            new Vector3(-1,  1,  1),
                            new Vector3(-1,  1,  1),
                            // F3 : 6 - 8
                            new Vector3(-1,  1, -1),
                            new Vector3(-1,  1, -1),
                            new Vector3(-1,  1, -1),
                            // F4 : 9 - 11
                            new Vector3( 1,  1, -1),
                            new Vector3( 1,  1, -1),
                            new Vector3( 1,  1, -1),
                            // F5 : 12 - 14
                            new Vector3( 1, -1,  1),
                            new Vector3( 1, -1,  1),
                            new Vector3( 1, -1,  1),
                            // F6 : 15 - 17
                            new Vector3(-1, -1,  1),
                            new Vector3(-1, -1,  1),
                            new Vector3(-1, -1,  1),
                            // F7 : 18 - 20
                            new Vector3(-1, -1, -1),
                            new Vector3(-1, -1, -1),
                            new Vector3(-1, -1, -1),
                            // F8 : 21 - 23
                            new Vector3( 1, -1, -1),
                            new Vector3( 1, -1, -1),
                            new Vector3( 1, -1, -1),
                            });

                    m.SetTriangles(new int[] {
                            0,  2,  1,
                            3,  5,  4,
                            6,  8,  7,
                            9,  11, 10,
                            12, 13, 14,
                            15, 16, 17,
                            18, 19, 20,
                            21, 22, 23,
                            },  0);

                    _octahedron = m;
                }
                return _octahedron;
            }
        }
    }
}
