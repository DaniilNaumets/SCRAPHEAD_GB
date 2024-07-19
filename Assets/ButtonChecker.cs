using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ButtonChecker : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    [SerializeField] private Sprite unpressedSprite;
    [SerializeField] private Sprite pressedSprite;

    private Button button;
    private Image buttonImage;

    private Animator animator;


    private void Awake()
    {
        button = GetComponent<Button>();
        buttonImage = GetComponent<Image>();
        animator = GetComponent<Animator>();
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        if (buttonImage != null && pressedSprite != null)
        {
            buttonImage.sprite = pressedSprite;
            animator.SetBool("isPressed", true);
        }
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        if (buttonImage != null && unpressedSprite != null)
        {
            buttonImage.sprite = unpressedSprite;
            animator.SetBool("isPressed", false);
        }
    }

    
}
