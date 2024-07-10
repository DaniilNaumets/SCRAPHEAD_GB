using System.Collections.Generic;
using UnityEngine;

namespace Enemies
{
    public class EnemyLineOfSight : MonoBehaviour
    {
        private List<Collider2D> collidersInTrigger = new List<Collider2D>();

        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (!collidersInTrigger.Contains(collision))
            {
                collidersInTrigger.Add(collision);
            }
        }

        private void OnTriggerExit2D(Collider2D collision)
        {
            if (collidersInTrigger.Contains(collision))
            {
                collidersInTrigger.Remove(collision);
            }
        }

        public bool GetContainsCollider<T>() where T : Component
        {
            foreach (var collider in collidersInTrigger)
            {
                if (collider.GetComponent<T>())
                {
                    return true;
                }
            }
            return false;
        }
    }
}
