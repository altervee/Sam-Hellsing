using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Jefe2 : MonoBehaviour
{
    private Animator animator;
    public Rigidbody2D rb;
    public Transform jugador;
    private bool mirarDerecha = true;
    [Header("Vida")]
    [SerializeField] private float vida;
    //[SerializeField] private BarraDeVida barraDeVida;
    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
        //barraDeVida.InicializarBarraDeVida(vida);
        jugador = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
    }
    public void TomarDaño(float daño)
    {
        vida -= daño;
        //barraDeVida.CambiarVidaActual(vida)
        if (vida <= 0)
        {
            animator.SetTrigger("Muerte");
        }
    }
    private void Muerte()
    {
        Destroy(gameObject);
    }
    public void MirarJugador()
    {
        if((jugador.position.x > transform.position.x && !mirarDerecha)||(jugador.position.x < transform.position.x && mirarDerecha))
        {
            mirarDerecha = !mirarDerecha;
            transform.eulerAngles = new Vector3(0, transform.eulerAngles.y +180, 0);// MIRAR AL JUG
        }
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
