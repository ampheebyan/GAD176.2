using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DetectionMiddleman : MonoBehaviour
{
    public DetectionBase[] detectionTypes;

    private void OnEnable()
    {
        foreach (DetectionBase detectionType in detectionTypes)
        {
            detectionType.OnDetected += DetectionHandler;
        }
    }

    private void DetectionHandler(object sender, BasePlayer player)
    {
        Debug.Log($"DetectionMiddleman: detected {player.gameObject.name}.");
    }
}
