using System;
using UnityEngine;

namespace VertexFragment
{
    public static class MathUtils
    {
        public const float Pi2 = 6.283185f;
        public const float Sqrt2 = 1.414213f;
        public const float Epsilon = 0.0001f;

        /// <summary>
        /// Performs a "circular" mod which provides positive results on negative values. For example:<para/>
        /// 
        /// mod(-5, 5) = 0,
        /// mod(-4, 5) = 1,
        /// mod(-3, 5) = 2,
        /// mod(-2, 5) = 3,
        /// mod(-1, 5) = 4,
        /// mod( 0, 5) = 0,
        /// mod( 1, 5) = 1,
        /// mod( 2, 5) = 2,
        /// mod( 3, 5) = 3,
        /// mod( 4, 5) = 4,
        /// mod( 5, 5) = 0<para/>
        /// 
        /// It is useful for iteration where we want a safe look ahead and look behind value.<para/>
        /// 
        /// Source: https://stackoverflow.com/a/1082938/735425
        /// </summary>
        /// <param name="x"></param>
        /// <param name="mod"></param>
        /// <returns></returns>
        public static int ModCircular(int x, int mod)
        {
            return (x % mod + mod) % mod;
        }

        /// <summary>
        /// Rounds the floating value along the current direction.
        /// If positive, returns the ceiling.
        /// If negative, returns the floor.
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static float RoundAlong(float value)
        {
            return (value >= 0) ? Mathf.Ceil(value) : Mathf.Floor(value);
        }

        public static float RoundToNearestQuarter(float value)
        {
            return (float)((int)Mathf.Round(value * 4)) / 4.0f;
        }

        public static Vector3 RoundToNearestQuarter(Vector3 v)
        {
            return new Vector3(RoundToNearestQuarter(v.x), RoundToNearestQuarter(v.y), RoundToNearestQuarter(v.z));
        }

        /// <summary>
        /// Determines if the two float values are equal to each other within the range of the epsilon.
        /// </summary>
        /// <param name="a"></param>
        /// <param name="b"></param>
        /// <param name="epsilon"></param>
        /// <returns></returns>
        public static bool FloatEquals(float a, float b, float epsilon = Epsilon)
        {
            return Mathf.Abs(a - b) <= epsilon;
        }

        public static bool IsZero(float a, float epsilon = Epsilon)
        {
            return FloatEquals(a, 0.0f, epsilon);
        }

        public static bool IsZero(Vector3 v, float epsilon = Epsilon)
        {
            return (IsZero(v.x, epsilon) && IsZero(v.y, epsilon) && IsZero(v.z, epsilon));
        }

        public static bool IsOne(float a, float epsilon = Epsilon)
        {
            return FloatEquals(a, 1.0f, epsilon);
        }

        public static bool VectorEquals(Vector2 a, Vector2 b, float epsilon = Epsilon)
        {
            return FloatEquals(a.x, b.x, epsilon) && FloatEquals(a.y, b.y, epsilon);
        }

        public static bool VectorEquals(Vector3 a, Vector3 b, float epsilon = Epsilon)
        {
            return FloatEquals(a.x, b.x, epsilon) && FloatEquals(a.y, b.y, epsilon) && FloatEquals(a.z, b.z, epsilon);
        }

        public static Vector2 VectorClamp(Vector2 a, Vector2 min, Vector2 max)
        {
            return new Vector2(Mathf.Clamp(a.x, min.x, max.x), Mathf.Clamp(a.y, min.y, max.y));
        }

        public static Vector3 VectorClamp(Vector3 a, Vector3 min, Vector3 max)
        {
            return new Vector3(Mathf.Clamp(a.x, min.x, max.x), Mathf.Clamp(a.y, min.y, max.y), Mathf.Clamp(a.z, min.z, max.z));
        }

