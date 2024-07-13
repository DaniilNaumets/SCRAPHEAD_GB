using Resources;
using System.Collections.Generic;
using Unity.VisualScripting;
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

        public Collider2D HasCurrentComponent<T>() where T : Component
        {
            foreach (var collider in collidersInTrigger)
            {
                if (collider.GetComponent<T>())
                {                   
                    return collider;
                }
            }
         
            return null;
        }
    }
}
