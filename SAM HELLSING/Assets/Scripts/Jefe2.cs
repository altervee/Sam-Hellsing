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
    [SerializeField] private Transform controladorGolpe;
    [SerializeField] private float radioGolpe;
    [SerializeField] private float da�oAtaque;
    //[SerializeField] private BarraDeVida barraDeVida;
    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
        //barraDeVida.InicializarBarraDeVida(vida);
        jugador = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
    }
    public void TomarDa�o(float da�o)
    {
        vida -= da�o;
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
    float distanciaJugador;
    private void Update()
    {

        if (jugador != null && animator != null)
        {
            float distanciaJugador = Vector2.Distance(transform.position, jugador.position);
            animator.SetFloat("distancia", distanciaJugador);
        }
        else
        {
            Debug.LogWarning("Jugador o Animator no est�n asignados.");
        }
    }
    public void Ataque()
     {
       Collider2D[] objetos = Physics2D.OverlapCircleAll(controladorGolpe.position, radioGolpe);
        foreach (Collider2D colision in objetos)
        {
            if (colision.CompareTag("Player"))
            {
                GameManager.Instance.PerderVida();

                
                //colision.GetComponent<GameManager>().PerderVida();
            }
        }

    }
    private void OnDrawGizmos()
    {
    Gizmos.color = Color.red;
    Gizmos.DrawWireSphere(controladorGolpe.position, radioGolpe);
    }
}
