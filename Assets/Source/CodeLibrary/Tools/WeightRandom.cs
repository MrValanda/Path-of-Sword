using System.Collections.Generic;
using System.Linq;

namespace Source.CodeLibrary.Tools
{
    public static class WeightRandom
    {
        public static bool DoChance(this float chance) => UnityEngine.Random.Range(0f, 100f) <= chance;
        public static bool DoChance(this int chance) => UnityEngine.Random.Range(0f, 100f) <= chance;

        public static T GetByWeight<T>(List<T> items) where T : IContainsPercent
        {
            if (items.Count == 0)
                return default;
            
            float max = items.Sum(setup => setup.GetWeight());
            float randomValue = UnityEngine.Random.Range(0, max - 1);
            
            foreach (T rewardSetup in items)
            {
                randomValue -= rewardSetup.GetWeight();
                if (randomValue > 0) 
                    continue;
                
                return rewardSetup;
            }

            return default;
        }
        
        public static T Random<T>(this IList<T> collection, int minIndex = -1, int maxIndex = -1)
        {
            return collection[UnityEngine.Random.Range(
                minIndex >= 0 ? minIndex : 0,
                maxIndex >= 0 ? maxIndex : collection.Count)];
        }
    }
    
    public interface IContainsPercent
    {
        public int GetWeight();
    }
}