using UnityEngine;

namespace FunnyBlox
{
    public class LevelController : MonoBehaviour
    {
        [SerializeField] private int levelTime = 60;
        [SerializeField] private Transform playerSpawnPoint;

        private void Start()
        {
            CommonData.LevelTime = levelTime;
            EventsService.PlayerToStartPosition(playerSpawnPoint.position);
        }
    }
}