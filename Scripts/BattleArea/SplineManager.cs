using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.U2D;

public class SplineManager : MonoBehaviour
{
    public float offsetValue = 7f;
    public float smoothingFactor = 3f;

    public SpriteAreaTest spriteAreaTest;
    private SpriteShapeController spriteShapeController;

    private List<TroopController> troops;

    private Vector3 clockWiseTangentDirectionLeft = new Vector3(-1, -1, 0);
    private Vector3 clockWiseTangentDirectionRight = new Vector3(1, 1, 0);
    private Vector3 counterClockWiseTangentDirectionLeft = new Vector3(1, -1, 0);
    private Vector3 counterClockWiseTangentDirectionRight = new Vector3(-1, 1, 0);


    private void Awake()
    {
        spriteShapeController = GetComponent<SpriteShapeController>();
    }

    void Start()
    {
        troops = spriteAreaTest.troops;
        Debug.Log("Troops count on SplineManager: " + troops.Count);
        SetTangentsOfMainSplines();
    }

    public void UpdateSplines()
    {
        Vector3 min = new Vector3(float.MaxValue, 0, float.MaxValue);
        Vector3 max = new Vector3(float.MinValue, 0, float.MinValue);

        foreach (TroopController troop in troops)
        {
            Vector3 position = troop.transform.localPosition;
            Debug.Log("Troop: " + troop.name + " position: " + position);
            min.x = Mathf.Min(min.x, position.x);
            min.z = Mathf.Min(min.z, position.z);
            max.x = Mathf.Max(max.x, position.x);
            max.z = Mathf.Max(max.z, position.z);
            Debug.Log("Min and Max Values: " + min + " - " + max);
        }
        min -= new Vector3(offsetValue, 0, offsetValue);
        max += new Vector3(offsetValue, 0, offsetValue);
        Debug.Log("Min and Max Values After Offset Change: " + min + " - " + max);

        MoveControlPoint(0, new Vector3(max.x, min.z, 0)); // BOTTOM RIGHT
        MoveControlPoint(1, new Vector3(min.x, min.z, 0)); // BOTTOM LEFT
        MoveControlPoint(2, new Vector3(min.x, max.z, 0)); // TOP LEFT
        MoveControlPoint(3, new Vector3(max.x, max.z, 0)); // TOP RIGHT
    }



    public int GetSplineCount()
    {
        return spriteShapeController.spline.GetPointCount();
    }

    public void AddControlPoint(Vector3 position)
    {
        spriteShapeController.spline.InsertPointAt(spriteShapeController.spline.GetPointCount(), position);
    }

    public void RemoveControlPoint()
    {
        if (spriteShapeController.spline.GetPointCount() > 0)
        {
            spriteShapeController.spline.RemovePointAt(1);
        }
    }

    public void MoveControlPoint(int index, Vector3 position)
    {
        var splinePointBeforeChanged = spriteShapeController.spline.GetPosition(index);
        if (index >= 0 && index < spriteShapeController.spline.GetPointCount())
        {
            spriteShapeController.spline.SetPosition(index, position);
            Debug.Log("Spline position at index " + index + " changed from " + splinePointBeforeChanged + " to " + position);
        }
    }
    public void SetTangentsOfMainSplines()
    {
        for (int i = 0; i < spriteShapeController.spline.GetPointCount(); i++)
        {
            spriteShapeController.spline.SetTangentMode(i, ShapeTangentMode.Broken);
            ReverseTangentVectors();
            if (i % 2 == 0)
            {
                SetTangents(i, (smoothingFactor * clockWiseTangentDirectionLeft), (smoothingFactor * clockWiseTangentDirectionRight));
            }
            else
            {
                SetTangents(i, (smoothingFactor * counterClockWiseTangentDirectionLeft), (smoothingFactor * counterClockWiseTangentDirectionRight));
                ReverseTangentVectors();
            }
        }
    }
    public void SetTangents(int index, Vector3 leftTangent, Vector3 rightTangent)
    {
        if (index >= 0 && index < spriteShapeController.spline.GetPointCount())
        {
            spriteShapeController.spline.SetLeftTangent(index, leftTangent);
            spriteShapeController.spline.SetRightTangent(index, rightTangent);
        }
    }
    public void SetAllTangentsLinear()
    {
        for (int i = 0; i < spriteShapeController.spline.GetPointCount(); i++)
        {
            spriteShapeController.spline.SetTangentMode(i, ShapeTangentMode.Linear);
        }
    }

    public void SetAllTangentsBroken()
    {
        for (int i = 0; i < spriteShapeController.spline.GetPointCount(); i++)
        {
            spriteShapeController.spline.SetTangentMode(i, ShapeTangentMode.Broken);
        }
    }

    public void ReverseTangentVectors()
    {
        clockWiseTangentDirectionLeft *= -1;
        clockWiseTangentDirectionRight *= -1;
        counterClockWiseTangentDirectionLeft *= -1;
        counterClockWiseTangentDirectionRight *= -1;
    }
}