        public static bool ColorEquals(Color a, Color b, float epsilon = Epsilon)
        {
            return FloatEquals(a.r, b.r, epsilon) && FloatEquals(a.g, b.g, epsilon) && FloatEquals(a.b, b.b, epsilon) && FloatEquals(a.a, b.a, epsilon);
        }

        /// <summary>
        /// Converts Euler angles to a <see cref="Quaternion"/>.
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <param name="z"></param>
        /// <returns></returns>
        public static Quaternion EulerToQuat(float x, float y, float z)
        {
            return Quaternion.Euler(x, y, z);
        }

        /// <summary>
        /// Returns true if the specified point is in view of the camera.
        /// </summary>
        /// <param name="camera"></param>
        /// <param name="worldPoint"></param>
        /// <returns></returns>
        public static bool PointInCamera(Camera camera, Vector3 worldPoint)
        {
            Vector3 pointInCamera = camera.WorldToViewportPoint(worldPoint);

            // Viewport space is normalized with x/y [0.0, 1.0] in range and z >= 0.0 in front of camera
            return (pointInCamera.x >= 0.0 && pointInCamera.x <= 1.0) &&
                   (pointInCamera.y >= 0.0 && pointInCamera.y <= 1.0) &&
                   (pointInCamera.z >= 0.0);
        }

        /// <summary>
        /// Projects a ray from the specified camera and returns  where on the specified plane it intersects.
        /// </summary>
        /// <param name="camera"></param>
        /// <param name="screenPos"></param>
        /// <param name="yPlane"></param>
        /// <returns></returns>
        public static Vector3 ScreenPosToWorldPlane(Camera camera, Vector3 screenPos, Plane plane)
        {
            Ray ray = camera.ScreenPointToRay(screenPos);

            float distance;

            if (!plane.Raycast(ray, out distance))
            {
                distance = 0.0f;
            }

            return (ray.origin + (ray.direction * distance));
        }

        /// <summary>
        /// Performs a floor on all of the components of the provided vector.
        /// </summary>
        /// <param name="v"></param>
        /// <returns></returns>
        public static Vector3 Floor(Vector3 v)
        {
            return Floor(ref v);
        }

        /// <summary>
        /// Performs a floor on all of the components of the provided vector.
        /// </summary>
        /// <param name="v"></param>
        /// <returns></returns>
        public static Vector3 Floor(ref Vector3 v)
        {
            return new Vector3(Mathf.Floor(v.x), Mathf.Floor(v.y), Mathf.Floor(v.z));
        }

        /// <summary>
        /// Performs a floor on all of the components of the provided vector.
        /// </summary>
        /// <param name="v"></param>
        /// <returns></returns>
        public static System.Numerics.Vector3 Floor(System.Numerics.Vector3 v)
        {
            return Floor(ref v);
        }

        /// <summary>
        /// Performs a floor on all of the components of the provided vector.
        /// </summary>
        /// <param name="v"></param>
        /// <returns></returns>
        public static System.Numerics.Vector3 Floor(ref System.Numerics.Vector3 v)
        {
            return new System.Numerics.Vector3(Mathf.Floor(v.X), Mathf.Floor(v.Y), Mathf.Floor(v.Z));
        }

        /// <summary>
        /// Performs a ceil on all of the components of the provided vector.
        /// </summary>
        /// <param name="v"></param>
        /// <returns></returns>
        public static Vector3 Ceil(Vector3 v)
        {
            return new Vector3(Mathf.Ceil(v.x), Mathf.Ceil(v.y), Mathf.Ceil(v.z));
        }

        /// <summary>
        /// Returns a vector comprised of the min components of the provided vectors.
        /// </summary>
        /// <param name="a"></param>
        /// <param name="b"></param>
        /// <returns></returns>
        public static Vector3 Min(Vector3 a, Vector3 b)
        {
            return new Vector3(Mathf.Min(a.x, b.x), Mathf.Min(a.y, b.y), Mathf.Min(a.z, b.z));
        }

