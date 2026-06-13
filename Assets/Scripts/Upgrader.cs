using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Upgrader : MonoBehaviour
{
    public CharacterMovement card;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            card.Level += 1;
            Destroy(gameObject);
        }
    }
}
