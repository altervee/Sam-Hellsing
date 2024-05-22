using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EnemigoLobo : MonoBehaviour
{
    [SerializeField] private float cooldownAtaque;
    public float velocidadMovimiento;
    public float fuerzaSalto;
    public float distanciaVision;
    [SerializeField] private float vida;
    [SerializeField] private float retrocesoDano; // Nueva variable para la fuerza de retroceso

    private Rigidbody2D rb;
    private Transform jugador;
    private Animator animator;
    private SpriteRenderer spriteRenderer;
    private bool puedeAtacar = true;
    private bool estaVivo = true;
    private bool enAtaque = false;
    private bool enRetroceso = false;

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
        if (!estaVivo) return;

        // Mirar hacia el jugador
        Vector2 direccionJugador = jugador.position - transform.position;
        float distanciaJugador = direccionJugador.magnitude;


        if (!enAtaque && !enRetroceso)
        {
            if (distanciaJugador <= distanciaVision)
            {
                // Establecer la orientación del sprite
                spriteRenderer.flipX = direccionJugador.x <= 0;

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
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            // Primera parte del script
            if (!puedeAtacar)
                return;

            puedeAtacar = false;
            enAtaque = true;

            Color color = spriteRenderer.color;
            color.a = 0.5f;
            spriteRenderer.color = color;

            GameManager.Instance.PerderVida();

            other.gameObject.GetComponent<playerControler>().AplicarGolpe();

            animator.SetTrigger("Ataque"); // Usar trigger en lugar de bool para la animación de ataque
            Invoke("ReactivarAtaque", cooldownAtaque);
        }
    }

    private void ReactivarAtaque()
    {
        enAtaque = false;
        puedeAtacar = true;
        
    }

    public void TomarDaño(float daño)
    {
        if (!estaVivo)
            return;
        velocidadMovimiento = velocidadMovimiento + 1; 
        vida -= daño;

        if (vida <= 0)
        {
            velocidadMovimiento = 0;
            puedeAtacar = false;
            Muerte();
        }
        else
        {
            StartCoroutine(Retroceso());
        }
    }

    private IEnumerator Retroceso()
    {
        enRetroceso = true;

        // Dirección del retroceso
        float retrocesoDireccion = retrocesoDano;

        // Distancia total a retroceder
        float distanciaRetroceso = 2f;
        float distanciaRetrocedida = 0f;

        // Desactivar la velocidad de movimiento
        float velocidadOriginal = velocidadMovimiento;
        velocidadMovimiento = 0;

        while (distanciaRetrocedida < distanciaRetroceso)
        {
            // Calcular el desplazamiento en este frame
            float desplazamiento = retrocesoDano * Time.deltaTime;

            // Asegurarse de no superar la distancia de retroceso
            if (distanciaRetrocedida + desplazamiento > distanciaRetroceso)
            {
                desplazamiento = distanciaRetroceso - distanciaRetrocedida;
            }

            // Aplicar el desplazamiento
            rb.MovePosition(rb.position + new Vector2(retrocesoDireccion * desplazamiento, 0));
            distanciaRetrocedida += desplazamiento;

            yield return null;
        }

        // Restaurar la velocidad de movimiento
        velocidadMovimiento = velocidadOriginal;
        enRetroceso = false;
    }

    private void Muerte()
    {
        estaVivo = false;
        animator.SetTrigger("Muerte"); // Usar trigger para la animación de muerte

        float tiempoDeEspera = 4.0f;
        SceneManager.LoadScene("Nivel2");
        Invoke("DestruirObjeto", tiempoDeEspera);
    }

    private void DestruirObjeto()
    {
        Destroy(gameObject);
    }
}