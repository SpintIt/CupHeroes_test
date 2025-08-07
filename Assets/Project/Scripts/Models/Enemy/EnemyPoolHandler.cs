using System.Linq;
using UnityEngine;

public class EnemyPoolHandler : Pool<Enemy>
{
    public EnemyPoolHandler(Enemy prefab, Transform parent, int preloadCount)
        : base(() => Preload(prefab, parent), GetAction, ReturnAction, preloadCount)
    { }

    public static Enemy Preload(Enemy prefab, Transform parent)
    {
        Enemy enemy = Object.Instantiate(prefab);
        enemy.transform.parent = parent;
        enemy.transform.localPosition = Vector3.zero;

        return enemy;
    }

    public static void GetAction(Enemy @object) => @object.Show();
    public static void ReturnAction(Enemy @object) => @object.Hide();

    public override Enemy GetFirst()
        => Queue.FirstOrDefault(enemy => enemy.gameObject.activeSelf == false && (enemy.Health == null || enemy.Health.IsAlive == false));
}
