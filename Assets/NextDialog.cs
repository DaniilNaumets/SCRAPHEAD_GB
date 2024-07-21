using Microsoft.Unity.VisualStudio.Editor;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NextDialog : MonoBehaviour
{
    [SerializeField] private Sprite[] sprites;
    private int currentInt;
    [SerializeField] private UnityEngine.UI.Image image;
    public void Next()
    {
        if (currentInt == sprites.Length)
        {
            FindObjectOfType<GameManager>().CloseDialogPanel();
            return;
        }
        currentInt++;
        image.sprite = sprites[currentInt];
    }
}
