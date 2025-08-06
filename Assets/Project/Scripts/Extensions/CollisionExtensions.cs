using UnityEngine;

public static class CollisionExtensions
{
    public static int LayerValue(this Collision collision)
    {
        return LayerMask.GetMask(LayerMask.LayerToName(collision.gameObject.layer));
    }
}