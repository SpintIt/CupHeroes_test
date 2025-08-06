using System.Collections.Generic;
using UnityEngine;

public static class GameObjectExtensions
{
    public static void SetActiveEx(this GameObject component, bool active)
    {
        component.gameObject.SetActive(active);
    }

    public static void Show(this GameObject component)
    {
        component.gameObject.SetActive(true);
    }

    public static void Hide(this GameObject component)
    {
        component.gameObject.SetActive(false);
    }

    public static void Show(this List<GameObject> listComponent)
    {
        for (var i = listComponent.Count - 1; i >= 0; i--)
            listComponent[i].Show();
    }
}