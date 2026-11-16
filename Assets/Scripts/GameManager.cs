using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    [SerializeField]
    GameObject gameOverPanel;

    [SerializeField]
    Button restartButton;

    [SerializeField]
    GameObject startPanel;

    [SerializeField]
    Button playButton;

    [SerializeField]
    Button exitButton;

    bool isGameOver = false;

    public bool IsGameOver { get => isGameOver; }

    public bool IsPlaying { get => !isGameOver && !isWaitingToStart; }

    private bool isWaitingToStart = false;

    private AudioSource audioSource;

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
            audioSource = GetComponent<AudioSource>();
            ReturnToStart();
        }
        // Si ja existeix un GameManager persistent, li passem el panell
        // de l'escena nova i n'ajustem la visibilitat segons l'estat de partida.
        // Destruïm només el gestor duplicat, sense reiniciar les dades
        else
        {
            instance.gameOverPanel = gameOverPanel;
            gameOverPanel.SetActive(instance.IsGameOver);

            instance.startPanel = startPanel;
            startPanel.SetActive(instance.isWaitingToStart);

            Destroy(gameObject);
        }

        // Afegeix listeners a cada botó i cadascún crida el mètode pertinent
        restartButton.onClick.AddListener(instance.RestartGame);

        playButton.onClick.AddListener(instance.RestartGame);

        exitButton.onClick.AddListener(instance.ReturnToStart);
    }
    public void GameOver()
    {
        if (isGameOver)
        {
            return;
        }

        isGameOver = true;
        gameOverPanel.SetActive(true);
        audioSource.Play();
        Time.timeScale = 0f;
    }
    public void RestartGame()
    {
        isGameOver = false;
        isWaitingToStart = false;
        Time.timeScale = 1f;
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void ReturnToStart()
    {
        isGameOver = false;
        isWaitingToStart = true;
        gameOverPanel.SetActive(false);
        startPanel.SetActive(true);
        Time.timeScale = 0f;
    }

}
