public class WeaponSpawner
{
    private WeaponPoolHandler _weaponPoolHandler;

    public WeaponSpawner(WeaponPoolHandler weaponPollHandler)
    {
        _weaponPoolHandler = weaponPollHandler;
    }

    public Weapon Spawn()
    {
        Weapon weapon = _weaponPoolHandler.Get();
        return weapon/*.SetColor(Utilits.GetRandomEnum<BallColorType>())*/;
    }
}