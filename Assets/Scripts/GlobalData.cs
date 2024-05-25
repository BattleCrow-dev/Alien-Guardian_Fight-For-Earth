public static class GlobalData
{
    public static int COINS_GAINED_COUNT = 0;
    public static int DIAMOND_GAINED_COUNT = 0;
    public static int ENEMY_KILLED_COUNT = 0;

    public static int FINAL_POINTS = 0;

    public static void EraseData()
    {
        COINS_GAINED_COUNT = 0;
        DIAMOND_GAINED_COUNT = 0;
        ENEMY_KILLED_COUNT = 0;
        FINAL_POINTS = 0;
    }
}
