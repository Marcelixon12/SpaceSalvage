using UnityEngine;
using System.Collections.Generic;

public class Propellers : MonoBehaviour
{
    public float rotationSpeed = 1500f;
    public Vector3 rotationAxis = Vector3.up;

    private List<Transform> propellers = new List<Transform>();

    void Start()
    {
        
        Transform[] allChildren = GetComponentsInChildren<Transform>();

        foreach (Transform child in allChildren)
        {
            string nameLower = child.name.ToLower();

            
            if (nameLower.Contains("propeller") ||
                nameLower.Contains("rotor") ||
                nameLower.Contains("blade") ||
                nameLower.Contains("screw") ||
                nameLower.Contains("wing"))
            {
                propellers.Add(child);
                
            }
        }

        if (propellers.Count == 0)
        {
            
        }
    }

    void Update()
    {
        // Obracamy niezale¿nie ka¿de znalezione œmig³o kodem C#
        foreach (Transform prop in propellers)
        {
            if (prop != null)
            {
                prop.Rotate(rotationAxis * rotationSpeed * Time.deltaTime, Space.Self);
            }
        }
    }
}
