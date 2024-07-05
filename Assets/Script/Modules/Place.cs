using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Place : MonoBehaviour
{
    [SerializeField] private locationType location;

    private bool isBusy;

    private void Awake()
    {
        if (transform.childCount == 0) isBusy = false;
        else isBusy = true;
    }
    public enum locationType
    {
        Top, Bottom
    }

    public void ChangeSortingLayer(SpriteRenderer renderer)
    {
        switch (location)
        {
            case locationType.Top: renderer.sortingLayerName = GlobalStringVars.TopModuleSortingLayer; break;
            case locationType.Bottom: renderer.sortingLayerName = GlobalStringVars.BottomModuleSortingLayer; break;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.GetComponent<Equipment>() && !isBusy)
        {
            collision.gameObject.GetComponent<Equipment>().SetEquip(this.transform);
            isBusy = true;
        }
    }

    public void SetBusy(bool value) => isBusy = value;
}
