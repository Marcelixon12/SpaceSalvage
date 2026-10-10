using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Screenshots : MonoBehaviour
{
    public GameObject[] lightsOn;
    // Start is called before the first frame update
    void Start()
    {
        TurnOffAllLights();
        for (int i = 0; i < lightsOn.Length; i++)
        {
            lightsOn[i].GetComponent<Light>().enabled = true;
        }
    }

    // Update is called once per frame
    void Update()
    {

    }
    public void TurnOffAllLights()
    {
        // Znajduje wszystkie œwiat³a (aktywne i nieaktywne) w scenie
        Light[] allLights = FindObjectsByType<Light>(FindObjectsInactive.Include, FindObjectsSortMode.None);

        foreach (Light l in allLights)
        {
            l.enabled = false;
        }


    }
}
