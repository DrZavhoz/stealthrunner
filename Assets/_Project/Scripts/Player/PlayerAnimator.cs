using UnityEngine;

namespace FunnyBlox
{
    public class PlayerAnimator : MonoBehaviour
    {
        [SerializeField] private Animator animator;
        
        private void OnEnable()
        {
            EventsService.OnPlayerWalk += OnPlayerWalk;
            EventsService.OnPlayerStay += OnPlayerStay;
            EventsService.OnPlayerFinished += OnPlayerFinished;
            EventsService.OnPlayerDeath += OnPlayerDeath;
        }

        private void OnDisable()
        {
            EventsService.OnPlayerWalk -= OnPlayerWalk;
            EventsService.OnPlayerStay -= OnPlayerStay;
            EventsService.OnPlayerFinished -= OnPlayerFinished;
            EventsService.OnPlayerDeath -= OnPlayerDeath;
        }
        
        private void OnPlayerStay()
        {
            animator.Play("Rig_main_003_Idle_with_barrel");
        }
        
        private void OnPlayerWalk()
        {
            animator.Play("Rig_main_003_Run_with_burrel");
        }

        private void OnPlayerFinished()
        {
            animator.Play("Rig_main_003_Dancing_loop");
        }
            
        private void OnPlayerDeath()
        {
            animator.Play("HandsUp");
        }
    }
}