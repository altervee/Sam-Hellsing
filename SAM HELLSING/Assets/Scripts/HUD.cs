using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class HUD : MonoBehaviour
{
    
    public TextMeshProUGUI puntos;
    public GameObject[] heart;// colleccion de varios objetos
    void Start()
    {
        
    }

    void Update()
    {
        puntos.text = GameManager.Instance.PuntosTotales.ToString();
    }
    public void UpdatePoints(int puntosTotales)
    {
        puntos.text = puntosTotales.ToString();
    }

    public void FinishHeart(int indice)//INDICE QUE LE PASMOS
    {
        heart[indice].SetActive(false);
    }

    public void EarntHeart(int indice)
    {

        heart[indice].SetActive(true);
    }

}
