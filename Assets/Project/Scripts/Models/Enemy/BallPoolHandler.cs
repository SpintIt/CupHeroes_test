using System.Linq;
using UnityEngine;

public class BallPoolHandler : Pool<Ball>
{
    public BallPoolHandler(Ball prefab, Transform parent, int preloadCount)
        : base(() => Preload(prefab, parent), GetAction, ReturnAction, preloadCount)
    { }

    public static Ball Preload(Ball prefab, Transform parent)
    {
        Ball ball = Object.Instantiate(prefab);
        ball.transform.parent = parent;

        return ball;
    }

    public static void GetAction(Ball @object) => @object.Show();
    public static void ReturnAction(Ball @object) => @object.Hide();

    public override Ball GetFirst()
        => Queue.FirstOrDefault(ball => ball.gameObject.activeSelf == false/* && signal.IsActive == false*/);
}
