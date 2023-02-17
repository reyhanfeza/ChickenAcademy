using System;
using UnityEngine;

namespace Base
{
    public abstract class Unit : MonoBehaviour
    {
        public float health;
        public int level;
        public float experience;
        public float targetExperience;
        
        private float _maxHealth;

        protected virtual void Awake()
        {
            _maxHealth = health;
        }


        protected virtual void AddExperience(float exp)
        {
            experience += exp;
            if (experience >= targetExperience)
            {
                LevelUp();
            }
        }

        protected virtual void LevelUp()
        {
            experience = 0;
            level = level+1;
        }

        protected virtual void TakeDamage(float value)
        {
            health -= value;
            if (health <= 0)
            {
                Die();
            }
        }

        protected virtual void Die()
        {
            health = 0;
        }

        protected virtual void ResetUnitAttributes()
        {
            health = _maxHealth;
            experience = 0;
        }
    }

}
