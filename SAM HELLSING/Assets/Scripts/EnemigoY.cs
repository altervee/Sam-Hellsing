using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemigoY : MonoBehaviour
{

    // Variables para el primer script
    public float cooldownAtaque;
    private bool puedeAtacar = true;
    private SpriteRenderer spriteRenderer;
    public float posicionYArriba;
    public float posicionYAbajo;
    public float velocidadMovimiento;

    // Variables para el segundo script
    [SerializeField] private float vida;
    private Animator animator;
    private bool moviendoseHaciaArriba = true;

    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        animator = GetComponent<Animator>();
    }
    void Update()
    {
        // Movimiento entre las dos posiciones
        float step = velocidadMovimiento * Time.deltaTime;
        if (moviendoseHaciaArriba)
        {
            Vector3 targetPosition = new Vector3(transform.position.x, posicionYArriba, transform.position.z);
            transform.position = Vector3.MoveTowards(transform.position, targetPosition, step);
            if (transform.position.y >= posicionYArriba)
            {
                moviendoseHaciaArriba = false;
            }
        }
        else
        {
            Vector3 targetPosition = new Vector3(transform.position.x, posicionYAbajo, transform.position.z);
            transform.position = Vector3.MoveTowards(transform.position, targetPosition, step);
            if (transform.position.y <= posicionYAbajo)
            {
                moviendoseHaciaArriba = true;
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

    // Segunda parte del script
    public void TomarDaño(float daño)
    {
        vida -= daño;

        if (vida <= 0)
        {
            Muerte();
        }
    }

    private void Muerte()
    {
        velocidadMovimiento = 0;
        puedeAtacar = false;
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