        /// <summary>
        /// Returns a vector comprised of the max components of the provided vectors.
        /// </summary>
        /// <param name="a"></param>
        /// <param name="b"></param>
        /// <returns></returns>
        public static Vector3 Max(Vector3 a, Vector3 b)
        {
            return new Vector3(Mathf.Max(a.x, b.x), Mathf.Max(a.y, b.y), Mathf.Max(a.z, b.z));
        }

        /// <summary>
        /// Returns the component value with the greatest value.
        /// </summary>
        /// <param name="v"></param>
        /// <returns></returns>
        public static float Max(Vector3 v)
        {
            return Mathf.Max(v.x, Mathf.Max(v.y, v.z));
        }

        /// <summary>
        /// Returns the component value with the greatest absolute value.
        /// </summary>
        /// <param name="v"></param>
        /// <returns></returns>
        public static float AbsMax(Vector3 v)
        {
            return Mathf.Max(Mathf.Abs(v.x), Mathf.Max(Mathf.Abs(v.y), Mathf.Abs(v.z)));
        }

        /// <summary>
        /// Returns a new vector where each component is the absolute value of the provided vector.
        /// </summary>
        /// <param name="v"></param>
        /// <returns></returns>
        public static Vector3 Abs(Vector3 v)
        {
            return new Vector3(Mathf.Abs(v.x), Mathf.Abs(v.y), Mathf.Abs(v.z));
        }

        /// <summary>
        /// Multiplies together two 3-component vectors since Unity does not provide this for some reason.
        /// </summary>
        /// <param name="a"></param>
        /// <param name="b"></param>
        /// <returns></returns>
        public static Vector3 Multiply(Vector3 a, Vector3 b)
        {
            return new Vector3(a.x * b.x, a.y * b.y, a.z * b.z);
        }

        /// <summary>
        /// Multiplies together two 3-component vectors since Unity does not provide this for some reason.
        /// </summary>
        /// <param name="a"></param>
        /// <param name="b"></param>
        /// <returns></returns>
        public static Vector3 Multiply(ref Vector3 a, ref Vector3 b)
        {
            return new Vector3(a.x * b.x, a.y * b.y, a.z * b.z);
        }

        /// <summary>
        /// Multiplies together two 4-component vectors since Unity does not provide this for some reason.
        /// </summary>
        /// <param name="a"></param>
        /// <param name="b"></param>
        /// <returns></returns>
        public static Vector4 Multiply(Vector4 a, Vector4 b)
        {
            return new Vector4(a.x * b.x, a.y * b.y, a.z * b.z, a.w * b.w);
        }

        /// <summary>
        /// Multiplies together two 4-component vectors since Unity does not provide this for some reason.
        /// </summary>
        /// <param name="a"></param>
        /// <param name="b"></param>
        /// <returns></returns>
        public static Vector4 Multiply(ref Vector4 a, ref Vector4 b)
        {
            return new Vector4(a.x * b.x, a.y * b.y, a.z * b.z, a.w * b.w);
        }

        /// <summary>
        /// Returns the signed distance from the point to the plane.<para/>
        /// 
        /// If the result is positive then the point is "in front" of the plane.
        /// If the result is negative then the point is "behind" the plane.
        /// If the result is zero then the point is on the plane.
        /// </summary>
        /// <param name="point"></param>
        /// <param name="planePoint"></param>
        /// <param name="planeNormal"></param>
        /// <returns></returns>
        public static float SignedDistanceToPlane(Vector3 point, Vector3 planePoint, Vector3 planeNormal)
        {
            // Remember that the dot product of an arbitrary vector and an unit vector 
            // is the length of the arbitrary vector projected onto the unit vector.
            return Vector3.Dot((point - planePoint), planeNormal);
        }

