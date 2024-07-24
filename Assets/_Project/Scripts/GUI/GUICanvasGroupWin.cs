using System;
using TMPro;
using UnityEngine;

namespace FunnyBlox.GUI
{
    public class GUICanvasGroupWin : GUICanvasGroup
    {
        [SerializeField] private TMP_Text timerBestLabel;
        [SerializeField] private TMP_Text timerResultLabel;


        public override void Show()
        {
            base.Show();

            timerBestLabel.text = IntToTime(CommonData.SavedData.BestTime);
            timerResultLabel.text = IntToTime(CommonData.LevelTime);
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