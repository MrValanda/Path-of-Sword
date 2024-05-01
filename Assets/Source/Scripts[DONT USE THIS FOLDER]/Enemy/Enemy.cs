using System;
using System.Collections.Generic;
using System.Linq;
using BehaviorDesigner.Runtime;
using DG.Tweening;
using Lean.Pool;
using Sirenix.OdinInspector;
using Source.CodeLibrary.ServiceBootstrap;
using Source.Scripts.Abilities;
using Source.Scripts.AttackPointCalculators;
using Source.Scripts.BehaviorsNodes.SharedVariables;
using Source.Scripts.CodeGenerator;
using Source.Scripts.InterfaceLinker;
using Source.Scripts.Interfaces;
using Source.Scripts.Setups;
using Source.Scripts.Setups.Characters;
using Source.Scripts.Utils;
using UnityEngine;

namespace Source.Scripts.Enemy
{
    [Serializable]
    public struct PatrolPointData
    {
        public float TimeToGetNextPoint;
        public Transform PatrolPoint;
    }

    public enum EnemySkinType
    {
        none = -1,
        Skeleton = 0,
        Rat = 1,
        Mushroom = 2,
        Dragon = 3,

        BossSkeleton = 50,
        BossGolem = 51,
        BossWizard = 52,
    }

    public abstract class Enemy : MonoBehaviour, IInteractable
    {
        private const float DestroyDelay = 5f;
        public event Action<double> ReceiveHealing;

        [SerializeField] private ComponentContainerMonoLinker _componentContainerMonoLinker;
        [SerializeField] private EnemyBehaviorTreeDataContainer _enemyBehaviorTreeDataContainer;
        [SerializeField] private EnemySkinType _enemySkinType;
        [SerializeField] private ParticleSystem _spawnVfx;
        [SerializeField] private ParticleSystem _dieVfx;
        [field: SerializeField] public Transform EnemySpawnPoint { get; private set; }
        [field: SerializeField] public Transform DieVFXSpawnPoint { get; private set; }
        [field: SerializeField] public DamageableLinker Target { get; private set; }
        [field: SerializeField] public EnemyComponents EnemyComponents { get; private set; }
        [field: SerializeField] public AbilityCaster AbilityCaster { get; private set; }


        private int _lvl;
        public List<PatrolPointData> PatrolPoints { get; private set; }
        public EnemyCharacterSetup EnemyCharacterSetup { get; private set; }
        public EnemyWeapon EnemyWeaponLeftHand { get; private set; }
        public EnemyWeapon EnemyWeaponRightHand { get; private set; }
        public double CurrentDamage { get; private set; }
        public double CurrentHealth { get; private set; }
        public bool IsInteractionAvailable => CurrentHealth > 0;
        public bool IsDead => !IsInteractionAvailable;
        public string EnemyKey { get; private set; }
        public bool NeedHeal => CurrentHealth < DefaultHealth();

        public ComponentContainer ComponentContainer => _componentContainerMonoLinker.ComponentsContainer;


        public EnemySkinType GetEnemyType() => _enemySkinType;

        public void Init(EnemyCharacterSetup enemyCharacterSetup, DamageableLinker damageableTarget, Transform enemySpawnPoint,
            List<PatrolPointData> patrolPoints, int lvl, string enemyKey)
        {
            _lvl = lvl;
            _componentContainerMonoLinker.Init();
            
            EnemyCharacterSetup = enemyCharacterSetup;
            Target = damageableTarget;
            EnemySpawnPoint = enemySpawnPoint;
            PatrolPoints = patrolPoints;
            GetComponent<CapsuleCollider>().enabled = true;
            AbilityCaster.Init(EnemyCharacterSetup.AbilityContainerSetup
                ? EnemyCharacterSetup.AbilityContainerSetup
                : ScriptableObject.CreateInstance<AbilityContainerSetup>(), this);

            EnemyWeaponLeftHand = LeanPool.Spawn(EnemyCharacterSetup.EnemyWeaponLeftHand,
                EnemyComponents.WeaponParentLeftHand);
            EnemyWeaponRightHand = LeanPool.Spawn(EnemyCharacterSetup.EnemyWeaponRightHand,
                EnemyComponents.WeaponParentRightHand);

            EnemyWeaponLeftHand.Init(EnemyCharacterSetup.AttakedUnits.DamageableTypes
                    .Select(x => x.Type).ToList());

            EnemyWeaponRightHand.Init(EnemyCharacterSetup.AttakedUnits.DamageableTypes
                    .Select(x => x.Type).ToList());
            
            // EnemyComponents.Hitbox.Init();
            // EnemyComponents.Hitbox.Enable();

            EnemyComponents.NpcMovement.Init(EnemyCharacterSetup.DefaultMoveSpeed,
                EnemyCharacterSetup.DefaultAcceleration);

            InitBehaviorTree();
            
            ResetHealth();
            CurrentDamage = 0;
            // ParticleSystem system = LeanPool.Spawn(_spawnVfx, transform);
            // system.transform.parent = null;
            EnemyKey = enemyKey;
        }


        private void InitBehaviorTree()
        {
            Transform targetTransform = Target.transform;
            IAttackPointCalculator attackPointCalculator = GetAttackPointCalculator();

            InitBehaviorVariables(targetTransform, attackPointCalculator);
            InitDeathVariables();
            InitAttackVariables();
            InitAbilityVariables();
        }

