using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;// libreria para cambiar de escena 

public class menuPrincipal : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void Jugar()
    {
        //SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex +1);
        // ACCEDER a lal primera pantalla mpor el index 
        SceneManager.LoadScene("Nivel1"); // Cargar la escena "Nivel1" por su nombre
    }
    public void Salir()
    {
        Debug.Log("Salir");
        Application.Quit();
    }
    // borrare todos los datos del palyereprehs
    public void Borrar()
    {
        PlayerPrefs.DeleteAll();
    }
}
