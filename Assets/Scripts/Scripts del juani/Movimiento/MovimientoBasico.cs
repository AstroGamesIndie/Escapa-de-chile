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
    public float poderDelSalto = 16;

    [Tooltip("Que layers o capas se cuentan como piso, solo para chequear si el jugador puede saltar.")]
    public LayerMask queEsPiso;

    private float horizontal;
    private bool estaMirandoALaDerecha = true;


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
    }

    public void Saltar(InputAction.CallbackContext ctx)
    {
        if(ctx.performed && EstaEnElPiso())
        {
            rb.velocity = new Vector2(rb.velocity.x, poderDelSalto);
        }

        if(ctx.canceled && rb.velocity.y > 0f)
        {
            rb.velocity = new Vector2(rb.velocity.x, rb.velocity.y * .5f);
        }
    }

    bool EstaEnElPiso()
    {
        return Physics2D.OverlapCircle(chequeoDePiso.position, .2f, queEsPiso);
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
}
