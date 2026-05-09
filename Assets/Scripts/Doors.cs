using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Doors : MonoBehaviour
{
   
    public float speed = 4f;
    AudioSource a;
    public AudioClip clip;
    // Start is called before the first frame update
    void Start()
    {
       
        a = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            transform.position = new Vector3(transform.position.x, transform.position.y - 3.3f, transform.position.z);
            a.PlayOneShot(clip);
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            transform.position = new Vector3(transform.position.x, transform.position.y + 3.3f, transform.position.z);
            a.PlayOneShot(clip);
        }
    }

}
