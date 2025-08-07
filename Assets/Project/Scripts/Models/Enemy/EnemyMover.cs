using UnityEngine;

public class EnemyMover
{
    private Enemy _enemy;
    private SpeedMove _speedMove;

    public EnemyMover(SpeedMove speedMove, Enemy enemy)
    { 
        _speedMove = speedMove;
        _enemy = enemy;
    }

    public void Handler()
    {
        _enemy.transform.Translate(Vector2.left * _speedMove.Value * Time.deltaTime);
    }
}