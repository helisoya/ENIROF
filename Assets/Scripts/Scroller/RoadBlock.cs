using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoadBlock : MonoBehaviour
{
    [SerializeField] private Renderer[] renderers;

    public void SetCurveValue(float value)
    {
        foreach (Renderer renderer in renderers)
        {
            foreach (Material material in renderer.materials)
            {
                material.SetFloat("_Strength_X", value);
            }
        }
    }
}
