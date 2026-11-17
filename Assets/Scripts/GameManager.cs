using TMPro;
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

    [SerializeField]
    TMP_Text resultsText;

    [SerializeField]
    TMP_Text scoreText;

    int score = 0;
    private AudioSource audioSource;

    [SerializeField]
    private AudioClip coinSound;
    
    [SerializeField]
    private AudioClip gameOverSound;

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

            instance.resultsText = resultsText;
            instance.scoreText = scoreText;
            scoreText.text = $"Puntuació: {instance.score}";

            Destroy(gameObject);
        }

        // Afegeix listeners a cada botó i cadascún crida el mètode pertinent
        restartButton.onClick.AddListener(instance.RestartGame);

        playButton.onClick.AddListener(instance.RestartGame);

        exitButton.onClick.AddListener(instance.ReturnToStart);
    }

    private void Start()
    {
        scoreText.text = $"Puntuació: {score}";
    }
    public void GameOver()
    {
        if (isGameOver)
        {
            return;
        }

        isGameOver = true;

        ObstacleCounter obstacleCounter = FindFirstObjectByType<ObstacleCounter>();
        ScriptTimeText timeCounter = FindFirstObjectByType<ScriptTimeText>();

        int minuts = Mathf.FloorToInt(timeCounter.ElapsedTime / 60f);
        int segons = Mathf.FloorToInt(timeCounter.ElapsedTime % 60f);

        resultsText.text = $"Monedes: {score}\n" +
                            $"Obstacles superats: {obstacleCounter.ObstaclesPassed}\n" +
                            $"Temps: {minuts:00}:{segons:00}";

        gameOverPanel.SetActive(true);
        audioSource.PlayOneShot(gameOverSound);
        Time.timeScale = 0f;
    }
    public void RestartGame()
    {
        audioSource.Stop();
        isGameOver = false;
        isWaitingToStart = false;
        Time.timeScale = 1f;
        score = 0;
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

    public void AddPoint()
    {
        score++;
        scoreText.text = $"Puntuació: {score}";
        audioSource.PlayOneShot(coinSound);
    }
}
