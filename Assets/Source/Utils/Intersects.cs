using UnityEngine;

namespace VertexFragment
{
    public static class Intersects
    {
        /// <summary>
        /// Returns if the specified box and circle intersect.
        /// </summary>
        /// <param name="boxMinX"></param>
        /// <param name="boxMaxX"></param>
        /// <param name="boxMinY"></param>
        /// <param name="boxMaxY"></param>
        /// <param name="circleX"></param>
        /// <param name="circleY"></param>
        /// <param name="circleRadius"></param>
        /// <returns></returns>
        public static bool BoxCircle(float boxMinX, float boxMaxX, float boxMinY, float boxMaxY, float circleX, float circleY, float circleRadius)
        {
            float closestX = Mathf.Clamp(circleX, boxMinX, boxMaxX);
            float closestY = Mathf.Clamp(circleY, boxMinY, boxMaxY);

            float distanceX = (circleX - closestX);
            float distanceY = (circleY - closestY);

            float distanceSquared = (distanceX * distanceX) + (distanceY * distanceY);

            return (distanceSquared < (circleRadius * circleRadius));
        }
    }
}
