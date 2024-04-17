using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Sirenix.OdinInspector;
using Source.Scripts.Interfaces;

namespace Source.Scripts.Setups.Characters
{
    [Serializable]
    public struct DamageableType
    {
        [ValueDropdown(nameof(_factoryTypes)), HideLabel]
        public string Type;

        private List<string> _factoryTypes =>
            Assembly.GetAssembly(typeof(IDamageable)).GetTypes()
                .Where(x => x.IsClass && x.GetInterface(nameof(IDamageable)) != null)
                .Select(x => x.Name).ToList();
    }
}