using Sirenix.OdinInspector;
using UnityEngine;

namespace FunnyBlox
{
    public enum EGameState
    {
        Starting = 0,
        Menu = 1,
        GamePlay = 2,
        GameWait = 3,
        GameDelay = 4,
        GameAfterDelay = 5,
        Pause = 6,
        GameOver = 7,
    }

    public enum EPlayerState
    {
        Idle = 0,
        Walk = 1,
        Dance = 2,
        Dead = 3,
    }

    public enum EEnemyState
    {
        Idle,
        Patrol,
        Attack
    }

    public class CommonData
    {
        public const string PLAYERPREFS_SAVED_DATA = "PlayerPrefs_Saved_Data";
        public static SavedData SavedData;

        public static EPlayerState PlayerState;

        public static int LevelTime;

        public static void LoadDataFromPrefs()
        {
            SavedData = SaveManager.Load<SavedData>(PLAYERPREFS_SAVED_DATA);
            if (SavedData == null)
                SavedData = new SavedData();
        }

        public static void SaveDataToPrefs()
        {
            SaveManager.Save(PLAYERPREFS_SAVED_DATA, SavedData);
        }
    }
}