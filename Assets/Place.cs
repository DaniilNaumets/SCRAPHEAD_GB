using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Place : MonoBehaviour
{
    [SerializeField] private locationType location;
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
}
