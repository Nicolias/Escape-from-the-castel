using System.Collections.Generic;
using UnityEngine;

namespace Scripts.Levels.FourCircles
{
    public class CircleItemComparer : IComparer<CircleItem>
    {
        private readonly Circle _circle;

        public CircleItemComparer(Circle circle) => _circle = circle;

        public int Compare(CircleItem firstItem, CircleItem secondItem)
        {
            float firstIItemDistance = Vector2.Distance(firstItem.Position, _circle.Position);
            float secondItemDistance = Vector2.Distance(secondItem.Position, _circle.Position);

            if (Mathf.Approximately(firstIItemDistance, secondItemDistance))
            {
                return 0;
            }

            if (firstIItemDistance > secondItemDistance)
            {
                return 1;
            }

            return -1;
        }
    }
}
