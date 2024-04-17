using System;
using System.Collections.Generic;
using Source.Scripts.Data.Models;
using UnityEngine;

namespace Source.Scripts.Setups.Characters
{
    [CreateAssetMenu(fileName = "DropListSetup", menuName = "Setups/Drop/DropListSetup")]
    public class DropListSetup : ScriptableObject
    {
        [field: SerializeField, Min(1)] public float ReceiveDamageToTryDropping { get; private set; } = 1;
        [field: SerializeField] public List<DropItemSetup> DropItemSetups { get; private set; }
    }
    

    [Serializable]
    public struct DropItemSetup
    {
        public ItemName ItemType;
        [Range(1, 15)] public int Count;
        [Range(0, 100)] public float Chance;
        [Min(1)] public int TotalCost;
        public bool IgnoreItemCapacity;
        public bool IsDropOnce;
    }
}