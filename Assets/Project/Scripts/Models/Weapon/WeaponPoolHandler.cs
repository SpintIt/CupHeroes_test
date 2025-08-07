using System.Linq;
using UnityEngine;

public class WeaponPoolHandler : Pool<Weapon>
{
    public WeaponPoolHandler(Weapon prefab, Transform parent, int preloadCount)
        : base(() => Preload(prefab, parent), GetAction, ReturnAction, preloadCount)
    { }

    public static Weapon Preload(Weapon prefab, Transform parent)
    {
        Weapon weapon = Object.Instantiate(prefab);
        weapon.transform.parent = parent;

        return weapon;
    }

    public static void GetAction(Weapon @object) => @object.Show();
    public static void ReturnAction(Weapon @object) => @object.Hide();

    public override Weapon GetFirst()
    {
        Weapon weapon = Queue.FirstOrDefault(ball => ball.gameObject.activeSelf == false);
        weapon.transform.localPosition = Vector3.zero;
        return weapon;
    }
}
