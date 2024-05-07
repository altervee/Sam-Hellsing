using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    // Start is called before the first frame update
    public static GameManager Instance { get; private set; }
    public HUD hud;
    public int PuntosTotales { get; private set; }
    //private int puntosTotales;
    private int vidas = 5;

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
    public void SumarPuntos(int puntosASumar)
    {
        PuntosTotales += puntosASumar;
        hud.UpdatePoints(PuntosTotales);
    }

    void Start()
    {
        
    }
    public void PerderVida()
    {
        vidas -= 1;

        if (vidas == 0)
        {
            // Reiniciamos el nivel.
            SceneManager.LoadScene(0);
        }

        hud.FinishHeart(vidas);
    }

    public bool RecuperarVida()
    {
        if (vidas == 5)
        {
            return false;
        }

        hud.EarntHeart(vidas);
        vidas += 1;
        return true;
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
