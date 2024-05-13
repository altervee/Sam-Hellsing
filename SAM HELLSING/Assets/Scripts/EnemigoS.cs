using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemigoS : MonoBehaviour
{
    [SerializeField] private float cooldownAtaque;
    public float velocidadMovimiento;
    public float fuerzaSalto;
    public float distanciaVision;

    [SerializeField] private float vida;

    private Rigidbody2D rb;
    private Transform jugador;
    private Animator animator;
    private SpriteRenderer spriteRenderer;
    private bool puedeAtacar = true;
    private bool estaVivo = true;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        jugador = GameObject.FindGameObjectWithTag("Player").transform;
        animator = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    void Update()
    {
        // Mirar hacia el jugador
        Vector2 direccionJugador = jugador.position - transform.position;
        if (direccionJugador.magnitude <= distanciaVision)
        {
            // Establecer la orientación del sprite
            if (direccionJugador.x > 0)
                spriteRenderer.flipX = false; // Mirando hacia la derecha
            else
                spriteRenderer.flipX = true; // Mirando hacia la izquierda

            // Normalizar la dirección para que el enemigo se mueva con una velocidad constante
            Vector2 direccionMovimiento = direccionJugador.normalized;

            // Mover al enemigo
            rb.velocity = direccionMovimiento * velocidadMovimiento;

            // Si el jugador está dentro de la distancia de visión, seguir al jugador
            animator.SetBool("Sigue", true);
        }
        else
        {
            // Si el jugador está fuera de la distancia de visión, detener la animación de seguimiento
            rb.velocity = Vector2.zero;
            animator.SetBool("Sigue", false);
        }
    }



    // Método para la animación de muerte y destrucción del enemigo
    public void TomarDaño(float daño)
    {
        if (!estaVivo)
            return;

        vida -= daño;

        if (vida <= 0)
        {
            Muerte();
        }
    }

    // Método para la animación de muerte y destrucción del enemigo
    private void Muerte()
    {
        estaVivo = false;

        // Activar la animación de muerte
        animator.SetTrigger("Muerte");

        // Desactivar componentes para evitar interacciones después de muerto
        GetComponent<Rigidbody2D>().simulated = false;
        GetComponent<Collider2D>().enabled = false;

        // Retrasar la destrucción del objeto después de un cierto tiempo
        float tiempoDeEspera = 2.0f; // Cambia esto al tiempo que desees
        Invoke("DestruirObjeto", tiempoDeEspera);
    }

    // Método para destruir el objeto después de la animación de muerte
    private void DestruirObjeto()
    {
        // Destruir el objeto después de que haya pasado el tiempo de espera
        Destroy(gameObject);
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            // Primera parte del script
            if (!puedeAtacar)
                return;

            puedeAtacar = false;

            // Hacer daño al jugador
            other.gameObject.GetComponent<playerControler>().AplicarGolpe();

            Invoke("ReactivarAtaque", cooldownAtaque);
        }
    }

    private void ReactivarAtaque()
    {
        puedeAtacar = true;
    }
}