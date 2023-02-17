using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Worm;
public class WorkerChickenStack : MonoBehaviour
{
    public List<WormController> wormStack = new List<WormController>();
    [SerializeField] private Transform wormPoint;

    public void AddToStack(WormController wormController)
    {
        wormStack.Add(wormController);

    }
}
