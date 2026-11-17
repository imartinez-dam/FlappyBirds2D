using UnityEngine;

public class CleanupZone : MonoBehaviour
{

    // Elimina els objectes generats que entren al trigger situat 15 unitats a l’esquerra de la càmera
    private void OnTriggerEnter2D(Collider2D collision)
    {
        // Limita l’eliminació als objectes identificats per les etiquetes següents
        if (collision.CompareTag("coin") ||
            collision.CompareTag("peach") ||
            collision.CompareTag("dragon") ||
            collision.CompareTag("obstacle"))
        {
            Destroy(collision.gameObject);
        }
    }
}
