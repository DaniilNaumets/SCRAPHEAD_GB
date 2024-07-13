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
            for (int i = collidersInTrigger.Count - 1; i >= 0; i--)
            {
                if (collidersInTrigger[i] == null || collidersInTrigger[i].gameObject == null)
                {
                    collidersInTrigger.RemoveAt(i);
                    continue;
                }

                if (collidersInTrigger[i].GetComponent<T>() != null)
                {
                    return collidersInTrigger[i];
                }
            }
            return null;
        }

    }
}
