using System;
using System.Collections.Generic;
using System.Linq;
using Sirenix.OdinInspector;
using Source.Scripts.Abilities;
using Source.Scripts.AnimationEventListeners;
using Source.Scripts.EntityLogic;
using Source.Scripts.Interfaces;
using Source.Scripts.Setups;
using UnityEngine;
using Animation = Source.Scripts.Enemy.Animation;
using Random = UnityEngine.Random;

namespace Source.Modules.AbilityModule.Scripts
{
    public class AbilityCaster
    {
        public event Action StartCastSpell;
        public event Action<Ability> AbilityEnded;

        private AbilityEventListener _abilityEventListener;
        private Animator _animator;

        private float _lastEndSpellTime;
        private float _currentCooldown;

        private Transform _castPoint;
        private Ability _currentUsedAbility;
        private AbilityContainerSetup _abilityContainerSetup;
        private bool _castBlocked;

        public bool IsAbilityProcessed { get; private set; }

        public Entity Sender { get; private set; }

        private readonly Dictionary<string, float> _cooldownAbilities = new Dictionary<string, float>();

        public void Init(AbilityContainerSetup abilityContainerSetup, Entity sender)
        {
            _castPoint ??= new GameObject().transform;
            
            _abilityEventListener = sender.Get<AbilityEventListener>();
            _animator = sender.Get<Animation>().Animator;
            
            _abilityContainerSetup = abilityContainerSetup;
            _cooldownAbilities.Clear();
            foreach (AbilitySetup abilitySetup in abilityContainerSetup.AbilitySetups)
            {
                _cooldownAbilities.Add(abilitySetup.name, 0);
            }

            Sender = sender;
            Sender.Get<IDying>().Dead += OnSenderDeath;
            _lastEndSpellTime = 0;
        }

        [Button]
        public AbilitySetup UseSpell()
        {
            if (CanUseAbility(out List<AbilitySetup> readyAbilities) == false || Sender.Get<IDying>().IsDead)
            {
                return null;
            }

            StartCastSpell?.Invoke();
            IsAbilityProcessed = true;
            _abilityEventListener.AbilityEnded += OnAbilityEnded;
            AbilitySetup abilitySetup = GetRandomAbility(readyAbilities);

            _currentUsedAbility = new Ability(new AbilityDataContainer()
            {
                AbilitySetup = abilitySetup, Animator = _animator, AbilityEventListener = _abilityEventListener
            });
            _castPoint.parent = Sender.transform;
            _castPoint.position = Sender.transform.position;
            _castPoint.forward = Sender.transform.forward;
            _currentUsedAbility.CastSpell(_castPoint, Sender);
            _currentCooldown = abilitySetup.CooldownToUseAbility;
            return abilitySetup;
        }

        private static AbilitySetup GetRandomAbility(List<AbilitySetup> readyAbilities)
        {
            AbilitySetup abilitySetup = null;

            float totalWeight = readyAbilities.Sum(x => x.ChanceToUse);

            float randomNum = Random.Range(0, totalWeight);
            float weightSum = 0f;

            foreach (AbilitySetup readyAbility in readyAbilities)
            {
                weightSum += readyAbility.ChanceToUse;
                if (randomNum <= weightSum)
                {
                    abilitySetup = readyAbility;
                    break;
                }
            }

            return abilitySetup;
        }

        public void StopCastSpell()
        {
            IsAbilityProcessed = false;
            _currentUsedAbility?.StopCast();
        }

        public bool CanUseAbility()
        {
            if (_castBlocked) return false;
            if (IsCooldown() && _currentUsedAbility != null) return false;

            List<AbilitySetup> readyAbilities = _abilityContainerSetup.AbilitySetups
                .Where(x => Time.time - _cooldownAbilities[x.name] >= x.CooldownAbility)
                .Where(x => x.AbilityToUseConditions.AbilityConditions.All(a =>
                    a.CanExecute(Sender.transform, Sender, x.AbilityDataSetup))).ToList();

            if (readyAbilities.Count == 0) return false;

            return true;
        }

        private void OnSenderDeath(IDying obj)
        {
            Sender.Get<IDying>().Dead -= OnSenderDeath;
            IsAbilityProcessed = false;
            _abilityEventListener.AbilityEnded -= OnAbilityEnded;
            _currentUsedAbility?.StopCast();
        }

        private void OnAbilityEnded()
        {
            IsAbilityProcessed = false;
            AbilityEnded?.Invoke(_currentUsedAbility);
            _abilityEventListener.AbilityEnded -= OnAbilityEnded;
            _cooldownAbilities[_currentUsedAbility.AbilitySetup.name] = Time.time;
            _currentUsedAbility = null;
            _lastEndSpellTime = Time.time;
        }

        private bool CanUseAbility(out List<AbilitySetup> abilitySetups)
        {
            List<AbilitySetup> readyAbilities = _abilityContainerSetup.AbilitySetups
                .Where(x => Time.time - _cooldownAbilities[x.name] >= x.CooldownAbility)
                .Where(x => x.AbilityToUseConditions.AbilityConditions.All(a =>
                    a.CanExecute(Sender.transform, Sender, x.AbilityDataSetup))).ToList();
            abilitySetups = readyAbilities;

            if (_castBlocked) return false;

            if (IsCooldown() && _currentUsedAbility != null) return false;
            if (readyAbilities.Count == 0) return false;

            return true;
        }

        private bool IsCooldown()
        {
            return Time.time - _lastEndSpellTime < _currentCooldown;
        }
    }
}