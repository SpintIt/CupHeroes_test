using UnityEngine;

public static class ColliderExtensions
{
    public static int LayerValue(this Collider collider)
    {
        return LayerMask.GetMask(LayerMask.LayerToName(collider.gameObject.layer));
    }
}
