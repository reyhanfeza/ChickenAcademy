using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MotherChicken;

namespace WorkerChickenAI
{
    public class WorkerChickenCollision : MonoBehaviour
    {
        [SerializeField] private WorkerChickenStack stackWorm;
        [SerializeField] WorkerChickenAI workerChickenAI;
        private void OnTriggerEnter(Collider other)
        {
            if (other.TryGetComponent(out EatWorms eatWorms))
            {
                for (int i = 0; i < stackWorm.wormStack.Count; i++)
                {
                    eatWorms.EatWorm(stackWorm.wormStack[0].foodValue, 0, stackWorm.wormStack[0].gameObject);
                    stackWorm.wormStack.Remove(stackWorm.wormStack[0]);
                   
                }
                workerChickenAI.currentStackCount = 0;
            }
        }
    }

}
