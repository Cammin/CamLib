using System.Collections.Generic;
using UnityEngine;

namespace CamLib
{
    public static class Physics2DUtil
    {
        public static RaycastHit2D BoxColliderCastNonAlloc(BoxCollider2D coll, Vector2 dir)
        {
            Bounds boxBounds = coll.bounds;
            return Physics2D.BoxCast(boxBounds.center, boxBounds.size, 0, dir);
        }
        public static List<Collider2D> OverlapBoxCollider(BoxCollider2D coll, Vector2 offset, ContactFilter2D filter)
        {
            List<Collider2D> overlapColls = new List<Collider2D>();
            Bounds boxBounds = coll.bounds;
            Physics2D.OverlapBox((Vector2)boxBounds.center + offset, boxBounds.size, 0, filter, overlapColls);
            return overlapColls;
        }
    }
}
