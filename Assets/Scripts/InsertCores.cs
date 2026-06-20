using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InsertCores : MonoBehaviour
{
    public string CoreID = "GeneratorCore";
    public GameObject Core;
    public GameObject player;
    public Inv_Inventory inventory;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            if (Input.GetKeyDown(KeyCode.E))
            {
                if (inventory.HasItem(CoreID))
                {
                    Core.SetActive(true);
                    inventory.RemoveItem(CoreID);
                    gameObject.GetComponent<BoxCollider>().enabled = false;
                }
                else
                {
                    Debug.Log("Nie posiadasz rdzenia");
                }
            }
        }
    }
}
