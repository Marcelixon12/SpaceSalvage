using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class SaveData 
{
    //Pozycja 
    public float posX;
    public float posY;
    public float posZ;

    //Rotacja
    public float rotX;
    public float rotY;
    public float rotZ;

    //Przedmioty z ekwipunku
    public List<string> savedItems = new List<string>();

    // Statystyki 
    public int battery;
    public float oxygen;
    public int cardLevel;
    // Zebrane przedmioty
    public List<string> collectedBottles = new List<string>();
    public List<string> collectedNotes = new List<string>();
    public List<string> collectedUpgraders = new List<string>();
    //Otworzone skrzynie
    public List<string> openedBoxes = new List<string>();
    //Rdzenie
    public List<string> activeCores = new List<string>();
    public List<string> collectedCoresOnGround = new List<string>();
    //Maska
    public bool helmetPut;
    //£adowarki
    public List<string> chargersNames = new List<string>();
    public List<int> chargersBattery = new List<int>();

}
