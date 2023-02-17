using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
namespace MotherChicken
{
    public class SpawnEggMC : MonoBehaviour
    {
        [SerializeField] private GameObject eggPrefab;
        [SerializeField] private MotherChickenAnimations motherAnimation;
        [SerializeField] private Transform eggCountTransform;
        [SerializeField] private TextMeshProUGUI eggCountText;
        public int currentEggCount;
        public int totalEggCount;
        public List<GameObject> eggsPoint = new List<GameObject>();
        public List<GameObject> eggList = new List<GameObject>();
        private void Start()
        {
            PrintEggCount();
        }
        private void Update()
        {
            eggCountTransform.position = Camera.main.WorldToScreenPoint(transform.position + Vector3.up*5);
        }
        public void SpawnEgg()
        {
            GameObject newEgg = Instantiate(eggPrefab, transform.position, Quaternion.identity);
            eggList.Add(newEgg);
            motherAnimation.SpawnEggAnimation();
            currentEggCount++;
            if (currentEggCount > eggsPoint.Count)
            {
                currentEggCount = 1;
            }
            StackNest(newEgg);
            PrintEggCount();
        }

        void StackNest(GameObject egg)
        {
            egg.transform.SetParent(eggsPoint[currentEggCount - 1].transform);
            egg.transform.rotation = eggsPoint[currentEggCount - 1].transform.rotation;
            egg.transform.position = eggsPoint[currentEggCount - 1].transform.position;
        }
        public void PrintEggCount()
        {
            if (currentEggCount<=0)
            {
                eggCountText.text = currentEggCount.ToString() + "/" + eggsPoint.Count.ToString();
            }
            else
            {
                eggCountText.text = (currentEggCount - 1).ToString() + "/" + eggsPoint.Count.ToString();
            }
           
        }
    }
}