        /// <summary>
        /// Projects the arbitrary vector onto the provided normal/unit vector.
        /// </summary>
        /// <param name="vector"></param>
        /// <param name="normal"></param>
        /// <returns></returns>
        public static Vector3 ProjectVectorOntoNormal(Vector3 vector, Vector3 normal)
        {
            float length = Vector3.Dot(vector, normal);
            return (normal * length);
        }

        /// <summary>
        /// Rotates the 2D vector clockwise 90 degrees.<para/>
        /// 
        /// <c>(1, 0)</c> to <c>(0, -1)</c> to <c>(-1, 0)</c> to <c>(0, 1)</c> back to <c>(1, 0)</c>.
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <returns></returns>
        public static (float, float) Rotate90Clockwise(float x, float y)
        {
            return (y, -x);
        }

        /// <summary>
        /// Rotates the 2D vector clockwise 90 degrees.<para/>
        /// 
        /// <c>(1, 0)</c> to <c>(0, -1)</c> to <c>(-1, 0)</c> to <c>(0, 1)</c> back to <c>(1, 0)</c>.
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <returns></returns>
        public static (int, int) Rotate90Clockwise(int x, int y)
        {
            return (y, -x);
        }

        /// <summary>
        /// Rotates the 2D vector counter-clockwise 90 degrees.<para/>
        /// 
        /// <c>(1, 0)</c> to <c>(0, 1)</c> to <c>(-1, 0)</c> to <c>(0, -1)</c> back to <c>(1, 0)</c>.
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <returns></returns>
        public static (float, float) Rotate90CounterClockwise(float x, float y)
        {
            return (-y, x);
        }

        /// <summary>
        /// Rotates the 2D vector counter-clockwise 90 degrees.<para/>
        /// 
        /// <c>(1, 0)</c> to <c>(0, 1)</c> to <c>(-1, 0)</c> to <c>(0, -1)</c> back to <c>(1, 0)</c>.
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <returns></returns>
        public static (int, int) Rotate90CounterClockwise(int x, int y)
        {
            return (-y, x);
        }

        /// <summary>
        /// Rotates the given position around the specified point using the specified rotation.
        /// </summary>
        /// <param name="point">The position that is being rotated.</param>
        /// <param name="pivot">The point we are rotating around.</param>
        /// <param name="rotation">The rotation to perform.</param>
        /// <returns></returns>
        public static Vector3 RotateAroundPivot(Vector3 point, Vector3 pivot, Quaternion rotation)
        {
            return (rotation * (point - pivot) + pivot);
        }

        /// <summary>
        /// Rotates the given position around the specified point using the specified rotation.
        /// </summary>
        /// <param name="point">The position that is being rotated.</param>
        /// <param name="pivot">The point we are rotating around.</param>
        /// <param name="euler">The euler angles to rotate around.</param>
        /// <returns></returns>
        public static Vector3 RotateAroundPivot(Vector3 point, Vector3 pivot, Vector3 euler)
        {
            return RotateAroundPivot(point, pivot, Quaternion.Euler(euler));
        }

        /// <summary>
        /// Returns <c>true</c> if <c>x</c> is in the range <c>[min, max]</c>.
        /// </summary>
        /// <param name="x"></param>
        /// <param name="min"></param>
        /// <param name="max"></param>
        /// <returns></returns>
        public static bool Between(int x, int min, int max)
        {
            return (x >= min) && (x <= max);
        }

        /// <summary>
        /// Returns <c>true</c> if <c>x</c> is in the range <c>[min, max]</c>.
        /// </summary>
        /// <param name="x"></param>
        /// <param name="min"></param>
        /// <param name="max"></param>
        /// <returns></returns>
        public static bool Between(float x, float min, float max)
        {
            return (x >= min) && (x <= max);
        }

