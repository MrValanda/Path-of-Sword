using UnityEngine;

namespace Source.Scripts.Enemy
{
    public class RangeEnemyWeapon : EnemyWeapon
    {
        [field: SerializeField] public Transform SpawnProjectilePosition { get; private set; }
    }
}