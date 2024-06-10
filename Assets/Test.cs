using System.Linq;
using Sirenix.OdinInspector;
using Source.Modules.CombatModule.Scripts;
using Source.Modules.HealthModule.Scripts;
using Source.Scripts.EntityLogic;
using UnityEngine;

public class Test : MonoBehaviour
{
    [SerializeField] private SwordAttackVisitor _swordAttackVisitor;
    [SerializeField] private Entity _entity;

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.CapsLock))
        {
            _swordAttackVisitor.Visit(_entity.Get<HealthComponent>());
        }
    }

    [Button]
    private void Test1(float t)
    {
        Time.timeScale = t;
    }

    [Button]
    private void Test22(Transform a, Transform b)
    {
        Transform[] aTransforms = a.GetComponentsInChildren<Transform>();
        string[] bTransforms = b.GetComponentsInChildren<Transform>().Select(x=>x.name).ToArray();
        foreach (var transform1 in aTransforms.Where(x=>bTransforms.Contains(x.name) == false))
        {
            Debug.LogError(transform1.name);
        }
    }
}