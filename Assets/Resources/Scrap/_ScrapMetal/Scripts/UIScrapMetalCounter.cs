using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Resources.UI
{
    public class UIScrapMetalCounter : MonoBehaviour
    {
        [Header("Counter text")]       
        [SerializeField] private Text scrapMetalCounterTextUI;

        [SerializeField] private TextMeshProUGUI _text1;
        [SerializeField] private TextMeshProUGUI _text2;
        [SerializeField] private TextMeshProUGUI _text3;

        int num;

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.Z))
            {
                OutputScrapMetalOnUI(num += 11);
            }
        }
        public void OutputScrapMetalOnUI(int value)
        {
            int razrad1 = value % 10;
            int razrad10 = (value / 10) % 10;
            int razrad100 = (value / 100) % 10;

            _text1.text = razrad1.ToString();
            _text2.text = razrad10.ToString();
            _text3.text = razrad100.ToString();

            num = value;
            Color color;
            if(ColorUtility.TryParseHtmlString("#FFA400", out color))
            {
                if (num > 100)
                    _text3.color = color;
                if (num > 10)
                    _text2.color = color;
                if(num > 0)
                    _text1.color = color;
            }

            if (ColorUtility.TryParseHtmlString("#464040", out color))
            {
                if (num < 100)
                    _text3.color = color;
                if (num < 10)
                    _text2.color = color;
                if (num < 0)
                    _text1.color = color;
            }

        }

        //public void OutputScrapMetalOnUI(int value)
        //{
        //    scrapMetalCounterTextUI.text = value.ToString();
        //}
    }
}

