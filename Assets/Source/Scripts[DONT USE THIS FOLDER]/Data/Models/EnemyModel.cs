using System.Collections.Generic;
using Source.Scripts.Enemy;
using UnityEngine;

namespace Source.Scripts.Data.Models
{
    public enum ItemName
    {
    }

    [UnityEngine.Scripting.Preserve]
    public class EnemyModel
    {
        private List<EnemySubModel> Models = new List<EnemySubModel>();
        private Dictionary<string, List<ItemName>> _nonDroppableItems = new();

        public void ClearData()
        {
            Models.Clear();
        }

        public bool CanDropItem(string key, ItemName itemData)
        {
            if (key == "" || _nonDroppableItems.ContainsKey(key) == false) return true;
            return _nonDroppableItems[key].Contains(itemData) == false;
        }

        public void RemoveDropChance(string key, ItemName itemName)
        {
            if (_nonDroppableItems.TryAdd(key, new List<ItemName>() {itemName}) == false)
            {
                _nonDroppableItems[key].Add(itemName);
            }
        }

        public EnemySubModel GetByID(string id)
        {
            EnemySubModel Model = Models.Find(model => id == model.i);
            if (Model != null)
            {
//                Debug.Log("id " + id + " +");
                return Model;
            }
            else
            {
//                Debug.Log("id " + id + " 1");
                return new EnemySubModel("none", 0);
            }
        }

        public List<EnemySubModel> GetList()
        {
            return Models;
        }

        public int GetDataCount()
        {
            return Models.Count;
        }

        public void Set(string id, long destroyTimeInTicks)
        {
            //todo cache
            if (Models.Find((model => id == model.i)) == null)
            {
                Debug.Log("Models NULL");
                Models.Add(new EnemySubModel(id, destroyTimeInTicks));
            }
            else
            {
                Debug.Log("Models not NULL");
                Models.Find((model => id == model.i)).t = destroyTimeInTicks;
            }
        }
    }
}