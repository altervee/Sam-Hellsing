using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class HUD : MonoBehaviour
{
    
    public TextMeshProUGUI puntos;
    void Start()
    {
        
    }

    void Update()
    {
        puntos.text = GameManager.Instance.PuntosTotales.ToString();
    }
}
