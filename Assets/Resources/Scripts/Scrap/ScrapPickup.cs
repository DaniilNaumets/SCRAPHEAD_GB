using UnityEngine;

namespace Resources
{
    public class ScrapPickup : MonoBehaviour
    {
        private int currentValueScrap;
        private float currentCollectionTime;

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
    }
}

