using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CombateMelee : MonoBehaviour
{
    [SerializeField] private Transform controladorGolpe;
    [SerializeField] private float radioGolpe;
    [SerializeField] private float da�oGolpe;
    [SerializeField] private float tiempoEntreAtaques;
    [SerializeField] private float tiempoSiguienteAtaque;
    private Animator animator;
    public AudioClip sonidAtaque;

    private void Start()
    {
        animator = GetComponent<Animator>();
    }

    private void Update()
    {
        if (tiempoSiguienteAtaque > 0)
        {
            tiempoSiguienteAtaque -= Time.deltaTime;
        }

        if (Input.GetButtonDown("Fire1") && tiempoSiguienteAtaque <= 0)
        {
            Ataque();
            tiempoSiguienteAtaque = tiempoEntreAtaques;
        }
    }

    private void Ataque()
    {
        animator.SetTrigger("Ataque");
        AudioManager.Instance.ReproducirSonido(sonidAtaque);

        Collider2D[] objetos = Physics2D.OverlapCircleAll(controladorGolpe.position, radioGolpe);

        foreach (Collider2D colisionador in objetos)
        {
            if (colisionador.CompareTag("Enemigo"))
            {
                Enemigo enemigo = colisionador.transform.GetComponent<Enemigo>();
                if (enemigo != null)
                {
                    enemigo.TomarDa�o(da�oGolpe);
                }
            }
            else if (colisionador.CompareTag("EnemigoY"))
            {
                EnemigoY enemigoY = colisionador.transform.GetComponent<EnemigoY>();
                if (enemigoY != null)
                {
                    enemigoY.TomarDa�o(da�oGolpe);
                }
            }
            else if (colisionador.CompareTag("EnemigoS"))
            {
                EnemigoS enemigo = colisionador.GetComponent<EnemigoS>();
                if (enemigo != null)
                {
                    enemigo.TomarDa�o(da�oGolpe);
                }
            }
            else if (colisionador.CompareTag("EnemigoLobo"))
            {
                EnemigoLobo enemigoLobo = colisionador.GetComponent<EnemigoLobo>();
                if (enemigoLobo != null)
                {
                    enemigoLobo.TomarDa�o(da�oGolpe);
                }
            }
        }
    }
    // ver el circulo del da�o del jugador 
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(controladorGolpe.position, radioGolpe);
    }
}
