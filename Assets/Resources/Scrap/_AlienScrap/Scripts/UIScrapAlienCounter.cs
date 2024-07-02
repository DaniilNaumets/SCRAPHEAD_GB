using UnityEngine;
using UnityEngine.UI;

namespace Resources
{
    public class UIScrapAlienCounter : MonoBehaviour
    {
        [Header("Counter text")]
        [SerializeField] private Text scrapMetalCounterTextUI;

        public void OutputScrapAlienOnUI(int value)
        {
            scrapMetalCounterTextUI.text = value.ToString();
        }
    }
}

