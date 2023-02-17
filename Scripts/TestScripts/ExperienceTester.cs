using Experience;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExperienceTester : MonoBehaviour
{
    public ExperienceSystem experienceSystem;
    public float experienceAmount = 10f;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            experienceSystem.AddExperience(experienceAmount);
        }
    }
}
