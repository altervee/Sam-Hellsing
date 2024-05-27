using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Plataforma : MonoBehaviour
{
    // Start is called before the first frame update
    public Transform[] Posi;
    public float velocidad;
    public int ID;
    public int suma;
    void Start()
    {
        transform.position = Posi[0].position; // que parta en la primera posición 
    }

    // Update is called once per frame
    void Update()
    {
        if (transform.position== Posi[ID].position)// cunado la p de la plata sea igual a la del id
        {
            ID += suma;
        }else
        if(ID == Posi.Length-1)
        {
            suma = -1;
        }else
        if (ID ==0)
        { 
            suma = 1;
        }
        transform.position = Vector3.MoveTowards(transform.position, Posi[ID].position, velocidad* Time.deltaTime);
    }
}
