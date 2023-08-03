using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class TestingScript : MonoBehaviour
{
    private Transform obje;
    private Vector3 targetposition;
    private float dampingvalue = 3f;
    // Update is called once per frame
    private void Awake()
    {
        
    }
    void Update()
    {
        obje.position = Vector3.Lerp(obje.position, targetposition, dampingvalue*Time.deltaTime);
    }
}

public class Studentrecord
{

}


