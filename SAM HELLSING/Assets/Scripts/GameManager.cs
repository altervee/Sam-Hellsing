using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    // Start is called before the first frame update
    public static GameManager Instance { get; private set; }
    public int PuntosTotales { get; private set; }
    //private int puntosTotales;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Debug.Log("Aviso! Máss de un GameManager en escena.");
        }
    }
    public void SumarPuntos(int puntosSumar)
    {
        PuntosTotales += puntosSumar; // Actualizar la propiedad PuntosTotales
        Debug.Log(PuntosTotales); // Visualizar los puntos totales
    }
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
