using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThiefFarmer : MonoBehaviour
{
    [SerializeField] private float speed = 2f;
    [SerializeField] private float chickenSearchInterval = 1f;

    [SerializeField] private GameObject searchArea;
    private Collider searchAreaCollider;

    private GameObject[] chickens;
    private GameObject targetChicken;

    private float reachDistance = 0.1f;

    private ThiefFarmerAnimations thiefFarmerAnimations;

    void Start()
    {
        searchAreaCollider = searchArea.GetComponent<Collider>();
        thiefFarmerAnimations = GetComponent<ThiefFarmerAnimations>();
        thiefFarmerAnimations.RunAnimation();
        StartCoroutine(FindChickens());
    }
    IEnumerator FindChickens()
    {
        while (true)
        {
            chickens = GameObject.FindGameObjectsWithTag("WorkerChicken");
            targetChicken = null;
            if (chickens.Length > 0)
            {
                float minDistance = float.MaxValue;
                foreach (GameObject chicken in chickens)
                {
                    Debug.Log("Is this position: " + chicken.transform.position + " in the collider area: " + searchAreaCollider.bounds.Contains(chicken.transform.position));
                    if (searchAreaCollider.bounds.Contains(chicken.transform.position))
                    {
                        Debug.Log("Gameobject " + chicken + " is in kidnapping area! ");
                        float dist = Vector3.Distance(chicken.transform.position, transform.position);
                        if (dist < minDistance)
                        {
                            minDistance = dist;
                            targetChicken = chicken;
                        }
                    }
                }
            }
            yield return new WaitForSeconds(chickenSearchInterval);
        }
    }

    void Update()
    {
        if (targetChicken != null)
        {
            // Update rotation towards chicken
            Vector3 direction = (targetChicken.transform.position - transform.position).normalized;
            Quaternion lookRotation = Quaternion.LookRotation(new Vector3(direction.x, 0, direction.z));
            transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * 5f);

            // Move towards chicken
            transform.position = Vector3.MoveTowards(transform.position, targetChicken.transform.position, speed * Time.deltaTime);
            // If farmer reached target, run animation
            if (IsFarmerReachedTarget())
            {
                //Debug.Log("Animation will be started...");
                //thiefFarmerAnimations.PickupAnimation();
            }
        }
    }

    private bool IsFarmerReachedTarget()
    {
        return Vector3.Distance(transform.position, targetChicken.transform.position) <= reachDistance;
    }
}