using System.Collections.Generic;
using UnityEngine;

namespace PlayerSystem
{
    public class StackEgg : MonoBehaviour
    {
        [SerializeField] private GameObject StackPoint;
        [SerializeField] private PlayerProperties properties;
        [SerializeField] private float stackDistance;
        [SerializeField] private GameObject eggPrefab;
        [SerializeField] private WormAndEggCountUI wormAndEggCountUI;
        public SaveSO eggStackCountSO;
        public int currentStackCount;
        public List<GameObject> eggsList = new List<GameObject>();
        public List<GameObject> followEggsList = new List<GameObject>();
        private int maxStackCount;

        private void Start()
        {
            currentStackCount = eggStackCountSO.saveInt;
            maxStackCount = properties.eggStackCount;
            LoadStackEgg();
            wormAndEggCountUI.EggCountUI(currentStackCount);
        }

        public void Stack(List<GameObject> eggs, int listCount)
        {

            for (int i = 0; i < listCount; i++)
            {

                if (currentStackCount <= maxStackCount)
                {

                    //eggs[0].transform.SetParent(StackPoint.transform);
                    //eggs[0].transform.rotation = Quaternion.identity;
                    eggs[0].transform.parent = null;
                    eggsList.Add(eggs[0]);
                    followEggsList.Add(eggs[0]);
                    eggs[0].transform.position = new Vector3(StackPoint.transform.position.x, StackPoint.transform.position.y + currentStackCount * stackDistance, StackPoint.transform.position.z);
                    eggs[0].GetComponent<EggStackMove>().FollowStack(followEggsList[eggsList.Count-1].transform,true);
                    eggs.Remove(eggs[0]);
                    currentStackCount++;
                    eggStackCountSO.saveInt = currentStackCount;
                    wormAndEggCountUI.EggCountUI(currentStackCount);
                }
                else
                {
                    wormAndEggCountUI.FeedBackFullStack("Egg");
                }

            }
        }
        private void LoadStackEgg()
        {
            for (int i = 0; i < currentStackCount; i++)
            {
                GameObject newEgg = Instantiate(eggPrefab, transform.position, Quaternion.identity);
                eggsList.Add(newEgg);
                followEggsList.Add(newEgg);
                newEgg.transform.position = new Vector3(StackPoint.transform.position.x, StackPoint.transform.position.y + i*stackDistance, StackPoint.transform.position.z);
                newEgg.GetComponent<EggStackMove>().FollowStack(followEggsList[eggsList.Count - 1].transform, true);
               
            }
        }
    }


}
