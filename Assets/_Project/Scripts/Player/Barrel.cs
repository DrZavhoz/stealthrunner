using UnityEngine;

namespace FunnyBlox
{
    public class Barrel : MonoBehaviour
    {
        [SerializeField] private Vector3 staePosition;
        [SerializeField] private Vector3 walkPosition;

        private void OnEnable()
        {
            EventsService.OnPlayerWalk += OnPlayerWalk;
            EventsService.OnPlayerStay += OnPlayerStay;
            EventsService.OnPlayerFinished += HideBarrel;
            EventsService.OnPlayerDeath += HideBarrel;
            EventsService.OnGameRestart += ShowBarrel;
        }

        private void OnDisable()
        {
            EventsService.OnPlayerWalk -= OnPlayerWalk;
            EventsService.OnPlayerStay -= OnPlayerStay;
            EventsService.OnPlayerFinished -= HideBarrel;
            EventsService.OnPlayerDeath -= HideBarrel;
            EventsService.OnGameRestart -= ShowBarrel;
        }

        private void OnPlayerWalk()
        {
            transform.localPosition = walkPosition;
        }

        private void OnPlayerStay()
        {
            transform.localPosition = staePosition;
        }

        private void HideBarrel()
        {
            transform.GetChild(0).gameObject.SetActive(false);
        }

        private void ShowBarrel()
        {
            transform.GetChild(0).gameObject.SetActive(true);
        }
    }
}