using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TroopController : MonoBehaviour
{
    public List<GameObject> units;

    void Start()
    {
        units = new List<GameObject>();
        foreach (Transform child in transform)
        {
            units.Add(child.gameObject);
        }
    }

    public void RemoveUnit(GameObject unit)
    {
        units.Remove(unit);
    }

    public bool IsEmpty()
    {
        return units.Count == 0;
    }

    public bool IsGroupDestroyed()
    {
        foreach (GameObject unit in units)
        {
            if (unit != null)
            {
                return false;
            }
        }
        return true;
    }
}
