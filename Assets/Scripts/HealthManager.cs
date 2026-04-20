using UnityEngine;

public class HealthManager : MonoBehaviour
{
    [Header("Configuración")]
    public int health = 1;         // Cuántos golpes aguanta
    public string damageTag = "";  // Qué Tag debe golpearlo para restarle vida

    private void OnTriggerEnter2D(Collider2D other)
    {
        // Comprobamos si el objeto que entró en nuestro trigger tiene el tag dañino
        if (other.CompareTag(damageTag))
        {
            health--; // Restamos 1 a la vida

            // Destruimos el misil que nos golpeó para que no lo atraviese
            Destroy(other.gameObject);

            // Si la vida llega a cero, ejecutamos el Game Over
            if (health <= 0)
            {
                EndGame();
            }
        }
    }

    void EndGame()
    {
        Debug.Log("¡GAME OVER! " + gameObject.name + " ha sido destruido.");

        // Detiene el tiempo del juego para que todo se congele
        Time.timeScale = 0f;

        // Destruye el objeto (la nave)
        Destroy(gameObject);

        // Nota: Aquí podrías añadir más tarde una llamada a tu UI para mostrar "Perdiste"
    }
}