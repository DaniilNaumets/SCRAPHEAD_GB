using UnityEngine;

namespace Enemies
{
    public class EnemyAggressiveState : MonoBehaviour
    {
        [SerializeField] private bool isAggressive = true;

        public void SetState(bool isAggressive)
        {
            this.isAggressive = isAggressive;
        } 

        public bool GetState() => isAggressive;
    }
}

