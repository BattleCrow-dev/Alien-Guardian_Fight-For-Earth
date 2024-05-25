public class GlobalConfigurationVariables
{
    public const float PLAYER_MAX_HEALTH = 100f;
    public const float PLAYER_DAMAGE = 2.5f;
    public const float PLAYER_HEART_HEAL = 25f;
    public const float PLAYER_RELOADING_TIME = 1.5f;
    public const int PLAYER_MAX_BULLETS = 10;

    public const float PATROL_SPIKE_MAX_HEALTH = PLAYER_DAMAGE * 4;
    public const float PATROL_SPIKE_DAMAGE = PLAYER_MAX_HEALTH / 100;

    public const float WAIT_ENEMY_MAX_HEALTH = PLAYER_DAMAGE * 6;
    public const float WAIT_ENEMY_DAMAGE = PLAYER_MAX_HEALTH / 50;

    public const float FLY_ENEMY_MAX_HEALTH = PLAYER_DAMAGE * 2;
    public const float FLY_ENEMY_DAMAGE = PLAYER_MAX_HEALTH / 50;

    public const float SHOOT_ENEMY_MAX_HEALTH = PLAYER_DAMAGE * 4;
    public const float SHOOT_ENEMY_DAMAGE = PLAYER_MAX_HEALTH / 10;

    public const float SPIKES_DAMAGE = PLAYER_MAX_HEALTH / 50;

    public const float WATER_DAMAGE = PLAYER_MAX_HEALTH / 25;

    public const int COIN_POINTS_VALUE = 10;
    public const int DIAMOND_POINTS_VALUE = 100;
    public const int ENEMY_POINTS_VALUE = 25;
}
