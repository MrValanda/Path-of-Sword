using System;
using System.Collections;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;
using XftWeapon;

public class Test : MonoBehaviour
{
    [SerializeField] private MeshFilter _meshRenderer;
    [SerializeField] private ParticleSystem _particleSystem;

    [Button]
    private void Test1(float t)
    {
        Time.timeScale = t;
    }
}