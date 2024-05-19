using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemigoLobo : MonoBehaviour
{
    [SerializeField] private float cooldownAtaque;
    public float velocidadMovimiento;
    public float distanciaVision;
    public float distanciaRetroceso;

    [SerializeField] private float vida;
    [SerializeField] private float ataque;

    private Rigidbody2D rb;
    private Transform jugador;
    private Animator animator;
    private SpriteRenderer spriteRenderer;
    private bool puedeAtacar = true;
    private bool estaVivo = true;
    private float ataqueOriginal;
    public AudioClip sonidMovimientoSlime;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        jugador = GameObject.FindGameObjectWithTag("Player")?.transform;
        animator = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        ataqueOriginal = ataque; // Guardar el valor original del ataque

        if (animator == null)
        {
            Debug.LogError("No se encontr� el componente Animator en el objeto " + gameObject.name);
        }

        if (jugador == null)
        {
            Debug.LogError("No se encontr� ning�n objeto con la etiqueta 'Player'");
        }
    }

    void Update()
    {
        if (!estaVivo || animator == null || jugador == null) return;

        // Mirar hacia el jugador
        Vector2 direccionJugador = jugador.position - transform.position;
        if (direccionJugador.magnitude <= distanciaVision)
        {
            // Establecer la orientaci�n del sprite
            spriteRenderer.flipX = direccionJugador.x < 0; // Mirar hacia la izquierda si el jugador est� a la izquierda

            // Normalizar la direcci�n para que el enemigo se mueva con una velocidad constante
            Vector2 direccionMovimiento = direccionJugador.normalized;

            // Mover al enemigo
            rb.velocity = direccionMovimiento * velocidadMovimiento;

            // Imprimir la velocidad en la consola para depurar
            Debug.Log("Velocidad del enemigo: " + rb.velocity);

            // Si el jugador est� dentro de la distancia de visi�n, seguir al jugador
            animator.SetBool("Sigue", true);

            // Activar la animaci�n de ataque si est� en proximidad
            animator.SetBool("Proximidad", direccionJugador.magnitude <= distanciaRetroceso);
        }
        else
        {
            // Si el jugador est� fuera de la distancia de visi�n, detener la animaci�n de seguimiento
            rb.velocity = Vector2.zero;
            animator.SetBool("Sigue", false);
            animator.SetBool("Proximidad", false); // Desactivar proximidad cuando no est� siguiendo al jugador
        }
    }

    // M�todo para la animaci�n de muerte y destrucci�n del enemigo
    public void TomarDa�o(float da�o)
    {
        if (!estaVivo)
            return;

        vida -= da�o;

        if (vida <= 0)
        {
            estaVivo = false;
            velocidadMovimiento = 0;
            puedeAtacar = false;

            Muerte();
        }
        else
        {
            // Retroceso del enemigo
            StartCoroutine(Retroceder());
        }
    }

    private IEnumerator Retroceder()
    {
        Vector2 direccionRetroceso = (transform.position - jugador.position).normalized;
        Vector2 posicionFinal = (Vector2)transform.position + direccionRetroceso * distanciaRetroceso;
        float tiempoRetroceso = 0.5f;
        float tiempoTranscurrido = 0;

        while (tiempoTranscurrido < tiempoRetroceso)
        {
            rb.velocity = direccionRetroceso * velocidadMovimiento;
            tiempoTranscurrido += Time.deltaTime;
            yield return null;
        }

        rb.velocity = Vector2.zero;
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            if (!puedeAtacar)
                return;

            puedeAtacar = false;
            ataque = 0; // Deshabilitar ataque durante el cooldown
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
        ataque = ataqueOriginal; // Restaurar el valor original del ataque

        Color c = spriteRenderer.color;
        c.a = 1f;
        spriteRenderer.color = c;
    }

    private void Muerte()
    {
        if (animator != null)
        {
            // Activar la animaci�n de muerte
            animator.SetTrigger("Muerte");
        }

        // Retrasar la destrucci�n del objeto despu�s de un cierto tiempo
        float tiempoDeEspera = 2.0f; // Cambia esto al tiempo que desees
        Invoke("DestruirObjeto", tiempoDeEspera);
    }

    private void DestruirObjeto()
    {
        // Destruir el objeto despu�s de que haya pasado el tiempo de espera
        Destroy(gameObject);
    }
}