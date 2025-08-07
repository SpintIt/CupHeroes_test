public class EnemySpawner
{
    private EnemyPoolHandler _enemyPoolHandler;

    public EnemySpawner(EnemyPoolHandler enemyPollHandler)
    {
        _enemyPoolHandler = enemyPollHandler;
    }

    public Enemy Spawn()
    {
        Enemy enemy = _enemyPoolHandler.Get();
        return enemy/*.SetColor(Utilits.GetRandomEnum<BallColorType>())*/;
    }
}