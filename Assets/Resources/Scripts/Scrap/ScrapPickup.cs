using Resources.UI;
using UnityEngine;

namespace Resources
{
    public class ScrapPickup : MonoBehaviour
    {
        [Header("Components")]
        [SerializeField] private UIScrapMetalCounter scrapMetalCounterUI;

        private int valueScrapMetal;

        public void SetValue(int value)
        {
            valueScrapMetal = value;
        }

        public int Pickup()
        {
            if (valueScrapMetal > 0)
            {
                //scrapMetalCounterUI.OutputValueOnUI(valueScrapMetal);
                return valueScrapMetal;
            }
            else
            {
                return 0;
            }      
        }
    }
}

