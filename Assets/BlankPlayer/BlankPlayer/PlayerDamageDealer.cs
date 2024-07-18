using UnityEngine;

public class PlayerDamageDealer : MonoBehaviour
{
    [SerializeField] private float damage;
    [SerializeField] private bool isDestroy;

    public float GetDamage()
    {
        if (isDestroy)
        transform.parent.gameObject.SetActive(false);
        return damage;
    }
}
