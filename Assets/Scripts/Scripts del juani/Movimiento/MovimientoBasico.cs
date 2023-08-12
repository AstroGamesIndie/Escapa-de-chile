using UnityEngine;
using UnityEngine.InputSystem;

public class MovimientoBasico : MonoBehaviour
{
    [Header("Variables")]
    [Tooltip("El rigidbody 2D que usa el jugador")]
    public Rigidbody2D rb;

    [Tooltip("El transform de donde se va a checker si se esta tocando el piso")]
    public Transform checkeoDePiso;

    [Tooltip("La velocidad del jugador al caminar")]
    public float velocidad = 7;

    [Tooltip("La fuerza del salto que hace el jugador")]
    public float poderDelSalto = 16;

    [Tooltip("Que layers o capas se cuentan como piso, solo para chequear si el jugador puede saltar")]
    public LayerMask queEsPiso;

    private float horizontal;
    private bool estaMirandoALaDerecha = true;

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
