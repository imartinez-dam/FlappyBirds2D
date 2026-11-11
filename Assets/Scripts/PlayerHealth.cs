using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerHealth : MonoBehaviour
{
    [SerializeField]
    int maxHealth = 3;

    int currentHealth;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        currentHealth = maxHealth;
        Debug.Log($"Vides: {currentHealth}");
    }

    public void TakeDamage()
    {
        currentHealth--;
        Debug.Log($"Vides restants: {currentHealth}");

        if(currentHealth <= 0)
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }
    }
}
