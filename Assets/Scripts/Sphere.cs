using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sphere : MonoBehaviour
{
    public Manager manager;

    public int Index = -1;
   
    
    private void Start()
    {
        manager = FindObjectOfType<Manager>();
        
    }

    public Color GetSphereColor()
    {
        
        return GetComponent<MeshRenderer>().material.GetColor("_Color");
    }
    
  

    public void SetSphereColor(Color newColor)
    {
        GetComponent<MeshRenderer>().material.SetColor("_Color",newColor);
    }
}
