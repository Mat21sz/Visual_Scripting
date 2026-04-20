using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(BoxCollider2D))]
public class ShipController : MonoBehaviour
{
    [Header("Configuraci�n de Movimiento")]
    public float moveSpeed = 5f;

    private Rigidbody2D rb;
    private BoxCollider2D col;
    private Vector2 movement;

    // Variables para los l�mites de la pantalla
    private Vector2 screenBounds;
    private float objectWidth;
    private float objectHeight;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        col = GetComponent<BoxCollider2D>();

        // Calcula las coordenadas del mundo basadas en el tama�o de la pantalla y la c�mara
        screenBounds = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height, Camera.main.transform.position.z));

        // Obtiene la mitad del ancho y alto del collider para que el rebote sea exacto en el borde
        objectWidth = col.bounds.extents.x;
        objectHeight = col.bounds.extents.y;
    }

    void Update()
    {
        // GetAxisRaw devuelve -1, 0 o 1, lo que da un movimiento seco y preciso, sin "resbalar"
        movement.x = Input.GetAxisRaw("Horizontal");
        movement.y = Input.GetAxisRaw("Vertical");
    }

    void FixedUpdate()
    {
        // 1. Aplicar la velocidad al Rigidbody2D (normalizamos para que no vaya m�s r�pido en diagonal)
        rb.linearVelocity = movement.normalized * moveSpeed;

        // 2. Limitar (Clamp) la posici�n para que no salga de la pantalla
        Vector2 clampedPosition = rb.position;

        clampedPosition.x = Mathf.Clamp(clampedPosition.x, (screenBounds.x * -1) + objectWidth, screenBounds.x - objectWidth);
        clampedPosition.y = Mathf.Clamp(clampedPosition.y, (screenBounds.y * -1) + objectHeight, screenBounds.y - objectHeight);

        // 3. Reasignar la posici�n restringida
        rb.position = clampedPosition;
    }
}