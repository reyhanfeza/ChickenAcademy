using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Worm;
namespace WorkerChickenAI
{
    public class PickUpWorm : MonoBehaviour
    {
        
        [SerializeField] WorkerChickenStack stackWorm;
        [SerializeField] WorkerChickenAI workerChickenAI;
        private WormController _currentWorm;
        private bool _isPulling=false;
        private void OnTriggerEnter(Collider other)
        {
          
            if (other.TryGetComponent(out WormController wormController) && !_isPulling)
            {
                
                var isPulling = wormController.TryPull(WorkerChickenProperties.Instance.level, transform, OnWormPicked);
                _currentWorm = wormController;

                if (isPulling)
                {

                    //workerChickenAI.currentStackCount++;
                    _isPulling = true;
                }
                else
                {
                    workerChickenAI.currentStackCount--;
                    return;
                }
            }
        }
        private void OnWormPicked()
        {
            _isPulling = false;
            stackWorm.AddToStack(_currentWorm);
            _currentWorm = null;
        }
    }

}
