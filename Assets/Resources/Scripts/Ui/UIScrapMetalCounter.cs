using UnityEngine;
using UnityEngine.UI;

namespace Resources.UI
{
    public class UIScrapMetalCounter : MonoBehaviour
    {
        [Header("Counter text")]       
        [SerializeField] private Text scrapMetalCounterTextUI;

        public void OutputValueOnUI(int value)
        {
            scrapMetalCounterTextUI.text = value.ToString();
        }
    }
}

