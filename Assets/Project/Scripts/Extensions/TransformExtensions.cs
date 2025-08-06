using UnityEngine;

public static class TransformExtensions
{
    public static bool TryGetComponentInParent<TComponent>(this Transform transform, out TComponent contract)
    {
        contract = transform.GetComponentInParent<TComponent>(true);
        return contract != null;
    }

    public static void DestroyChildren(this Transform transform)
    {
        if (transform != null && transform.childCount > 0)
            for (var i = transform.childCount - 1; i >= 0; i--)
                Object.Destroy(transform.GetChild(i).gameObject);
    }

    public static void Reset(this Transform transform)
    {
        transform.localPosition = Vector3.zero;
        transform.localRotation = Quaternion.identity;
        transform.localScale = Vector3.one;
    }
}
