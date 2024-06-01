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
    public AudioClip sonidMovimientoSlime;

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
        if ((direccionJugador.magnitude <= distanciaVision)&& estaVivo)
        {
            // Establecer la orientaci�n del sprite
            if (direccionJugador.x > 0)
                spriteRenderer.flipX = false; // Mirando hacia la derecha
            else
                spriteRenderer.flipX = true; // Mirando hacia la izquierda

            // Normalizar la direcci�n para que el enemigo se mueva con una velocidad constante
            Vector2 direccionMovimiento = direccionJugador.normalized;

            // Mover al enemigo
            rb.velocity = direccionMovimiento * velocidadMovimiento;

            // Si el jugador est� dentro de la distancia de visi�n, seguir al jugador
            animator.SetBool("Sigue", true);
            
        }
        else
        {
            // Si el jugador est� fuera de la distancia de visi�n, detener la animaci�n de seguimiento
            rb.velocity = Vector2.zero;
            animator.SetBool("Sigue", false);
        }
    }



    // M�todo para la animaci�n de muerte y destrucci�n del enemigo
    public void TomarDa�o(float da�o)
    {
        if (!estaVivo)
            return;
        AudioManager.Instance.ReproducirSonido(sonidMovimientoSlime);
        vida -= da�o;

        if (vida <= 0)
        {
            velocidadMovimiento = 0;
            puedeAtacar = false;

            Muerte();
        }
    }

    // M�todo para la animaci�n de muerte y destrucci�n del enemigo

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            // Primera parte del script
            if (!puedeAtacar)
                return;

            puedeAtacar = false;
            Color color = spriteRenderer.color;
            color.a = 0.5f;
            spriteRenderer.color = color;

            GameManager.Instance.PerderVida();

            other.gameObject.GetComponent<playerControler>().AplicarGolpe();

            Invoke("ReactivarAtaque", cooldownAtaque);
        }
    }

    void ReactivarAtaque()
    {
        puedeAtacar = true;

        Color c = spriteRenderer.color;
        c.a = 1f;
        spriteRenderer.color = c;
    }
    private void Muerte()
    {
        // Activar la animaci�n de muerte
        animator.SetTrigger("Muerte");

        // Retrasar la destrucci�n del objeto despu�s de un cierto tiempo

    }

    private void DestruirObjeto()
    {
        // Destruir el objeto despu�s de que haya pasado el tiempo de espera
        Destroy(gameObject);
    }
}