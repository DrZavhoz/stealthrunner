using System;
using UnityEngine;

namespace FunnyBlox
{
    public class EventsService
    {
        /// <summary>
        /// Начало игры
        /// </summary>
        public static event Action OnGameStart;

        public static void GameStart()
        {
            OnGameStart?.Invoke();
        }

        /// <summary>
        /// Конец игры
        /// </summary>
        public static event Action OnGameWin;

        public static void GameWin()
        {
            OnGameWin?.Invoke();
        }
        
        /// <summary>
        /// Конец игры
        /// </summary>
        public static event Action OnGameOver;

        public static void GameOver()
        {
            OnGameOver?.Invoke();
        }

        /// <summary>
        /// Сброс уровня перед загрузкой нового
        /// </summary>
        public static event Action OnGameRestart;

        public static void GameRestart()
        {
            OnGameRestart?.Invoke();
        }

        /// <summary>
        /// Продолженгие игры
        /// </summary>
        public static event Action OnGameContinue;

        public static void GameContinue()
        {
            OnGameContinue?.Invoke();
        }

        //---------------------- СОБЫТИЯ ИГРОКА ----------------------
        
        /// <summary>
        /// Двигаем игрока
        /// </summary>
        public static event Action<Vector3> OnPlayerToStartPosition;

        public static void PlayerToStartPosition(Vector3 value)
        {
            OnPlayerToStartPosition?.Invoke(value);
        }
        
        /// <summary>
        /// Двигаем игрока
        /// </summary>
        public static event Action<float, float> OnPlayerTurn;

        public static void PlayerTurn(float value1, float value2)
        {
            OnPlayerTurn?.Invoke(value1, value2);
        }
        
        /// <summary>
        /// Игрок стоит
        /// </summary>
        public static event Action OnPlayerStay;
        public static void PlayerStay()
        {
            OnPlayerStay?.Invoke();
        }
        
        /// <summary>
        /// Игрок пошёл
        /// </summary>
        public static event Action OnPlayerWalk;

        public static void PlayerWalk()
        {
            OnPlayerWalk?.Invoke();
        }

        /// <summary>
        /// Игрок убит
        /// </summary>
        public static event Action OnPlayerDeath;

        public static void PlayerDeath()
        {
            OnPlayerDeath?.Invoke();
        }
        
        /// <summary>
        /// Игрок финишировал
        /// </summary>
        public static event Action OnPlayerFinished;

        public static void PlayerFinished()
        {
            OnPlayerFinished?.Invoke();
        }
    }
}