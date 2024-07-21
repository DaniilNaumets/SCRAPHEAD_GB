
using UnityEngine;
using UnityEngine.UI;

public class NextDialog : MonoBehaviour
{
    [SerializeField] private Sprite[] sprites;
    private int currentInt;
    [SerializeField] private UnityEngine.UI.Image image;

    private void Awake()
    {
        currentInt = 0;
    }
    public void Next()
    {
        if (currentInt == sprites.Length - 1)
        {
            FindObjectOfType<GameManager>().CloseDialogPanel();
            return;
        }
        currentInt++;
        image.sprite = sprites[currentInt];
    }
}
