public class BallSpawner
{
    private BallPoolHandler _signalBallHandler;

    public BallSpawner(BallPoolHandler signalBallHandler)  // inteface
    {
        _signalBallHandler = signalBallHandler;
    }

    public Ball Spawn()
    { 
        Ball ball = _signalBallHandler.Get();
        return ball.SetColor(Utilits.GetRandomEnum<BallColorType>());
    }
}