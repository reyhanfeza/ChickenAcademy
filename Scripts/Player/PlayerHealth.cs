using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
namespace PlayerSystem
{
    public class PlayerHealth : MonoBehaviour
    {
        public float maxHealth;
        
        [SerializeField] private float healingRatio = 0.1f; //heals 10% of maxHealth per second
        [SerializeField] private float healingDelay = 2.5f; //seconds before healing starts
        [SerializeField] private Image healthFillImage;
        
        private float currentHealth;
        private float lastDamageTime = 0f;
        private PlayerProperties properties;
        private Coroutine healingCoroutine;

        void Start()
        {
            properties = GetComponent<PlayerProperties>();
            UpdateValue();

        }
        
        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.H))
            {
                TakeDamage(20);
            }
        }

        public void TakeDamage(float damage)
        {
            currentHealth -= damage;
            healthFillImage.DOFillAmount(currentHealth/maxHealth, .2f);
            properties.currentHealth = currentHealth;
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
            currentHealth += healAmount;
            healthFillImage.DOFillAmount(currentHealth/maxHealth,.2f);
            if (currentHealth > maxHealth)
            {
                currentHealth = maxHealth;
            }
            properties.currentHealth = currentHealth;
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
        public void UpdateValue()
        {         
            maxHealth = properties.maxHealth;
            currentHealth = maxHealth;
            healthFillImage.fillAmount = currentHealth / maxHealth;
        }
        
    }
}


