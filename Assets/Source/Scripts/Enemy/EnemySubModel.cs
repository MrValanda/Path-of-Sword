using System;
using UnityEngine;

namespace Source.Scripts.Enemy
{
    [Serializable]
    public class EnemySubModel
    {
        public long t; //Last Destroying Time in Ticks
        public string i = ""; //id ресурса
        

        public void SetState(long ticksTime)
        {
            t = ticksTime;
        }
        
        public EnemySubModel(string id, long DestroyTimeTicks)
        {
            i = id;
            t = DestroyTimeTicks;
        }
    }
}
