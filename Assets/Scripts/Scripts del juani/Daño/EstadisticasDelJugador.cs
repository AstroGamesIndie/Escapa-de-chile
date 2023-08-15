using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class EstadisticasDelJugador : MonoBehaviour
{
    public int vida = 100;
    public int da�oDeAtaque = 40;
    public Animator anim;
    List<GameObject> enemigosADa�ar = new List<GameObject>();

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Enemigo")
        {
            enemigosADa�ar.Add(collision.gameObject);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        enemigosADa�ar.Remove(collision.gameObject);
    }

    public void AtaqueVFX(InputAction.CallbackContext ctx)
    {
        if(ctx.performed)
        {
            anim.SetTrigger("Ataque");
        }
    }

    public void RecivirDa�o(int da�oARecivir)
    {
        anim.SetTrigger("Da�o");
        vida -= da�oARecivir;
    }

    public void Ataque(int da�o)
    {
        foreach(GameObject gameObject in enemigosADa�ar)
        {

        }
    }
}
