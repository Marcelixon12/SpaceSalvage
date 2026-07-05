using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AI : MonoBehaviour
{
    public GameObject[] dronsSpawns;
    public GameObject[] spidersSpawns;
    public GameObject player;
    public GameObject dron;
    public GameObject spider;
    public int maxDrons = 2;
    public float moveInterval = 5f;

    private GameObject[] spawnedDrons;
    private float timer = 0f;
    public int maxSpiders = 2;
    private GameObject[] spawnedSpiders;

    void Start()
    {
        spawnedDrons = new GameObject[dronsSpawns.Length];
        spawnedSpiders = new GameObject[spidersSpawns.Length];
    }

    void Update()
    {
        for (int i = 0; i < dronsSpawns.Length; i++)
        {
            GameObject spawn = dronsSpawns[i];

            float distX = Mathf.Abs(player.transform.position.x - spawn.transform.position.x);
            float distZ = Mathf.Abs(player.transform.position.z - spawn.transform.position.z);

            if (distX <= 10 && distZ <= 10 && CountActiveDrons() < maxDrons && spawnedDrons[i] == null)
            {
                GameObject newDron = Instantiate(dron, spawn.transform.position, Quaternion.identity);
                spawnedDrons[i] = newDron;
            }
        }
        MoveDrons();
        for (int i = 0; i < spidersSpawns.Length; i++)
        {
            GameObject spawn = spidersSpawns[i];

            float distX = Mathf.Abs(player.transform.position.x - spawn.transform.position.x);
            float distZ = Mathf.Abs(player.transform.position.z - spawn.transform.position.z);

            if (distX <= 10 && distZ <= 10 && CountActiveSpiders() < maxSpiders && spawnedSpiders[i] == null)
            {
                GameObject newSpider = Instantiate(spider, spawn.transform.position, Quaternion.identity);
                spawnedSpiders[i] = newSpider;
            }
        }
        MoveSpiders();



    }

    void MoveDrons()
    {
        for (int i = 0; i < spawnedDrons.Length; i++)
        {
            if (spawnedDrons[i] == null) continue;


            float currentDist = Vector3.Distance(new Vector3(dronsSpawns[i].transform.position.x, 0, dronsSpawns[i].transform.position.z), new Vector3(player.transform.position.x, 0, player.transform.position.z));


           

            int bestSpawn = -1;
            float bestDist = currentDist; 

            
            for (int j = 0; j < dronsSpawns.Length; j++)
            {
                if (spawnedDrons[j] != null) continue; 
                if (j == i) continue;

                float dist = Vector3.Distance(new Vector3(dronsSpawns[j].transform.position.x, 0, dronsSpawns[j].transform.position.z), new Vector3(player.transform.position.x, 0, player.transform.position.z));


              

                if (dist < bestDist) 
                {
                    bestDist = dist;
                    bestSpawn = j;
                }
            }

            
            if (bestSpawn != -1)
            {
                spawnedDrons[i].transform.position = dronsSpawns[bestSpawn].transform.position;
                spawnedDrons[bestSpawn] = spawnedDrons[i];
                spawnedDrons[i] = null;
            }
        }
    }
    void MoveSpiders()
    {
        for (int i = 0; i < spawnedSpiders.Length; i++)
        {
            if (spawnedSpiders[i] == null) continue;


            float currentDist = Vector3.Distance(new Vector3(spidersSpawns[i].transform.position.x, 0, spidersSpawns[i].transform.position.z), new Vector3(player.transform.position.x, 0, player.transform.position.z));



            int bestSpawn = -1;
            float bestDist = currentDist;


            for (int j = 0; j < spidersSpawns.Length; j++)
            {
                if (spawnedSpiders[j] != null) continue;
                if (j == i) continue;

                float dist = Vector3.Distance(new Vector3(spidersSpawns[j].transform.position.x, 0, spidersSpawns[j].transform.position.z), new Vector3(player.transform.position.x, 0, player.transform.position.z));


               

                if (dist < bestDist)
                {
                    bestDist = dist;
                    bestSpawn = j;
                }
            }


            if (bestSpawn != -1)
            {
                spawnedSpiders[i].transform.position = spidersSpawns[bestSpawn].transform.position;
                spawnedSpiders[bestSpawn] = spawnedSpiders[i];
                spawnedSpiders[i] = null;
            }
        }
    }

    int CountActiveDrons()
    {
        int count = 0;
        foreach (GameObject d in spawnedDrons)
        {
            if (d != null) count++;
        }
        return count;
    }
    int CountActiveSpiders()
    {
        int count = 0;
        foreach (GameObject d in spawnedSpiders)
        {
            if (d != null) count++;
        }
        return count;
    }
}
