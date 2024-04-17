using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using XftWeapon;

public class Test : MonoBehaviour
{
    [SerializeField] private MeshFilter _meshRenderer;
    [SerializeField] private ParticleSystem _particleSystem;

    private void Update()
    {
        if (_meshRenderer == null) return;
        _particleSystem.GetComponent<ParticleSystemRenderer>().mesh = _meshRenderer.mesh;
    }
}