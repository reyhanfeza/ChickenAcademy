using System.Collections;
using UnityEngine;

namespace Health
{
    public class HealthSystem : MonoBehaviour
    {
        [SerializeField] private float maxHealth = 100f;
        [SerializeField] private float currentHealth;
        [SerializeField] private float healingRatio = 0.1f; //heals 10% of maxHealth per second
        [SerializeField] private float healingDelay = 2.5f; //seconds before healing starts
        private float lastDamageTime = 0f;
        private Coroutine healingCoroutine;

        void Start()
        {
            currentHealth = maxHealth;
        }

        public void TakeDamage(float damage)
        {
            currentHealth -= damage;
            lastDamageTime = Time.time;
            if (currentHealth <= 0)
            {
                Die();
            }
            else
            {
                StopHealing();
                healingCoroutine = StartCoroutine(HealingCoroutine());
            }
        }

        public void Heal(float healAmount)
        {
            Debug.Log("Healing by amount of " + healAmount);
            currentHealth += healAmount;
            if (currentHealth > maxHealth)
            {
                currentHealth = maxHealth;
            }
        }

        IEnumerator HealingCoroutine()
        {
            yield return new WaitForSeconds(healingDelay);
            while (currentHealth < maxHealth)
            {
                if (Time.time - lastDamageTime >= healingDelay)
                {
                    Heal(maxHealth * healingRatio);
                }
                yield return new WaitForSeconds(1f);
            }

        }
        private void StopHealing()
        {
            if (healingCoroutine != null)
            {
                StopCoroutine(healingCoroutine);
            }
        }

        void Die()
        {
            Debug.Log("Gameobject dead");
            // Handle death
        }
    }
}