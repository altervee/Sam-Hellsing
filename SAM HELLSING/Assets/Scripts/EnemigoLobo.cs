using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemigoLobo : MonoBehaviour
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
    //public AudioClip sonidMovimientoSlime;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        if (rb == null)
        {
            Debug.LogError("Rigidbody2D no encontrado en el objeto " + gameObject.name);
        }

        GameObject jugadorObject = GameObject.FindGameObjectWithTag("Player");
        if (jugadorObject != null)
        {
            jugador = jugadorObject.transform;
        }
        else
        {
            Debug.LogError("No se encontró un objeto con la etiqueta 'Player' en la escena.");
        }

        animator = GetComponent<Animator>();
        if (animator == null)
        {
            Debug.LogError("Animator no encontrado en el objeto " + gameObject.name);
        }

        spriteRenderer = GetComponent<SpriteRenderer>();
        if (spriteRenderer == null)
        {
            Debug.LogError("SpriteRenderer no encontrado en el objeto " + gameObject.name);
        }
    }

    void Update()
    {
        if (jugador == null || rb == null || animator == null)
        {
            return; // No hacer nada si faltan componentes esenciales
        }

        // Mirar hacia el jugador
        Vector2 direccionJugador = jugador.position - transform.position;
        float distanciaJugador = direccionJugador.magnitude;

        // Imprimir "CERCA" o "LEJOS" según la distancia al jugador
        if (distanciaJugador <= 3.0f)
        {
            Debug.Log("CERCA");
        }
        else
        {
            Debug.Log("LEJOS");
        }

        if ((distanciaJugador <= distanciaVision) && estaVivo)
        {
            // Establecer la orientación del sprite
            spriteRenderer.flipX = direccionJugador.x <= 0;

            // Normalizar la dirección para que el enemigo se mueva con una velocidad constante
            Vector2 direccionMovimiento = direccionJugador.normalized;

            // Mover al enemigo
            rb.velocity = direccionMovimiento * velocidadMovimiento;

            // Si el jugador está dentro de la distancia de visión, seguir al jugador
            if (distanciaJugador <= 3.0f)
            {
                animator.SetBool("Sigue", false);
                animator.SetBool("Ataque", true);
            }
            else
            {
                animator.SetBool("Ataque", false);
                animator.SetBool("Sigue", true);
            }
        }
        else
        {
            // Si el jugador está fuera de la distancia de visión, detener la animación de seguimiento
            rb.velocity = Vector2.zero;
            animator.SetBool("Sigue", false);
            animator.SetBool("Ataque", false);
        }
    }

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

    private void ReactivarAtaque()
    {
        puedeAtacar = true;
    }

    public void TomarDaño(float daño)
    {
        if (!estaVivo)
            return;

        vida -= daño;

        if (vida <= 0)
        {
            velocidadMovimiento = 0;
            puedeAtacar = false;
            Muerte();
        }
    }

    private void Muerte()
    {
        // Activar la animación de muerte
        animator.SetTrigger("Muerte");

        // Retrasar la destrucción del objeto después de un cierto tiempo
        float tiempoDeEspera = 2.0f; // Cambia esto al tiempo que desees
        Invoke("DestruirObjeto", tiempoDeEspera);
    }

    private void DestruirObjeto()
    {
        // Destruir el objeto después de que haya pasado el tiempo de espera
        Destroy(gameObject);
    }
}