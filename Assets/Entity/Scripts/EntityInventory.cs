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

        public void AddScrapMetalToInventory(int scrapMetal)
        {
            quantityScrapMetal += scrapMetal;
            scrapMetalCounterUI?.OutputScrapMetalOnUI(quantityScrapMetal);
        }

        public void AddScrapAlienToInventory(int scrapAlien)
        {
            quantityScrapAlien += scrapAlien;
            scrapAlienCounterUI?.OutputScrapAlienOnUI(quantityScrapAlien);
        }
    }
}