        /// <summary>
        /// Returns <c>true</c> for <c>(x, y)</c> if <c>x</c> is in the range <c>[min.x, max.x]</c> and <c>y</c> is in the range <c>[min.y, max.y]</c>.
        /// </summary>
        /// <param name="x"></param>
        /// <param name="min"></param>
        /// <param name="max"></param>
        /// <returns></returns>
        public static bool Between(Vector2 x, Vector2 min, Vector2 max)
        {
            return Between(x.x, min.x, max.x) &&
                   Between(x.y, min.y, max.y);
        }

        /// <summary>
        /// Returns <c>true</c> for <c>(x, y, z)</c> if <c>x</c> is in the range <c>[min.x, max.x]</c> and <c>y</c> is in the range <c>[min.y, max.y]</c> and <c>z</c> is in the range <c>[min.z, max.z]</c>.
        /// </summary>
        /// <param name="x"></param>
        /// <param name="min"></param>
        /// <param name="max"></param>
        /// <returns></returns>
        public static bool Between(Vector3 x, Vector3 min, Vector3 max)
        {
            return Between(x.x, min.x, max.x) &&
                   Between(x.y, min.y, max.y) &&
                   Between(x.z, min.z, max.z);
        }

        /// <summary>
        /// Takes a normalized direction vector and turns it into a cardinal direction.
        /// </summary>
        /// <param name="dir"></param>
        /// <returns></returns>
        public static Vector3 ToCardinalDirection(Vector3 dir)
        {
            if (Mathf.Abs(dir.x) > Mathf.Abs(dir.z))
            {
                if (dir.x < 0)
                {
                    return Vector3.left;
                }
                else
                {
                    return Vector3.right;
                }
            }
            else
            {
                if (dir.z < 0)
                {
                    return Vector3.back;
                }
                else
                {
                    return Vector3.forward;
                }
            }
        }

        /// <summary>
        /// Calculates the distance between two vectors.<para/>
        /// 
        /// Yes, Unity provides this functionality already but it involves making copies of the data.
        /// </summary>
        /// <param name="a"></param>
        /// <param name="b"></param>
        /// <returns></returns>
        public static float VectorDistanceSq(ref Vector3 a, ref Vector3 b)
        {
            float x = a.x - b.x;
            float y = a.y - b.y;
            float z = a.z - b.z;

            return ((x * x) + (y * y) + (z * z));
        }

        /// <summary>
        /// Calculates the distance between two vectors.<para/>
        /// 
        /// Yes, Unity provides this functionality already but it involves making copies of the data.
        /// </summary>
        /// <param name="ax"></param>
        /// <param name="ay"></param>
        /// <param name="az"></param>
        /// <param name="bx"></param>
        /// <param name="by"></param>
        /// <param name="bz"></param>
        /// <returns></returns>
        public static float VectorDistanceSq(float ax, float ay, float az, float bx, float by, float bz)
        {
            float x = ax - bx;
            float y = ay - by;
            float z = az - bz;

            return ((x * x) + (y * y) + (z * z));
        }

        public enum DotClassification
        {
            /// <summary>
            /// The vectors are inbetween one of the other general classifications.
            /// </summary>
            Transitive,

            /// <summary>
            /// The vectors are perpendicular from each other. Dot ~= 0.0
            /// </summary>
            Perpendicular,

            /// <summary>
            /// The vectors are parallel facing the same direction. Dot ~= 1.0
            /// </summary>
            Parallel,

            /// <summary>
            /// The vectors are parallel facing opposite directions. Dot ~= -1.0
            /// </summary>
            AntiParallel
        }

        /// <summary>
        /// Perfroms the dot product on the two vectors and returns a generalized classification of the vectors.
        /// Utiltiy to quickly determine are they parallel or perpendicular.
        /// </summary>
        /// <param name="a"></param>
        /// <param name="b"></param>
        /// <param name="precision"></param>
        /// <returns></returns>
        public static DotClassification DotClassify(Vector3 a, Vector3 b, float precision = Epsilon)
        {
            return DotClassify(ref a, ref b, precision);
        }

