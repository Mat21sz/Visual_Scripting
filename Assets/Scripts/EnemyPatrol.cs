using UnityEngine;

[RequireComponent(typeof(PolygonCollider2D))]
public class EnemyPatrol : MonoBehaviour
{
    [Header("Configuración de Movimiento")]
    public float moveSpeed = 3f;
    [Tooltip("Marca la casilla para que empiece hacia la derecha de la pantalla")]
    public bool startMovingRight = true;

    private Vector2 screenBounds;
    private float boundingWidth;
    private float moveDirection;

    void Start()
    {
        PolygonCollider2D col = GetComponent<PolygonCollider2D>();

        // bounds.extents.x siempre nos dará la mitad del ancho en el espacio del mundo, 
        // perfecto para calcular choques de izquierda a derecha sin importar la rotación.
        boundingWidth = col.bounds.extents.x;

        // Límites de la cámara
        screenBounds = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height, Camera.main.transform.position.z));

        moveDirection = startMovingRight ? 1f : -1f;
    }

    void Update()
    {
        // LA MAGIA: Al agregar 'Space.World', la nave se moverá de izquierda a derecha 
        // en la pantalla, ignorando totalmente hacia dónde apunta.
        transform.Translate(Vector3.right * moveDirection * moveSpeed * Time.deltaTime, Space.World);

        // Colisión con el borde DERECHO de la pantalla
        if (transform.position.x >= screenBounds.x - boundingWidth)
        {
            moveDirection = -1f; // Cambiamos dirección hacia la izquierda

            // Limitamos la posición X global
            transform.position = new Vector3(screenBounds.x - boundingWidth, transform.position.y, transform.position.z);
        }
        // Colisión con el borde IZQUIERDO de la pantalla
        else if (transform.position.x <= (screenBounds.x * -1) + boundingWidth)
        {
            moveDirection = 1f; // Cambiamos dirección hacia la derecha

            // Limitamos la posición X global
            transform.position = new Vector3((screenBounds.x * -1) + boundingWidth, transform.position.y, transform.position.z);
        }
    }
}