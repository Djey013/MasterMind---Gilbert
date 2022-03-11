using System;
using UnityEngine;
using Random = UnityEngine.Random;

public class Manager : MonoBehaviour
{
    private Color[] _colors = new Color[] {Color.red, Color.green, Color.yellow, Color.blue,};
    
    public Color[] answer = new Color[4];

    public Line[] lines;

    private int _actualLineNumber = -1;
    private Sphere _actualSphere;
    
    private void Start()
    {
        for (int i = 0; i < answer.Length; i++)
        {
            answer[i] = _colors[Random.Range(0, 4)];
        }
        ActivateNewLine();
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            MouseRaycast();
        }

        if (Input.GetKeyDown(KeyCode.Return))
        {
            CheckLine();
            ActivateNewLine();
        }
    }

    private void CheckLine() // vérification de la ligne active
    {
        int goodSphereNumber = 0;
        for (int i = 0; i < answer.Length; i++) // On teste chaque couleur
        {
            if (_colors[lines[_actualLineNumber].spheres[i].Index] == answer[i]) // On Vérifie d'abord si la couleur est bien placée
            {
                Debug.Log("la boule n°" + i+" est bien placée !");
                goodSphereNumber++;
            }
            else
            {
                for (int j = 0; j < answer.Length; j++) // On vérifie ensuite si la couleur est juste mais mal placée
                {
                    if (_colors[lines[_actualLineNumber].spheres[i].Index] == answer[j]) 
                    {
                        Debug.Log("la boule n°" + i+" est mal placée !");

                    }
                }
            }
        }

        if (goodSphereNumber == 4)
        {
            Debug.Log("VICTOIRE !!!!");
        }
    }
    
    private void ActivateNewLine()
    {
        if (_actualLineNumber < lines.Length - 1) // test pour vérifier que l'on est toujours dans le tableau de lignes
        {
            _actualLineNumber++; // on augmente l'index de ligne actuelle
            lines[_actualLineNumber].gameObject.SetActive(true); // on active les gameobjects de la ligne
            lines[_actualLineNumber].isActive = true; // on active la ligne via une variable bool dans la class
            if (_actualLineNumber > 0) // on vérifie que l'on est pas au premier passage, donc à la première ligne. Si ce n'est pas le cas on désactive la ligne précédente.
            {
                lines[_actualLineNumber-1].isActive = false;

            }
        }
    }
    
    private void MouseRaycast()
    {
        RaycastHit hit;
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(ray,out hit))
        {
            if (hit.collider.gameObject.GetComponent<Sphere>() != null && hit.collider.gameObject.GetComponentInParent<Line>().isActive) // on vérifie qu'il y a bien un component sphère et que sa ligne est la ligne active. Si la ligne n'est pas active on ne fait rien
            {
                _actualSphere = hit.collider.gameObject.GetComponent<Sphere>(); // utilisation d'une variable pour simplifier le code
                _actualSphere.Index++; // on utilise l'index de la sphère pour que le cycle de couleur soit adapté à la sphère
                if (_actualSphere.Index >= _colors.Length)
                {
                    _actualSphere.Index = 0;
                }
                hit.collider.gameObject.GetComponent<MeshRenderer>().material.SetColor("_Color", _colors[_actualSphere.Index]);


            }        
        }

    }
}
