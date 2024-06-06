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
        AudioManager.Instance.ReproducirSonido(sonidMovimientoSlime);
        vida -= daño;

        if (vida <= 0)
        {
            velocidadMovimiento = 0;
            puedeAtacar = false;

            Muerte();
        }
    }

    // Método para la animación de muerte y destrucción del enemigo

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
        // Activar la animación de muerte
        animator.SetTrigger("Muerte");

        // Retrasar la destrucción del objeto después de un cierto tiempo

    }

    private void DestruirObjeto()
    {
        // Destruir el objeto después de que haya pasado el tiempo de espera
        Destroy(gameObject);
    }
}