        private void InitAbilityVariables()
        {
            InitVariable<SharedGameConditionsContainer, GameConditionsContainer>(
                BehaviorVariables._abilityProcessingGameConditions,
                _enemyBehaviorTreeDataContainer.GetAbilityProcessingConditionsContainer());

            InitVariable<SharedGameActionsContainer, GameActionContainer>(
                BehaviorVariables._abilityProcessCastingGameActions,
                _enemyBehaviorTreeDataContainer.GetProcessingUseAbilityActionsContainer());

            InitVariable<SharedGameActionsContainer, GameActionContainer>(
                BehaviorVariables._afkActionsContainer,
                _enemyBehaviorTreeDataContainer.GetAfkGameActionsContainer());

            InitVariable<SharedGameConditionsContainer, GameConditionsContainer>(
                BehaviorVariables._canUseAbilityConditionsContainer,
                _enemyBehaviorTreeDataContainer.GetCanUseAbilityConditionsContainer());

            InitVariable<SharedGameActionsContainer, GameActionContainer>(BehaviorVariables._startUseAbilityGameActions,
                _enemyBehaviorTreeDataContainer.GetStartUseAbilityActionsContainer());
        }

        private void InitDeathVariables()
        {
            InitVariable<SharedGameConditionsContainer, GameConditionsContainer>(
                BehaviorVariables._deathGameConditionContainer,
                _enemyBehaviorTreeDataContainer.GetIsDeathConditionsContainer());

            InitVariable<SharedGameActionsContainer, GameActionContainer>(BehaviorVariables._deathGameActions,
                _enemyBehaviorTreeDataContainer.GetDeathActionsContainer());

            InitVariable<SharedGameActionsContainer, GameActionContainer>(BehaviorVariables._takeDamageGameActions,
                _enemyBehaviorTreeDataContainer.GetTakeDamageGameActionsContainer());
        }

        private void InitAttackVariables()
        {
            InitVariable<SharedGameActionsContainer, GameActionContainer>(BehaviorVariables._attackGameActions,
                _enemyBehaviorTreeDataContainer.GetAttackActionsContainer());

            InitVariable<SharedGameConditionsContainer, GameConditionsContainer>(
                BehaviorVariables._canAttackGameConditionsContainer,
                _enemyBehaviorTreeDataContainer.GetCanAttackConditionsContainer());

            InitVariable<SharedGameConditionsContainer, GameConditionsContainer>(
                BehaviorVariables._canSeeGameConditionsContainer,
                _enemyBehaviorTreeDataContainer.GetCanSeeConditionsContainer());
        }

        private void InitBehaviorVariables(Transform targetTransform, IAttackPointCalculator attackPointCalculator)
        {
            InitVariable<SharedTransform, Transform>(BehaviorVariables._target, targetTransform);

            InitVariable<SharedTransform, Transform>(BehaviorVariables._spawnPoint, EnemySpawnPoint.transform);

            InitVariable<SharedTransform, Transform>(BehaviorVariables._enemy, transform);

            InitVariable<SharedEnemyMovement, IMovement>(BehaviorVariables._enemyMovement,
                EnemyComponents.NpcMovement);

            InitVariable<SharedAttackPointCalculatorLinker, AttackPointCalculatorLinker>(
                BehaviorVariables._attackPointCalculatorLinker, new AttackPointCalculatorLinker(attackPointCalculator));

            InitVariable<SharedPatrolPointList, List<PatrolPointData>>(BehaviorVariables._patrolPoints, PatrolPoints);
            InitVariable<SharedTransform, Transform>(BehaviorVariables._currentPatrolPoint,
                PatrolPoints.FirstOrDefault().PatrolPoint);

            InitVariable<SharedGameObject, GameObject>(BehaviorVariables._enemyAnimator,
                EnemyComponents.Animation.gameObject);

            InitVariable<SharedGameConditionsContainer, GameConditionsContainer>(
                BehaviorVariables._needBackToSpawnPoint,
                _enemyBehaviorTreeDataContainer.GetNeedToBackSpawnPointConditionsContainer());
        }

        public virtual IAttackPointCalculator GetAttackPointCalculator()
        {
            return new MeleeAttackPointCalculator(Target.transform, transform);
        }

        private void InitVariable<T, T1>(string nameVariable, T1 value) where T : SharedVariable<T1>, new()
        {
            EnemyComponents.BehaviorTree.SetVariable(nameVariable, new T() {Value = value});
        }

        public void UpdateDamage(double newBaseDamage)
        {
            CurrentDamage = newBaseDamage + EnemyCharacterSetup.DamageMultiplier * _lvl;
        }
        
        // protected void Die()
        // {
        //     if (EnemyCharacterSetup.SlowMotionTime > 0)
        //     {
        //         Time.timeScale = 0.2f;
        //         DOVirtual.DelayedCall(EnemyCharacterSetup.SlowMotionTime, () => { Time.timeScale = 1f; });
        //     }
        //
        //     DOVirtual.DelayedCall(DestroyDelay, () =>
        //     {
        //         ParticleSystem system = LeanPool.Spawn(_dieVfx, DieVFXSpawnPoint);
        //         system.transform.parent = null;
        //         system.transform.localScale = _dieVfx.transform.localScale;
        //         LeanPool.Despawn(this);
        //         LeanPool.Despawn(EnemyWeaponLeftHand);
        //         LeanPool.Despawn(EnemyWeaponRightHand);
        //     });
        // }

        public void ReceiveHeal(double heal)
        {
            if (NeedHeal)
            {
                CurrentHealth += heal;
                CurrentHealth = Math.Clamp(CurrentHealth, 0, DefaultHealth());
                ReceiveHealing?.Invoke(heal);
            }
        }

        public void ResetHealth()
        {
            ReceiveHeal(DefaultHealth());
        }

        public double DefaultHealth()
        {
            return EnemyCharacterSetup.DefaultHealth + EnemyCharacterSetup.HealthMultiplier * _lvl;
        }
    }
}