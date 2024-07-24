using System;
using TMPro;
using UnityEngine;

namespace FunnyBlox.GUI
{
    public class LevelCounter : MonoBehaviour
    {

        [SerializeField] private TMP_Text levelNumberLabel;
        
        private void OnEnable()
        {
            EventsService.OnGameRestart += SetupLevelNumber;
        }

        private void OnDisable()
        {
            EventsService.OnGameRestart -= SetupLevelNumber;
        }

        private void Start()
        {
            SetupLevelNumber();
        }

        private void SetupLevelNumber()
        {
            levelNumberLabel.text =string.Format("Level {0}", CommonData.SavedData.LevelNumber + 1);
        }
    }
}