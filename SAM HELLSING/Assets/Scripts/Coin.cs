using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coin : MonoBehaviour
{
    // Start is called before the first frame update
    public int valor = 1;
    public GameManager gameManger;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {// basia¡camente comprbar que si es tocada por el jugador se destrute
        //Debug.Log("Pasaste por la moneda"); comproba si se pasa
        if (collision.CompareTag("Player"))
        {
            gameManger.SumarPuntos(valor);
            Destroy(this.gameObject);
        }
    }
}
