using UnityEngine;

namespace Resources
{
    public class ScrapPickup : MonoBehaviour
    {
        private int currentValueScrap;
        private float currentCollectionTime;

        private bool isGoing;

        public void SetTransmittedValue(int valueScrap, float collectionTime)
        {
            currentValueScrap = valueScrap;
            currentCollectionTime = collectionTime;
        }

        public int GetValueScrap()
        {
            if (currentValueScrap > 0)
            {
                return currentValueScrap;
            }
            else
            {
                return 0;
            }      
        }

        public float GetCollectionTime()
        {
            if (currentCollectionTime > 0)
            {
                return currentCollectionTime;
            }
            else
            {
                return 0;
            }
        }

        public void SetIsGoing(bool isGoing) => this.isGoing = isGoing;

        public bool GetIsGoing() => isGoing;
    }
}

