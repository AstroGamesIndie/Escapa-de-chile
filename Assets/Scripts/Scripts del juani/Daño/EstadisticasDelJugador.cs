using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class EstadisticasDelJugador : MonoBehaviour
{
    public int vida = 100;
    public int dañoDeAtaque = 40;
    public Animator anim;
    List<GameObject> enemigosADañar = new List<GameObject>();

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Enemigo")
        {
            enemigosADañar.Add(collision.gameObject);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        enemigosADañar.Remove(collision.gameObject);
    }

    public void AtaqueVFX(InputAction.CallbackContext ctx)
    {
        if(ctx.performed)
        {
            anim.SetTrigger("Ataque");
        }
    }

    public void RecivirDaño(int dañoARecivir)
    {
        anim.SetTrigger("Daño");
        vida -= dañoARecivir;
    }

    public void Ataque(int daño)
    {
        foreach(GameObject gameObject in enemigosADañar)
        {

        }
    }
}
