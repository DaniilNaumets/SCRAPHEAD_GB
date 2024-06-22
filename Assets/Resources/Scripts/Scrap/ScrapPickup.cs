using Resources.UI;
using UnityEngine;

namespace Resources
{
    public class ScrapPickup : MonoBehaviour
    {
        [Header("Components")]
        [SerializeField] private UIScrapMetalCounter scrapMetalCounterUI;

        private int currentValueScrap;
        private float currentCollectionTime;

        public void SetTransmittedValue(int valueScrap, float collectionTime)
        {
            currentValueScrap = valueScrap;
            currentCollectionTime = collectionTime;
        }

        public int GetValueScrap()
        {
            if (currentValueScrap > 0)// сделать bool или убрать и сделать проверку в луче
            {
                //scrapMetalCounterUI.OutputValueOnUI(valueScrapMetal);
                return currentValueScrap;
            }
            else
            {
                return 0;
            }      
        }

        public float GetCollectionTime()
        {
            if (currentCollectionTime > 0)// сделать bool или убрать и сделать проверку в луче
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

