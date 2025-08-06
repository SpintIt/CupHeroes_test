using System.Collections.Generic;
using UnityEngine;

public static class ComponentExtensions
{
    public static void SetActiveEx(this Component component, bool active)
    {
        component.gameObject.SetActive(active);
    }

    public static void Show(this Component component)
    {
        component.gameObject.SetActive(true);
    }

    public static void Hide(this Component component)
    {
        component.gameObject.SetActive(false);
    }

    public static void Show(this List<Component> listComponent)
    {
        for (var i = listComponent.Count - 1; i >= 0; i--)
            listComponent[i].Show();
    }

    public static void Hide<T>(this List<T> list)
        where T : Component
    {
        foreach (var item in list)
            item.Hide();
    }

    public static void Show<T>(this List<T> list)
        where T : Component
    {
        foreach (var item in list)
            item.Show();
    }
}
