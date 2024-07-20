using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
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

    private float startScale;
    private void Awake()
    {
        startScale = GetComponent<RectTransform>().localScale.x;
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

    private void OnEnable()
    {
        buttonImage.sprite = unpressedSprite;
        GetComponent<RectTransform>().localScale = new Vector3(startScale, startScale);
    }

}
