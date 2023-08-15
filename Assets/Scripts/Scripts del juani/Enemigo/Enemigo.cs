using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Enemigo : MonoBehaviour
{
    public int puntosDeVida = 100;
    public Transform posicionIncialDelAtaque;
    [Tooltip("El daño que se hara hacia el jugador.")]
    public int dañoAHacer = 30;
    public Animator anim;
    List<GameObject> juagdoresADañar;

    public void TakeDamage(int damage)
    {
        puntosDeVida -= damage;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "Jugador")
        {
            juagdoresADañar.Add(collision.gameObject);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        juagdoresADañar.Remove(collision.gameObject);
    }

    void AnimacionDeAtaque(InputAction.CallbackContext ctx)
    {
        if (ctx.performed)
        {
            anim.SetTrigger("Ataque");
        }
    }

    public void DañarJugadoresEnRango()
    {
        foreach (GameObject obj in juagdoresADañar)
        {
            int indx = 0;
            juagdoresADañar[indx].GetComponent<EstadisticasDelJugador>().RecivirDaño(dañoAHacer);
            indx++;
        }
    }

    public void RecivirDaño(int dañoARecivir)
    {
        anim.SetTrigger("Daño");
        puntosDeVida -= dañoARecivir;
    }
}
