using System.Collections.Generic;
using Base;
using UnityEngine;
using Pathfinding;

namespace AI
{
    public abstract class BaseAI : Unit
    {
        public AIDestinationSetter aIDestinationSetter;
        public AIPath aIPath;
        public Transform baseTarget;
        public int speed; 
        

        public virtual void GoTarget(Transform target)
        {
            aIDestinationSetter.target = target;
        }
        public virtual void ReturnBase()
        {
            aIDestinationSetter.target = baseTarget;

        }

        public virtual void ChangeTarget(Transform target)
        {

            aIDestinationSetter.target = target;

        }

        public virtual void SetSpeed()
        {
            aIPath.maxSpeed = speed;
        }

        public virtual void Attack()
        {
        }

    }
}

