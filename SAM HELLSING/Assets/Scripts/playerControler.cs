using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerControler : MonoBehaviour
{
    public float fuerzaGolpe;
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
    private bool puedeMoverse = true;
    RaycastHit2D hit; //plataformas 
    public Vector3 v3;
    public LayerMask layer;// detectar la platafora 
    public float distance; //distancia del raycast




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
        Detectar_Plataforma();
        ProcesarMovimiento();
        ProcesarSalto();    
    }
    bool EstaSuelo()
    {
        RaycastHit2D rycasHit = Physics2D.BoxCast(boxCollider.bounds.center, new Vector2(boxCollider.bounds.size.x, boxCollider.bounds.size.y), 0f, Vector2.down, 0.2f, capaSuelo);
        if ((rycasHit.collider != null && animator.GetBool("isJumping")) || CheckCollision)
        {
            animator.SetBool("isJumping", false);
        }
        return rycasHit.collider != null;
    }
    void ProcesarSalto()
    {
        if(EstaSuelo()|| CheckCollision)
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
            animator.SetBool("isJumping", true);
        }
    }

    void ProcesarMovimiento()
    {
        if (!puedeMoverse)
        {
            return;
        }
        // Obtener la entrada horizontal.
        
        float inputHorizontal = Input.GetAxis("Horizontal");
        if(inputHorizontal != 0 && EstaSuelo())
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
    public void AplicarGolpe()
    {
        puedeMoverse = false;

        Vector2 direccionGolpe = mirarDerecha ? Vector2.left : Vector2.right;

        rgby.AddForce(direccionGolpe * fuerzaGolpe, ForceMode2D.Impulse);

        StartCoroutine(EsperarYActivarMovimiento());
    }

    IEnumerator EsperarYActivarMovimiento()
    {
        yield return new WaitForSeconds(0.1f);

        while (!EstaSuelo())
        {
            yield return null;
        }

        puedeMoverse = true;
    }
    public void LlevarAJugadorAPosicion(float x, float y)
    {
        rgby.velocity = Vector2.zero;
        transform.position = new Vector2(x, y);
        puedeMoverse = true;
    }

    public void Detectar_Plataforma()
    {
        if (CheckCollision)
        {
            transform.parent = hit.collider.transform;
        }
        else
        {
            transform.parent = null;
        }
    }
    bool CheckCollision
    {
        get
        {
            hit = Physics2D.Raycast(transform.position + v3, transform.up * -1, distance, layer);
            return hit.collider != null;

        }

    }
    private void OnDrawGizmos()
    {
        Gizmos.DrawRay(transform.position + v3, transform.up * -1* distance);
    }

}