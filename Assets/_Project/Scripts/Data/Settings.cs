using Sirenix.OdinInspector;
using UnityEngine;

namespace FunnyBlox
{
    [CreateAssetMenu(fileName = "Settings", menuName = "Data/Settings", order = 52)]
    public class Settings : SingletonScriptable<Settings>
    {
        [FoldoutGroup("Настройки игры"), GUIColor("#7FFFD4")] [LabelText("Количество уровней")]
        public int AmountLevels = 3;
        
        [FoldoutGroup("Настройки игрока"), GUIColor("#7FFFD4")] [LabelText("Скорость передвижения")]
        public float PlayerSpeed = 1f;
    }
}