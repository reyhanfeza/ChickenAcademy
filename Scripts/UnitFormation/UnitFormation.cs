using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

public class UnitFormation : MonoBehaviour
{
    public bool clearOnStart = false;
    public Transform[] inactiveUnits;
    public List<Transform> units;
    private Vector3[] positions;

    [Range(1,20)]public int formationWidth = 3;
    //public float formationDepth = 10.0f;
    public Vector3 offset = Vector3.zero;
    public float spacing = 2f;

    public float updateInterval = 0.016f;

    private void Start()
    {
        positions = new Vector3[units.Count];
        formationWidth = Mathf.Max(formationWidth, 1);
        //formationDepth = Mathf.Max(formationDepth, 1); // vertical formation

        if(clearOnStart)
            ResetUnitSize();
        
        StartCoroutine(UpdateFormation());
    }

    private IEnumerator UpdateFormation()
    {
        while (true)
        {
            UpdatePositions();
            ApplyPositions();
            yield return new WaitForSeconds(updateInterval);
        }
    }

    public void AddUnit(Transform unit)
    {
        units.Add(unit);
    }

    public void RemoveUnit()
    {
        units.RemoveAt(units.Count-1);
    }

    public void ResetUnitSize()
    {
        units.Clear();
    }
    
    public void IncreaseSize()
    {
        units.Add(inactiveUnits[units.Count]);
    }
    
    private void UpdatePositions()
    {
        float centerX = (formationWidth - 1) * spacing / 2;
        float centerZ = ((units.Count / formationWidth) - 1) * spacing / 2;
        for (int i = 0; i < units.Count; i++)
        {
            float xPos = (i % formationWidth) * spacing + offset.x - centerX;
            int row = ((int)(i / formationWidth));
            float zPos = row * spacing + offset.z - centerZ;

            float yPos = offset.y; // no vertical formation
            //float yPos = (i / (formationWidth * formationDepth)) * spacing + offset.y; // vertical formation

            positions[i] = new Vector3(xPos, yPos, zPos);
        }
    }

    private void ApplyPositions()
    {
        if(positions==null)
            return;
        for (int i = 0; i < units.Count; i++)
        {
            units[i].localPosition = positions[i];
        }
    }
#if UNITY_EDITOR
    private void OnDrawGizmos()
    {
        
        int length = units.Count / formationWidth * Convert.ToInt32(spacing);
        int width = formationWidth * Convert.ToInt32(spacing);
        Gizmos.color = new Color(0f, .5f, 1f, 0.25f);
        if(gameObject.name.Contains("Enemy"))
            Gizmos.color = new Color(1f, 0f, 0, 0.25f);
        Gizmos.DrawCube(transform.position,new Vector3(width,0.2f,length));
        UnityEditor.Handles.color = Color.white;
        UnityEditor.Handles.Label(transform.position+new Vector3(-1f,1f,0f), gameObject.name );

    }
#endif
}
