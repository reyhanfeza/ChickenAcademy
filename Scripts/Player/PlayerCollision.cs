using UnityEngine;
using MotherChicken;
using Incubation;
using Worm;

namespace PlayerSystem
{
    public class PlayerCollision : MonoBehaviour
    {
        private SpawnEggMC motherChicken;
        private StackEgg playerStack;
        private PlayerProperties _properties;
        private PlayerAttack playerAttack;
        private PlayerSM playerSM;
        private PlayerExperience _playerExperience;
        [SerializeField] private StackWorm stackWorm;
        [SerializeField] private WormAndEggCountUI wormAndEggCountUI;
        private void Start()
        {
            playerSM = GetComponent<PlayerSM>();
            playerStack = GetComponent<StackEgg>();
            _properties = GetComponent<PlayerProperties>();
            playerAttack = GetComponent<PlayerAttack>();
            _playerExperience = GetComponent<PlayerExperience>();
        }
        private void OnTriggerEnter(Collider other)
        {


            if (other.TryGetComponent(out WormSlot wormSlot))
            {
                if (stackWorm.wormStack.Count < _properties.wormStackCount)
                {
                    var targetTransform = wormSlot.TryGetTarget();

                    if (targetTransform != null)
                    {
                        playerSM.rotationTargetTransform = targetTransform;
                        playerSM.ChangeState(playerSM.stPlayerAttack);
                    }
                }
                else
                {
                    wormAndEggCountUI.FeedBackFullStack("Worm");
                }
               
            }
        }
        private void OnTriggerStay(Collider other)
        {
            if (other.TryGetComponent(out EggPlacement eggPlacement))
            {
                eggPlacement.Placement(playerStack.eggsList, playerStack.eggsList.Count, playerStack);
            }

            if (other.TryGetComponent(out SpawnEggMC spawnEggMc))
            {
                motherChicken = spawnEggMc;
                playerStack.Stack(motherChicken.eggList, motherChicken.eggList.Count);
                spawnEggMc.currentEggCount = 1;
                spawnEggMc.PrintEggCount();
            }

            if (other.TryGetComponent(out EatWorms eatWorms))
            {
                for (int i = 0; i < stackWorm.wormStack.Count; i++)
                {
                    eatWorms.EatWorm(stackWorm.wormStack[0].foodValue, _properties.income, stackWorm.wormStack[0].gameObject);
                    _playerExperience.AddExperience(stackWorm.wormStack[0].foodValue);
                    stackWorm.wormStack.Remove(stackWorm.wormStack[0]);
                    stackWorm.WormCountUI();
                }

            }
        }
    }

}
