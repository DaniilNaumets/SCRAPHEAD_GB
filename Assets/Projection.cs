using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projection : MonoBehaviour
{
    [SerializeField] private GameObject drone;
    [SerializeField] private GameObject[] placesForGuns;

    [ContextMenu("Init")]
    private void Proj()
    {
        for (int i = 0; i < drone.transform.childCount; i++)
        {
            if (drone.transform.GetChild(i).gameObject.TryGetComponent<Place>(out Place place))
            {
                if(place.gameObject.transform.childCount > 0)
                {
                    GameObject newPlace = GameObject.Instantiate(new GameObject(), placesForGuns[i].transform.position, placesForGuns[i].transform.rotation, this.transform);
                    newPlace.transform.localScale *= 5;
                    SpriteRenderer render = place.gameObject.GetComponentInChildren<SpriteRenderer>();
                    newPlace.AddComponent<SpriteRenderer>();
                    newPlace.GetComponent<SpriteRenderer>().sprite = render.sprite;
                }
            }
        }
        this.gameObject.GetComponent<SpriteRenderer>().sprite = drone.GetComponent<SpriteRenderer>().sprite;
    }
}
