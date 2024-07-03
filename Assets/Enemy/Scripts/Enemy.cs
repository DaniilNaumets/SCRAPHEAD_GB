using UnityEngine;

namespace Enemies
{
    [CreateAssetMenu(fileName = "New Enemy", menuName = "Enemy")]
    public class Enemy : ScriptableObject
    {
        public new string name;
        public Sprite sprite;
        public float health;
        public float movementSpeed;
        public float rotationSpeed;
        public float shootingDistance;
    }
}