        /// <summary>
        /// Perfroms the dot product on the two vectors and returns a generalized classification of the vectors.
        /// Utiltiy to quickly determine are they parallel or perpendicular.
        /// </summary>
        /// <param name="a"></param>
        /// <param name="b"></param>
        /// <param name="precision"></param>
        /// <returns></returns>
        public static DotClassification DotClassify(ref Vector3 a, ref Vector3 b, float precision = Epsilon)
        {
            float dot = Vector3.Dot(a, b);

            if (FloatEquals(dot, -1.0f, precision))
            {
                return DotClassification.AntiParallel;
            }
            else if (FloatEquals(dot, 0.0f, precision))
            {
                return DotClassification.Perpendicular;
            }
            else if (FloatEquals(dot, 1.0f, precision))
            {
                return DotClassification.Parallel;
            }
            else
            {
                return DotClassification.Transitive;
            }
        }

        /// <summary>
        /// Returns the fractional part of the number.
        /// If you want the integer part, see <see cref="Math.Truncate"/>.
        /// </summary>
        /// <param name="v"></param>
        /// <returns></returns>
        public static float Fract(float v)
        {
            return (v - (float)Math.Truncate(v));
        }

        /// <summary>
        /// Returns the fractional part of each component of the provided vector.
        /// </summary>
        /// <param name="v"></param>
        /// <returns></returns>
        public static Vector2 Fract(Vector2 v)
        {
            return new Vector2(Fract(v.x), Fract(v.y));
        }

        /// <summary>
        /// Returns the fractional part of each component of the provided vector.
        /// </summary>
        /// <param name="v"></param>
        /// <returns></returns>
        public static Vector2 Fract(ref Vector2 v)
        {
            return new Vector2(Fract(v.x), Fract(v.y));
        }

        /// <summary>
        /// Returns the fractional part of each component of the provided vector.
        /// </summary>
        /// <param name="v"></param>
        /// <returns></returns>
        public static System.Numerics.Vector2 Fract(System.Numerics.Vector2 v)
        {
            return new System.Numerics.Vector2(Fract(v.X), Fract(v.Y));
        }

        /// <summary>
        /// Returns the fractional part of each component of the provided vector.
        /// </summary>
        /// <param name="v"></param>
        /// <returns></returns>
        public static System.Numerics.Vector2 Fract(ref System.Numerics.Vector2 v)
        {
            return new System.Numerics.Vector2(Fract(v.X), Fract(v.Y));
        }

        /// <summary>
        /// Returns the fractional part of each component of the provided vector.
        /// </summary>
        /// <param name="v"></param>
        /// <returns></returns>
        public static Vector3 Fract(Vector3 v)
        {
            return Fract(ref v);
        }

        /// <summary>
        /// Returns the fractional part of each component of the provided vector.
        /// </summary>
        /// <param name="v"></param>
        /// <returns></returns>
        public static Vector3 Fract(ref Vector3 v)
        {
            return new Vector3(Fract(v.x), Fract(v.y), Fract(v.z));
        }

        /// <summary>
        /// Returns the fractional part of each component of the provided vector.
        /// </summary>
        /// <param name="v"></param>
        /// <returns></returns>
        public static System.Numerics.Vector3 Fract(System.Numerics.Vector3 v)
        {
            return Fract(ref v);
        }

        /// <summary>
        /// Returns the fractional part of each component of the provided vector.
        /// </summary>
        /// <param name="v"></param>
        /// <returns></returns>
        public static System.Numerics.Vector3 Fract(ref System.Numerics.Vector3 v)
        {
            return new System.Numerics.Vector3(Fract(v.X), Fract(v.Y), Fract(v.Z));
        }

        public static float DistanceSq(float ax, float ay, float bx, float by)
        {
            float dx = (bx - ax);
            float dy = (by - ay);

            return (dx * dx) + (dy * dy);
        }
    }
}
