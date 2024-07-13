using System.Collections;
using UnityEngine;
using UnityEngine.UI;

namespace Resources.UI
{
    public class UIScrapCollectionProgress : MonoBehaviour
    {
        [Header("UI Components")]
        [SerializeField] private Image scrapProgressImage;
        private Coroutine fillCoroutine;

        public void StartFill(float duration)
        {
            if (fillCoroutine != null)
            {
                StopCoroutine(fillCoroutine);
            }

            fillCoroutine = StartCoroutine(FillOverTime(duration));
        }

        public void ResetFill()
        {
            if (fillCoroutine != null)
            {
                StopCoroutine(fillCoroutine);
                fillCoroutine = null;
            }
            scrapProgressImage.fillAmount = 0f;
        }

        private IEnumerator FillOverTime(float duration)
        {
            float elapsedTime = 0f;

            while (elapsedTime < duration)
            {
                elapsedTime += Time.deltaTime;
                scrapProgressImage.fillAmount = Mathf.Clamp01(elapsedTime / duration);
                yield return null;
            }

            scrapProgressImage.fillAmount = 1f;
        }
    }
}