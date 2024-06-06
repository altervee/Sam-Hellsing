using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class pausa : MonoBehaviour
{
    [SerializeField] public GameObject botonPausa;
    [SerializeField] public GameObject menuPausa;
    // Start is called before the first frame update
    void Start()
    {
        
    }
    public void Pusar()
    {
        Time.timeScale = 0.0f;
        botonPausa.SetActive(false);
        menuPausa.SetActive(true);
    }

    // Update is called once per frame
    public void Resume()
    {
        Time.timeScale = 1.0f;
        botonPausa.SetActive(true);
        menuPausa.SetActive(false);
    }
    public void Reiniciar()
    {
        Time.timeScale = 1.0f;
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);// coger el nimbre automaticamente de la escena
    }
    public void Cerrar()
    {
        Application.Quit(); 
    }

}
