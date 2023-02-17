using System;
using System.Collections;
using System.Collections.Generic;
using ObjectPooling;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
namespace Incubation
{

    public class EggHatching : MonoBehaviour
    {
        Slider hatchingSilider;
        public void Hatching(float hatchingTime, String chickenTypeTag, Transform nest, GameObject incubationFarm, List<GameObject> workerChickenList, List<GameObject> totalEgg)
        {
            StartCoroutine(HatchingTimer(hatchingTime, chickenTypeTag, nest, incubationFarm, workerChickenList,totalEgg));
        }
        public IEnumerator HatchingTimer(float hatchingTime, string chickenTypeTag, Transform nest, GameObject incubationFarm, List<GameObject> workerChickenList, List<GameObject> totalEgg)
        {
            hatchingSilider = incubationFarm.GetComponent<EggPlacement>().hatchingSilider;
            hatchingSilider.DOValue(hatchingSilider.maxValue, hatchingTime);
            yield return new WaitForSeconds(hatchingTime);

            var chicken = ObjectPooler.instance.SpawnFromPool(chickenTypeTag, transform.position, Quaternion.identity);
            if (chicken.TryGetComponent(out SoldierChickenAI soldier))
            {
                soldier.InsertIntoSquad();
                LoadWorkerAndSoldierChicken.Instance.soldierChickenCount.saveInt++;
            }
            else
            {
                workerChickenList.Add(chicken);
                LoadWorkerAndSoldierChicken.Instance.workerChickenCount.saveInt++;
            }
            incubationFarm.GetComponent<EggPlacement>().allNest.Add(nest);
            totalEgg.Remove(gameObject);
            incubationFarm.GetComponent<EggPlacement>().totalEggSave.saveInt = totalEgg.Count;
            transform.parent = null;
            gameObject.SetActive(false);
            hatchingSilider.value = 0;

        }
       

    }
}
