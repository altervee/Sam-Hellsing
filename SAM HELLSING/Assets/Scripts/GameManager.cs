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
    private int muertes = 0;
    private float tiempoNivel = 0f;

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
            muertes += 1;
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
    // LOGIca para registros de datos 
    public void GuardarDatosN1()
    {
        PlayerPrefs.SetInt("PuntosN1", PuntosTotales);
        PlayerPrefs.SetInt("VidasN1", vidas);
        PlayerPrefs.SetInt("MuertesN1", muertes);
        PlayerPrefs.SetFloat("TiempoN1", tiempoNivel);
        PlayerPrefs.Save();

        ActualizarRecordN1();
    }

    public void GuardarDatosN2()
    {
        PlayerPrefs.SetInt("PuntosN2", PuntosTotales);
        PlayerPrefs.SetInt("VidasN2", vidas);
        PlayerPrefs.SetInt("MuertesN2", muertes);
        PlayerPrefs.SetFloat("TiempoN2", tiempoNivel);
        PlayerPrefs.Save();

        ActualizarRecordN2();
    }
    private void ActualizarRecordN1()
    {
        int recordPuntosN1 = PlayerPrefs.GetInt("RecordPuntosN1", 0);
        int recordVidasN1 = PlayerPrefs.GetInt("RecordVidasN1", 0);
        int recordMuertesN1 = PlayerPrefs.GetInt("RecordMuertesN1", int.MaxValue);
        float recordTiempoN1 = PlayerPrefs.GetFloat("RecordTiempoN1", float.MaxValue);

        if ((PuntosTotales > recordPuntosN1))// && (vidas > recordVidasN1)&& (tiempoNivel < recordTiempoN1) lo ahré solo por puntos 
        {
            PlayerPrefs.SetInt("RecordPuntosN1", PuntosTotales);
            PlayerPrefs.SetInt("RecordVidasN1", vidas);
            PlayerPrefs.SetFloat("RecordTiempoN1", tiempoNivel);
        }

        if (muertes < recordMuertesN1)
        {
            PlayerPrefs.SetInt("RecordMuertesN1", muertes);
        }

        PlayerPrefs.Save();
    }

    private void ActualizarRecordN2()
    {
        int recordPuntosN2 = PlayerPrefs.GetInt("RecordPuntosN2", 0);
        int recordVidasN2 = PlayerPrefs.GetInt("RecordVidasN2", 0);
        int recordMuertesN2 = PlayerPrefs.GetInt("RecordMuertesN2", int.MaxValue);
        float recordTiempoN2 = PlayerPrefs.GetFloat("RecordTiempoN2", float.MaxValue);

        if ((PuntosTotales > recordPuntosN2))// solo por puuntos && (vidas > recordVidasN2)&& (tiempoNivel < recordTiempoN2)
        {
            PlayerPrefs.SetInt("RecordPuntosN2", PuntosTotales);
            PlayerPrefs.SetInt("RecordVidasN2", vidas);
            PlayerPrefs.SetFloat("RecordTiempoN2", tiempoNivel);
        }

        if (muertes < recordMuertesN2)
        {
            PlayerPrefs.SetInt("RecordMuertesN2", muertes);
        }


        PlayerPrefs.Save();
    }
    // Update is called once per frame
    void Update()
    {
        tiempoNivel += Time.deltaTime;
    }
}
