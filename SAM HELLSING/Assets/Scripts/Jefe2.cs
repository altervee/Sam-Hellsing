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
    private float vidaMax;
    [SerializeField] private Transform controladorGolpe;
    [SerializeField] private float radioGolpe;
    [SerializeField] private float dañoAtaque;
    [SerializeField] private barraVida barra;
    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
        vidaMax = vida; 
        jugador = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
    }
    public void TomarDaño(float daño)
    {
        vida -= daño;
        barra.UpdateHealth(vidaMax, vida);
        //barraDeVida.CambiarVidaActual(vida)
        if (vida <= 0)
        {
            
            Muerte();
        }
    }
    private void Muerte()
    {
        animator.SetTrigger("Muerte");
        GameManager.Instance.GuardarDatosN2();// GUARADAR LOS DATOS DE LA PUNTUACION 
        
    }


    private void DestruirObjeto()
    {
        
        Destroy(gameObject);
        SceneManager.LoadScene("Final");
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
            Debug.LogWarning("Jugador o Animator no están asignados.");
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
