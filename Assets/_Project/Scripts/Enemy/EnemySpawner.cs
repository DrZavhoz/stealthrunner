using System;
using System.Collections.Generic;
using FunnyBlox.Utils;
using UnityEngine;

namespace FunnyBlox
{
    public class EnemySpawner : MonoBehaviour
    {
        private Pool enemiesPool;

        private List<Transform> waypoints;

        private EnemyController enemy;

        
        private void OnEnable()
        {
            EventsService.OnGameStart += OnGameStart;
            EventsService.OnGameRestart += OnGameRestart;
        }

        private void OnDisable()
        {
            EventsService.OnGameStart -= OnGameStart;
            EventsService.OnGameRestart -= OnGameRestart;
        }
        
        private void Start()
        {
            enemiesPool = PoolCollection.GetPool("Enemy");

            waypoints = new List<Transform>();
            foreach (Transform child in transform)
            {
                waypoints.Add(child);
            }
            
            enemy = (EnemyController)enemiesPool.GetElement.Component;

            enemy.Spawn(waypoints);
            
        }

        private void OnGameStart()
        {
            
        }

        private void OnGameRestart()
        {
            enemy.Despawn();
            enemiesPool.FreeElement(enemy.transform, true);
        }
    }
}