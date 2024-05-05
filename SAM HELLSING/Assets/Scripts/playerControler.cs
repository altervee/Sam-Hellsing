using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControladorDeJugador : MonoBehaviour
{
    public float velocidad;
    public float fuerzaSalto;
    public LayerMask capaSuelo;
    public int saltosMaximos;
    public AudioClip sonidSalto;

    private Rigidbody2D rgby;//cacheado de componeses
    private BoxCollider2D boxCollider;
    private bool mirarDerecha = true;
    private int saltosRestantes;
    private Animator animator;



    void Start()
    {

        // Obtener el componente Rigidbody2D una vez al inicio para mejorar el rendimiento.
        rgby = GetComponent<Rigidbody2D>();
        boxCollider = GetComponent<BoxCollider2D>();
        saltosRestantes = saltosMaximos;
        animator = GetComponent<Animator>();
       
    }

    void Update()
    {
        ProcesarMovimiento();
        ProcesarSalto();    
    }
    bool EstaSuelo()
    {
        RaycastHit2D rycasHit = Physics2D.BoxCast(boxCollider.bounds.center, new Vector2(boxCollider.bounds.size.x, boxCollider.bounds.size.y), 0f, Vector2.down, 0.2f, capaSuelo);
        return rycasHit.collider != null;
    }
    void ProcesarSalto()
    {
        if(EstaSuelo())
        {
            saltosRestantes = saltosMaximos;
        }
        //COMPROBAR SI SE PULSA EL ESPACIO
        if (Input.GetKeyDown(KeyCode.Space) && saltosRestantes>1)
        {
            saltosRestantes--;
            rgby.velocity = new Vector2 (rgby.velocity.x, 0f);
            rgby.AddForce(Vector2.up*fuerzaSalto, ForceMode2D.Impulse);
            AudioManager.Instance.ReproducirSonido(sonidSalto);
        }
    }

    void ProcesarMovimiento()
    {
        // Obtener la entrada horizontal.
        
        float inputHorizontal = Input.GetAxis("Horizontal");
        if(inputHorizontal != 0)
        {
            animator.SetBool("isWalking", true);
        }
        else
        {
            animator.SetBool("isWalking", false);
        }
        // Aplicar la velocidad al Rigidbody2D.
        rgby.velocity = new Vector2(inputHorizontal * velocidad, rgby.velocity.y);

        GestionarOrientacion(inputHorizontal);
    }
    void GestionarOrientacion(float inputHorizontal)
    {
        if ((mirarDerecha== true && inputHorizontal < 0)|| mirarDerecha==false && inputHorizontal > 0) 
        {
            mirarDerecha = !mirarDerecha;
            transform.localScale= new Vector2(-transform.localScale.x, transform.localScale.y);
        }
    }
}