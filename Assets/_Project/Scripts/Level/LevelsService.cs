using System;
using UnityEngine;

namespace FunnyBlox
{
    public class LevelsService : MonoBehaviour
    {
        [SerializeField] private GameObject level;

        private void OnEnable()
        {
            EventsService.OnGameRestart += UnloadLevel;
            EventsService.OnGameWin += OnGameWin;
        }

        private void OnDisable()
        {
            EventsService.OnGameRestart -= UnloadLevel;
            EventsService.OnGameWin -= OnGameWin;
        }

        private void Start()
        {
            LoadLevel();
        }

        private void LoadLevel()
        {
            string path = string.Format("Levels/Level_{0:00}", CommonData.SavedData.LevelNumber);
            level = Instantiate(Resources.Load<GameObject>(path));
        }

        private void UnloadLevel()
        {
            Destroy(level);
            LoadLevel();
        }

        private void OnGameWin()
        {
            CommonData.SavedData.LevelNumber++;
            if (CommonData.SavedData.LevelNumber >= Settings.i.AmountLevels)
                CommonData.SavedData.LevelNumber = 0;
            CommonData.SaveDataToPrefs();
        }
    }
}