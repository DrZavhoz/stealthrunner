using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using Sirenix.OdinInspector;
using UnityEngine;

namespace FunnyBlox
{
    public class EnemyController : MonoBehaviour
    {
        [LabelText("Трансформ врага")] [SerializeField]
        private Transform enemyTransform;

        [LabelText("Аниматор врага")] [SerializeField]
        private EnemyAnimator enemyAnimator;

        [Space] [LabelText("Поле зрения")] [SerializeField]
        private LineOfSight lineOfSight;

        [Space] [LabelText("Список вейпоинтов")] [SerializeField]
        private List<Transform> patrolWaypoints;

        [Space] [LabelText("Скорость передвижения")] [SerializeField]
        private float enemySpeed = 3f;

        [Space] [LabelText("Материал поля зрения стандартный")] [SerializeField]
        private Material normalVisionMaterial;

        [LabelText("Материал поля зрения активный")] [SerializeField]
        private Material angryVisionMaterial;

        private int currentPointId;

        private EEnemyState enemyState;

        private void OnEnable()
        {
            EventsService.OnGameStart += OnGameStart;
        }

        private void OnDisable()
        {
            EventsService.OnGameStart -= OnGameStart;
        }

        /// <summary>
        /// спавним на поле
        /// </summary>
        /// <param name="waypoints"></param>
        public void Spawn(List<Transform> waypoints)
        {
            patrolWaypoints = waypoints;

            currentPointId = 0;
            enemyTransform.position = patrolWaypoints[currentPointId].position;

            enemyTransform.Rotate(0f, Random.Range(0f, 360f), 0f);

            enemyAnimator.OnEnemyStay();
        }

        /// <summary>
        /// убираем с поля
        /// </summary>
        public void Despawn()
        {
            lineOfSight.visibleTargets.Clear();
            lineOfSight.gameObject.SetActive(false);

            transform.DOKill();
            StopAllCoroutines();
        }

        private void OnGameStart()
        {
            Patrol();

            StartCoroutine(OnLive());
        }

        /// <summary>
        /// стоит бездельничает
        /// </summary>
        private void Stay()
        {
            StartCoroutine(StayRoutine());
        }

        /// <summary>
        /// корутина безделия
        /// </summary>
        /// <returns></returns>
        private IEnumerator StayRoutine()
        {
            enemyAnimator.OnEnemyStay();
            yield return new WaitForSeconds(Random.Range(1f, 2f));
            Patrol();
        }

        /// <summary>
        /// пошёл к следующему партрульному поинту 
        /// </summary>
        private void Patrol()
        {
            enemyTransform.DOMove(
                    patrolWaypoints[currentPointId].position,
                    (patrolWaypoints[currentPointId].position - enemyTransform.position).magnitude / enemySpeed)
                .SetEase(Ease.Linear)
                .OnComplete(Random.Range(0, 100) > 50 ? Patrol : Stay);

            enemyTransform.LookAt(patrolWaypoints[currentPointId].position);

            enemyAnimator.OnEnemyWalk();

            currentPointId++;
            if (currentPointId >= patrolWaypoints.Count)
                currentPointId = 0;
        }

        /// <summary>
        /// корутина проверки "поля зрения"
        /// </summary>
        /// <returns></returns>
        private IEnumerator OnLive()
        {
            lineOfSight.gameObject.SetActive(true);
            yield return null;
            lineOfSight.SetMaterial(normalVisionMaterial);
            WaitForSeconds wfs = new WaitForSeconds(0.02f);
            while (true)
            {
                if (lineOfSight.visibleTargets.Count > 0 && CommonData.PlayerState == EPlayerState.Walk)
                {
                    StopAllCoroutines();
                    OnEnemyAtack();
                }

                yield return wfs;
            }
        }

        /// <summary>
        /// атакует игрока
        /// </summary>
        private void OnEnemyAtack()
        {
            enemyTransform.DOKill();

            lineOfSight.SetMaterial(angryVisionMaterial);
            enemyTransform.LookAt(lineOfSight.visibleTargets[0].position);

            enemyAnimator.OnEnemyAtack();

            EventsService.PlayerDeath();
        }

        private void OnDrawGizmosSelected()
        {
            Gizmos.color = Color.yellow;
            Gizmos.DrawWireSphere(enemyTransform.position, 2);
        }
    }
}