using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;


public class UI : MonoBehaviour
{
    public string code = "";
    public GameObject Note1;
    public GameObject Note2;
    public GameObject Note3;
    public GameObject Note4;
    public GameObject Note5;
    public GameObject Note6;
    public GameObject Note7;
    public GameObject Note8;
    public GameObject Note9;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void Code1()
    {
        
        code += "1";
    }
    public void Code2()
    {

        code += "2";
    }
    public void Code3()
    {

        code += "3";
    }
    public void Code4()
    {

        code += "4";
    }
    public void Code5()
    {

        code += "5";
    }
    public void Code6()
    {

        code += "6";
    }
    public void Code7()
    {

        code += "7";
    }
    public void Code8()
    {

        code += "8";
    }
    public void Code9()
    {

        code += "9";
    }
    public void Code0()
    {

        code += "0";
    }
    public void Note1Exit()
    {
        Note1.SetActive(false);
    }
    public void Note2Exit()
    {
        Note2.SetActive(false);
    }
    public void Note3Exit()
    {
        Note3.SetActive(false);
    }
    public void Note4Exit()
    {
        Note4.SetActive(false);
    }
    public void Note5Exit()
    {
        Note5.SetActive(false);
    }
    public void Note6Exit()
    {
        Note6.SetActive(false);
    }
    public void Note7Exit()
    {
        Note7.SetActive(false);
    }
    public void Note8Exit()
    {
        Note8.SetActive(false);
    }
    public void Note9Exit()
    {
        Note9.SetActive(false);
    }
}
