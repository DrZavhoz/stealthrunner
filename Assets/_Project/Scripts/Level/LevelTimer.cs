using System;
using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace FunnyBlox
{
    public class LevelTimer : MonoBehaviour
    {
        [SerializeField] private Slider timerSlider;
        [SerializeField] private TMP_Text timerLabel;
        private int totalTime;

        private void OnEnable()
        {
            EventsService.OnPlayerToStartPosition += OnPlayerToStartPosition;
            EventsService.OnGameStart += OnGameStart;
            EventsService.OnPlayerFinished += OnPlayerFinished;
            EventsService.OnGameOver += StopTimer;
        }

        private void OnDisable()
        {
            EventsService.OnPlayerToStartPosition -= OnPlayerToStartPosition;
            EventsService.OnGameStart -= OnGameStart;
            EventsService.OnPlayerFinished -= OnPlayerFinished;
            EventsService.OnGameOver -= StopTimer;
        }

        private void OnPlayerToStartPosition(Vector3 value)
        {
            timerSlider.value = 1f;
            totalTime = CommonData.LevelTime;
            timerLabel.text = IntToTime(totalTime);
        }

        private void OnGameStart()
        {
            StartCoroutine(LevelTimeRoutine());
        }

        private IEnumerator LevelTimeRoutine()
        {
            WaitForSeconds wfs = new WaitForSeconds(1f);

            do
            {
                yield return wfs;
                CommonData.LevelTime--;
                timerLabel.text = IntToTime(CommonData.LevelTime);
                timerSlider.value = (float)CommonData.LevelTime / totalTime;
            } while (CommonData.LevelTime > 0f);

            EventsService.GameOver();
        }

        private void OnPlayerFinished()
        {
            StopTimer();

            if (CommonData.SavedData.BestTime == 0 || CommonData.SavedData.BestTime < CommonData.LevelTime)
            {
                CommonData.SavedData.BestTime = CommonData.LevelTime;
                CommonData.SaveDataToPrefs();
            }
        }

        private void StopTimer()
        {
            StopAllCoroutines();
        }

        private string IntToTime(int value)
        {
            TimeSpan timeSpan = TimeSpan.FromSeconds(value);
            int minutes = timeSpan.Minutes;
            int seconds = timeSpan.Seconds;
            return string.Format("{0:00}:{1:00}", minutes, seconds);
        }
    }
}