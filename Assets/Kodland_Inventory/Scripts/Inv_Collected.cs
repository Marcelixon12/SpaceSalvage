using UnityEngine;

public class Inv_Collected : MonoBehaviour
{
    public string itemName;
    public Sprite image;
    private Inv_Inventory inventory;

    private void Start()
    {
        inventory = FindObjectOfType<Inv_Inventory>();
    }

    private void OnTriggerEnter(Collider other)
    {   
        if (other.gameObject.CompareTag("Player"))
            inventory.AddItem(image, itemName, gameObject);
    }

}
