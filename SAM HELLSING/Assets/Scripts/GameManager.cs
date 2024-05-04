using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    // Start is called before the first frame update
    public int PuntosTotales { get {  return puntosTotales; } }
    private int puntosTotales;
    public void SumarPuntos(int puntosSumar)
    {
        puntosTotales += puntosSumar;
        Debug.Log(puntosTotales);// visualizar los puntos totaes
    }
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
