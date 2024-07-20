using Resources;
using Resources.UI;
using UnityEngine;

namespace Entity
{
    public class EntityInventory : MonoBehaviour
    {
        [Header("Components")]
        [SerializeField] private UIScrapMetalCounter scrapMetalCounterUI;
        [SerializeField] private UIScrapAlienCounter scrapAlienCounterUI;

        private int quantityScrapMetal;
        private int quantityScrapAlien;
        private int scrap;

        public void AddScrapMetalToInventory(int scrapMetal)
        {
            quantityScrapMetal += scrapMetal;
            SumUpAllScrap(scrapMetal);
            Debug.Log(5);
            scrapMetalCounterUI?.OutputScrapMetalOnUI(quantityScrapMetal);
        }

        public void AddScrapAlienToInventory(int scrapAlien)
        {
            quantityScrapAlien += scrapAlien;
            SumUpAllScrap(scrapAlien);
            scrapAlienCounterUI?.OutputScrapAlienOnUI(quantityScrapAlien);
        }

        private void SumUpAllScrap(int scrap)
        {
            this.scrap += scrap;
        }

        public int GetScrap() => scrap; 
    }
}

