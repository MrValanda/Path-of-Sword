using System;
using System.Collections;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using Source.Scripts.EntityLogic;
using Source.Scripts.Visitors;
using UnityEngine;
using XftWeapon;

public class Test : MonoBehaviour
{
    [SerializeField] private SwordAttackVisitor _swordAttackVisitor;
    [SerializeField] private Entity _entity;

    private void Update()
    {
        throw new NotImplementedException();
    }

    [Button]
    private void Test1(float t)
    {
        Time.timeScale = t;
    }
}