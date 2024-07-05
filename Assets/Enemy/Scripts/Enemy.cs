using UnityEngine;

namespace Enemies
{
    [CreateAssetMenu(fileName = "New Enemy", menuName = "Enemy")]
    public class Enemy : ScriptableObject
    {
        [SerializeField] public string Name;
        [SerializeField] public Sprite Sprite;
        [SerializeField] public float Health;
        [SerializeField] public float MovementSpeed;
        [SerializeField] public float Size;
        //[SerializeField] public float rotationSpeed;
        [SerializeField] public float ShootingDistance;
        [SerializeField][Range(1, 10)] public float SpawnFrequency;
    }
}

