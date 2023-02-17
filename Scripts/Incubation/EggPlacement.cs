using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using PlayerSystem;
using System.Collections;
using UnityEngine.UI;
namespace Incubation
{

    public class EggPlacement : MonoBehaviour
    {

        [SerializeField] private IncubationProperties incubationProperties;
        [SerializeField] private GameObject eggPrefab;
        [SerializeField] private WormAndEggCountUI wormAndEggCountUI;
        public SaveSO totalEggSave;
        public List<Transform> allNest = new List<Transform>();
        public List<Transform> activeNest = new List<Transform>();
        public List<GameObject> workerChickenList = new List<GameObject>();
        public List<GameObject> totalEgg = new List<GameObject>();
        public Slider hatchingSilider;
        public float placementRate;
        private float placementDelay;
        private int totalEggCount;
        private void Start()
        {
            totalEggCount = totalEggSave.saveInt;
            LoadEggs();
            StartCoroutine(HatchingTirigger());
        }
        public void Placement(List<GameObject> eggs, int listCount, StackEgg stackEgg)
        {
            if (Time.time > placementDelay)
            {
                if (allNest.Count > 0 && eggs.Count > 0)
                {
                    totalEgg.Add(eggs[eggs.Count - 1]);
                    totalEggCount++;
                    totalEggSave.saveInt = totalEggCount;
                    eggs[eggs.Count - 1].transform.DOJump(allNest[0].position, 1, 1, 1);
                    eggs[eggs.Count - 1].transform.parent = allNest[0];
                    Destroy(eggs[eggs.Count - 1].GetComponent<EggStackMove>());
                    stackEgg.followEggsList.Remove(eggs[eggs.Count - 1]);
                    allNest.Remove(allNest[0]);
                    eggs.Remove(eggs[eggs.Count - 1]);
                    stackEgg.currentStackCount--;
                    stackEgg.eggStackCountSO.saveInt = stackEgg.currentStackCount;
                    wormAndEggCountUI.EggCountUI(stackEgg.currentStackCount);
                }
                placementDelay = Time.time + placementRate;
            }

        }
        public IEnumerator HatchingTirigger()
        {
            while (true)
            {

                if (totalEgg.Count > 0)
                {
                    //totalEgg[0].transform.DOMove(activeNest[0].transform.position, .2f);
                    totalEgg[0].GetComponent<EggHatching>().Hatching(incubationProperties.hatchingTime, incubationProperties.chickenPoolType, allNest[0], gameObject, workerChickenList, totalEgg);

                    yield return new WaitForSeconds(incubationProperties.hatchingTime);
                }
                else
                {
                    yield return new WaitForSeconds(.2f);
                }

            }

        }
        private void LoadEggs()
        {
            for (int i = 0; i < totalEggCount; i++)
            {
                GameObject newEgg = Instantiate(eggPrefab, transform.position, Quaternion.identity);
                totalEgg.Add(newEgg);
                newEgg.transform.DOJump(allNest[0].position, 1, 1, 1);
                newEgg.transform.parent = allNest[0];
                allNest.Remove(allNest[0]);
            }
        }
        public void UpgradeNestCount()
        {
            if (allNest.Count > 0)
            {
                allNest[activeNest.Count + 1].gameObject.SetActive(true);
                activeNest.Add(allNest[activeNest.Count + 1]);
                allNest.Remove(allNest[activeNest.Count + 1]);
            }

        }

    }
}
