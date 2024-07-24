using UnityEngine;

namespace FunnyBlox
{
    public class EventsHandler : MonoBehaviour
    {
        public void GameStart()
        {
            EventsService.GameStart();
        }

        public void GameRestart()
        {
            EventsService.GameRestart();
        }
    }
}