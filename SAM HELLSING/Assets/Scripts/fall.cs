using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class fall : MonoBehaviour
{
    public float xPosicion;
    public float yPosicion;
    // Start is called before the first frame update
    void Start()
    {
        

    }

    // Update is called once per frame
    void Update()
    {
        
    }
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            playerControler jugador = other.GetComponent<playerControler>();
            if (jugador != null)
            {
                GameManager.Instance.PerderVida();
                jugador.LlevarAJugadorAPosicion(xPosicion, yPosicion);
            }
        }
    }
}
