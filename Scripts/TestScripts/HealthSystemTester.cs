using Health;
using UnityEngine;

namespace TestScripts
{
    public class HealthSystemTester : MonoBehaviour
    {
        public HealthSystem healthSystem;
        public float damageAmount = 10f;

        void Update()
        {
            if (Input.GetKeyDown(KeyCode.T))
            {
                healthSystem.TakeDamage(damageAmount);
            }
        }
    }
}