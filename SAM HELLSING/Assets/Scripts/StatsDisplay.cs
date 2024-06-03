using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;// patextmesh pro 
public class StatsDisplay : MonoBehaviour
{
    public TextMeshProUGUI puntosText;
    public TextMeshProUGUI vidasText;
    public TextMeshProUGUI tiempoN1Text;
    public TextMeshProUGUI tiempoN2Text;
    public TextMeshProUGUI recordPuntosText;
    public TextMeshProUGUI recordVidasText;
    public TextMeshProUGUI recordTiempoN1Text;
    public TextMeshProUGUI recordTiempoN2Text;


    private void Start()
    {
        MostrarDatos();
    }

    private void MostrarDatos()
    {
        int puntosN1 = PlayerPrefs.GetInt("PuntosN1", 0);
        int puntosN2 = PlayerPrefs.GetInt("PuntosN2", 0);
        int vidasN1 = PlayerPrefs.GetInt("VidasN1", 0);
        int vidasN2 = PlayerPrefs.GetInt("VidasN2", 0);
        float tiempoN1 = PlayerPrefs.GetFloat("TiempoN1", 0f);
        float tiempoN2 = PlayerPrefs.GetFloat("TiempoN2", 0f);

        int totalPuntos = puntosN1 + puntosN2;
        int totalVidas = vidasN1 + vidasN2;

        puntosText.text = ": " + totalPuntos;
        vidasText.text = "s: " + totalVidas;
        tiempoN1Text.text = ": " + tiempoN1.ToString("F2") + "s";
        tiempoN2Text.text = ": " + tiempoN2.ToString("F2") + "s";

        int recordPuntosN1 = PlayerPrefs.GetInt("RecordPuntosN1", 0);
        int recordPuntosN2 = PlayerPrefs.GetInt("RecordPuntosN2", 0);
        int recordVidasN1 = PlayerPrefs.GetInt("RecordVidasN1", 0);
        int recordVidasN2 = PlayerPrefs.GetInt("RecordVidasN2", 0);
        float recordTiempoN1 = PlayerPrefs.GetFloat("RecordTiempoN1");
        float recordTiempoN2 = PlayerPrefs.GetFloat("RecordTiempoN2");
        //int recordMuertesN1 = PlayerPrefs.GetInt("RecordMuertesN1", int.MaxValue);
        //int recordMuertesN2 = PlayerPrefs.GetInt("RecordMuertesN2", int.MaxValue);

        int totalRecordPuntos = recordPuntosN1 + recordPuntosN2;
        int totalRecordVidas = recordVidasN1 + recordVidasN2;

        recordPuntosText.text = " : " + totalRecordPuntos;
        recordVidasText.text = ": " + totalRecordVidas;
        recordTiempoN1Text.text = ": " + recordTiempoN1.ToString("F2") + "s";
        recordTiempoN2Text.text = ": " + recordTiempoN2.ToString("F2") + "s";

    }
}
