using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.U2D;

public class SpriteAreaTest : MonoBehaviour
{
    [SerializeField] private SpriteShapeController battleAreaSprite;

    private SplineManager splineManager;

    public float executeDelay = 2f;

    public List<TroopController> troops;

    private List<GameObject> allUnits;

    private float debugDelay = 1f;

    void Start()
    {
        allUnits = new List<GameObject>();

        splineManager = battleAreaSprite.GetComponent<SplineManager>();

        SetAllUnits();
        Debug.Log(allUnits.Count);

        splineManager.UpdateSplines();
        StartCoroutine(DebugCoroutine());
    }

    private IEnumerator DebugCoroutine()
    {
        Debug.Log("All units count: " + allUnits.Count);
        yield return new WaitForSeconds(debugDelay);
    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Q))
        {
            if (allUnits.Count > 0)
            {
                int randomIndex = Random.Range(0, allUnits.Count);
                GameObject unitToDestroy = allUnits[randomIndex];
                allUnits.RemoveAt(randomIndex);

                TroopController selectedTroop = unitToDestroy.GetComponentInParent<TroopController>();
                selectedTroop.RemoveUnit(unitToDestroy);

                Destroy(unitToDestroy);

                if (selectedTroop.IsEmpty())
                {
                    splineManager.UpdateSplines();
                    troops.Remove(selectedTroop);
                }
            }
        }
    }
    private void SetAllUnits()
    {
        Debug.Log("Troops count: " + troops.Count);
        foreach (TroopController troop in troops)
        {
            foreach (GameObject unit in troop.units)
            {
                allUnits.Add(unit);
            }
        }
    }
}