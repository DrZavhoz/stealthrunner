using System;
using UnityEngine;

namespace FunnyBlox
{
    public class PlayerCollider : MonoBehaviour
    {
        private void OnTriggerEnter(Collider sender)
        {
            if (sender.CompareTag("Finish") && CommonData.PlayerState != EPlayerState.Dance)
            {
                EventsService.PlayerFinished();
            }
        }
    }
}