using System;
using System.Collections.Generic;
using System.Linq;
using Sirenix.OdinInspector;
using Source.Scripts.AnimationEventListeners;
using Source.Scripts.Interfaces;
using Source.Scripts.Setups;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Source.Scripts.Abilities
{
    public class AbilityCaster : MonoBehaviour
    {
        public event Action StartCastSpell;
        public event Action<Ability> AbilityEnded;

        [SerializeField] private AbilityEventListener _abilityEventListener;
        [SerializeField] private Animator _animator;

        private float _lastCastSpellTime;
        private float _lastEndSpellTime;
        private float _currentCooldown;

        private Transform _castPoint;
        private Ability _currentUsedAbility;
        private AbilityContainerSetup _abilityContainerSetup;
        private bool _castBlocked;

        public bool IsAbilityProcessed { get; private set; }

        public Enemy.Enemy Sender { get; private set; }

        private readonly Dictionary<string, float> _cooldownAbilities = new Dictionary<string, float>();

        public void Init(AbilityContainerSetup abilityContainerSetup, Enemy.Enemy sender)
        {
            _castPoint ??= new GameObject().transform;
            _abilityContainerSetup = abilityContainerSetup;
            _cooldownAbilities.Clear();
            foreach (AbilitySetup abilitySetup in abilityContainerSetup.AbilitySetups)
            {
                _cooldownAbilities.Add(abilitySetup.name, 0);
            }

            Sender = sender;
            Sender.ComponentContainer.GetComponent<IDying>().Dead += OnSenderDeath;
            _lastEndSpellTime = 0;
            _lastCastSpellTime = 0;
        }

        public void BlockCast()
        {
            _castBlocked = true;
        }

        public void UnBlockCast()
        {
            _castBlocked = false;
        }

        [Button]
        public AbilitySetup UseSpell()
        {
            if (CanUseAbility(out var readyAbilities) == false || Sender.IsDead)
            {
                return null;
            }

            StartCastSpell?.Invoke();
            IsAbilityProcessed = true;
            _abilityEventListener.AbilityEnded += OnAbilityEnded;
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

            _currentUsedAbility = new Ability(new AbilityDataContainer()
            {
                AbilitySetup = abilitySetup, Animator = _animator, AbilityEventListener = _abilityEventListener
            });
            
            _castPoint.position = Sender.transform.position;
            _castPoint.forward = Sender.transform.forward;
            _currentUsedAbility.CastSpell(_castPoint, Sender);
            _currentCooldown = abilitySetup.CooldownToUseAbility;
            _lastCastSpellTime = Time.time;
            return abilitySetup;
        }

        public void StopSpell()
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
            Sender.ComponentContainer.GetComponent<IDying>().Dead -= OnSenderDeath;
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