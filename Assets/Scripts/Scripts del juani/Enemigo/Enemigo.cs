using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Enemigo : MonoBehaviour
{
    public int puntosDeVida = 100;
    public Transform posicionIncialDelAtaque;
    [Tooltip("El da�o que se hara hacia el jugador.")]
    public int da�oAHacer = 30;
    public Animator anim;
    List<GameObject> juagdoresADa�ar;

    public void TakeDamage(int damage)
    {
        puntosDeVida -= damage;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "Jugador")
        {
            juagdoresADa�ar.Add(collision.gameObject);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        juagdoresADa�ar.Remove(collision.gameObject);
    }

    void AnimacionDeAtaque(InputAction.CallbackContext ctx)
    {
        if (ctx.performed)
        {
            anim.SetTrigger("Ataque");
        }
    }

    public void Da�arJugadoresEnRango()
    {
        foreach (GameObject obj in juagdoresADa�ar)
        {
            int indx = 0;
            juagdoresADa�ar[indx].GetComponent<EstadisticasDelJugador>().RecivirDa�o(da�oAHacer);
            indx++;
        }
    }

    public void RecivirDa�o(int da�oARecivir)
    {
        anim.SetTrigger("Da�o");
        puntosDeVida -= da�oARecivir;
    }
}
