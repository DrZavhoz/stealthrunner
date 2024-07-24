using System;
using System.Collections;
using Sirenix.OdinInspector;
using UnityEngine;

namespace FunnyBlox
{
    public class PlayerController : MonoBehaviour
    {
        [LabelText("Трансформ рута игрока")] [SerializeField]
        private Transform playerRootTransform;

        [LabelText("Трансформ визуала игрока")] [SerializeField]
        private Transform playerBodyTransform;

        private float verticalValue;
        private float horizontalValue;
        private Vector3 mDirection;

        private void OnEnable()
        {
            EventsService.OnPlayerToStartPosition += OnPlayerToStartPosition;
            EventsService.OnGameStart += OnGameStart;
            EventsService.OnPlayerTurn += OnPlayerTurn;
            EventsService.OnPlayerFinished += OnPlayerFinished;
            EventsService.OnPlayerDeath += OnPlayerDeath;
            EventsService.OnGameRestart += OnGameRestart;
        }

        private void OnDisable()
        {
            EventsService.OnPlayerToStartPosition -= OnPlayerToStartPosition;
            EventsService.OnGameStart -= OnGameStart;
            EventsService.OnPlayerTurn -= OnPlayerTurn;
            EventsService.OnPlayerFinished -= OnPlayerFinished;
            EventsService.OnPlayerDeath -= OnPlayerDeath;
            EventsService.OnGameRestart -= OnGameRestart;
        }

        private void OnPlayerToStartPosition(Vector3 position)
        {
            playerRootTransform.position = position;
            OnGameStart();
        }

        private void OnGameStart()
        {
            CommonData.PlayerState = EPlayerState.Idle;
            EventsService.PlayerStay();
        }

        /// <summary>
        /// двигаем игрока
        /// </summary>
        /// <param name="vertical">значение Х</param>
        /// <param name="horizontal">значение З</param>
        private void OnPlayerTurn(float vertical, float horizontal)
        {
            if (CommonData.PlayerState == EPlayerState.Dance
                || CommonData.PlayerState == EPlayerState.Dead) return;

            if (vertical != 0f || horizontal != 0f)
            {
                verticalValue = vertical;
                horizontalValue = horizontal;
            }
        }

        private void Update()
        {
            if (verticalValue != 0f || horizontalValue != 0f)
            {
                if (CommonData.PlayerState == EPlayerState.Idle)
                {
                    CommonData.PlayerState = EPlayerState.Walk;
                    EventsService.PlayerWalk();
                }

                mDirection = Vector3.forward * verticalValue +
                             Vector3.right * horizontalValue;

                playerBodyTransform.forward = Vector3.Lerp(playerBodyTransform.forward,
                    playerBodyTransform.forward + mDirection, 0.5f);

                mDirection *= Settings.i.PlayerSpeed;
                playerRootTransform.position = Vector3.Lerp(playerRootTransform.position,
                    playerRootTransform.position + mDirection, 0.5f);

                verticalValue = horizontalValue = 0f;
            }
            else
            {
                if (CommonData.PlayerState == EPlayerState.Walk)
                {
                    CommonData.PlayerState = EPlayerState.Idle;
                    EventsService.PlayerStay();
                }
            }
        }

        /// <summary>
        /// игрок на финише
        /// </summary>
        private void OnPlayerFinished()
        {
            StartCoroutine(OnPlayerFinishedRoutine());
        }

        private IEnumerator OnPlayerFinishedRoutine()
        {
            CommonData.PlayerState = EPlayerState.Dance;

            yield return new WaitForSeconds(1f);

            EventsService.GameWin();
        }

        /// <summary>
        /// игрок погиб
        /// </summary>
        private void OnPlayerDeath()
        {
            StartCoroutine(OnPlayerDeathRoutine());
        }

        /// <summary>
        /// корутина смерти игрока
        /// </summary>
        /// <returns></returns>
        private IEnumerator OnPlayerDeathRoutine()
        {
            CommonData.PlayerState = EPlayerState.Dead;

            yield return new WaitForSeconds(1f);

            EventsService.GameOver();
        }

        private void OnGameRestart()
        {
        }
    }
}