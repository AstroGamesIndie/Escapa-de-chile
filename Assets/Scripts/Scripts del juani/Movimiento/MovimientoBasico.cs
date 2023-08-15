using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;

public class MovimientoBasico : MonoBehaviour
{
    [Header("Componentes necesarios")]
    [Tooltip("El rigidbody 2D que usa el jugador.")]
    public Rigidbody2D rb;

    [Tooltip("El transform de donde se va a checker si se esta tocando el piso.")]
    public Transform chequeoDePiso;

    [Tooltip("El componente que da las animaciones al jugador.")]
    public Animator anim;

    [Header("Valores relacionados al movimiento")]
    [Tooltip("La velocidad del jugador al caminar.")]
    public float velocidad = 7;

    [Tooltip("La fuerza del salto que hace el jugador.")]
    public float poderDelSalto = 10;

    [Tooltip("Que layers o capas se cuentan como piso, solo para chequear si el jugador puede saltar.")]
    public LayerMask queEsPiso;

    [Header("Dash")]
    [Tooltip("Determina si el jugador podra dashear.")]
    public bool puedeDashear = false;

    public float fuerzaDelDash = 6f;

    [Header("WallSide y jump")]
    [SerializeField] bool seEstaDeslizandoEnLaPared;
    [SerializeField] float velocidadDeDeslizoDePared = 2f;
    [SerializeField] LayerMask queEsPared;
    public Transform chequeoDePared;

    private float vertical;
    private float horizontal;
    private bool estaMirandoALaDerecha = true;

    bool EstaEnElPiso()
     {
        return Physics2D.OverlapCircle(chequeoDePiso.position, .2f, queEsPiso);
     }

    bool EstaEnPared()
    {
        return Physics2D.OverlapCircle(chequeoDePared.position, 0.2f, queEsPared);
    }

    void WallSlide()
    {
        if (EstaEnPared() && horizontal != 0f) 
        {
            seEstaDeslizandoEnLaPared = true;
            rb.velocity = new Vector2(rb.velocity.x, Mathf.Clamp(rb.velocity.y, -velocidadDeDeslizoDePared, float.MaxValue));
        }
    }

    // Update is called once per frame
    void Update()
    {
        rb.velocity = new Vector2(horizontal * velocidad, rb.velocity.y);
        if(!estaMirandoALaDerecha && horizontal > 0f)
        {
            Flip();
        }

        if (estaMirandoALaDerecha && horizontal < 0f)
        {
            Flip();
        }

        if(!puedeDashear && EstaEnElPiso())
        {
            puedeDashear = true;
        }
       
        if(EstaEnElPiso())
        {
            anim.SetBool("Cayendo", false);
        }
        else if(!EstaEnElPiso())
        {
            anim.SetBool("Cayendo", true);
        }

        WallSlide();
    }

    public void Saltar(InputAction.CallbackContext ctx)
    {
        if(ctx.performed && EstaEnElPiso())
        {
            rb.velocity = new Vector2(rb.velocity.x, poderDelSalto);
            anim.SetBool("saltando", true);
        }

        if(ctx.canceled && rb.velocity.y > 0f)
        {
            rb.velocity = new Vector2(rb.velocity.x, rb.velocity.y * .5f);
            anim.SetBool("saltando", false);
        }
    }

   
    private void Flip()
    {
        estaMirandoALaDerecha = !estaMirandoALaDerecha;
        Vector3 escalaLocal = transform.localScale;
        escalaLocal.x *= -1;
        transform.localScale = escalaLocal;
    }

    public void Mover(InputAction.CallbackContext ctx)
    {
        horizontal = ctx.ReadValue<Vector2>().x;
        vertical = ctx.ReadValue<Vector2>().y;
    }

    private void OnDrawGizmos()
    {
        if (EstaEnElPiso())
        {
            Gizmos.color = Color.green;
        }

        if (!EstaEnElPiso())
        {
            Gizmos.color = Color.red;
        }

        Gizmos.DrawWireSphere(chequeoDePiso.position, .2f);
    }

    private void FixedUpdate()
    {
        if(horizontal < 0f || horizontal > 0f)
        {
            anim.SetBool("Caminando", true);
        }
        else if(horizontal == 0f)
        {
            anim.SetBool("Caminando", false);
        }
    }

    public void Dash(InputAction.CallbackContext ctx)
    {
        if(ctx.performed)
        {
            if(puedeDashear) 
            { 
                Vector2 dir = new Vector2(horizontal, vertical);
                rb.velocity =new Vector2(rb.velocity.x + dir.x * fuerzaDelDash * 3, rb.velocity.y + dir.y * fuerzaDelDash);
                puedeDashear = false;
            }
            if (!puedeDashear)
                return;
        }
    }


}