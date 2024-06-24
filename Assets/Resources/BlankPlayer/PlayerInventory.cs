using Resources.UI;
using UnityEngine;

public class PlayerInventory : MonoBehaviour
{
    [Header("Components")]
    [SerializeField] private UIScrapMetalCounter scrapMetalCounterUI;

    private int quantityScrapMetal;

    public void AddScrapMetalToInventory(int scrapMetal)
    {
        quantityScrapMetal += scrapMetal;
        scrapMetalCounterUI.OutputScrapMetalOnUI(quantityScrapMetal);
    }
}
