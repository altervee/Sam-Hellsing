using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemigo : MonoBehaviour //, IDaño
{
    // Variables para el primer script
    public float cooldownAtaque;
    private bool puedeAtacar = true;
    private SpriteRenderer spriteRenderer;

    // Variables para el segundo script
    [SerializeField] private float vida;
    private Animator animator;

    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        animator = GetComponent<Animator>();
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
            puedeAtacar = false;
            Muerte();
        }
    }

    private void Muerte()
    {
        // Activar la animación de muerte
        animator.SetTrigger("Muerte");

        // Retrasar la destrucción del objeto después de un cierto tiempo
        //Invoke("DestruirObjeto", tiempoDeEspera);
    }

    private void DestruirObjeto()
    {
        // Destruir el objeto después de que haya pasado el tiempo de espera
        Destroy(gameObject);
    }
}