using UnityEngine;

namespace FunnyBlox
{
    public class EnemyAnimator : MonoBehaviour
    {
        [SerializeField] private Animator animator;
        
        public void OnEnemyStay()
        {
            animator.Play("RIg_Long_Idle_Long");
        }
        
        public void OnEnemyWalk()
        {
            animator.Play("RIg_Long_Walking_Long");
        }

        public void OnEnemyAtack()
        {
            animator.Play("RIg_Long_Shoot_Long");
        }
    }
}