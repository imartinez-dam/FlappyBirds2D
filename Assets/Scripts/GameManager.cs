using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    // Instància persistent del gestor a la qual poden accedir els altres scripts
    public static GameManager instance;

    // Panell del Canvas on es mostra el resum quan s’acaba la partida
    [SerializeField]
    GameObject gameOverPanel;

    // Botó del panell de Game Over que inicia una partida nova
    [SerializeField]
    Button restartButton;

    // Panell del Canvas que es mostra abans de començar a jugar
    [SerializeField]
    GameObject startPanel;

    // Botó del menú inicial que inicia la partida recarregant MainScene
    [SerializeField]
    Button playButton;

    // Botó del panell de Game Over que torna al menú inicial
    [SerializeField]
    Button exitButton;

    // Indica si la partida ha acabat
    bool isGameOver = false;

    // Permet consultar des d’altres scripts si la partida ha acabat
    public bool IsGameOver { get => isGameOver; }

    // La partida està activa quan no es mostra el menú inicial ni s’ha produït un Game Over
    public bool IsPlaying { get => !isGameOver && !isWaitingToStart; }

    // Indica si el joc espera que el jugador premi el botó de jugar
    private bool isWaitingToStart = false;

    // Text del panell de Game Over que mostra les monedes, els obstacles superats i el temps
    [SerializeField]
    TMP_Text resultsText;

    // Text del Canvas que mostra les monedes recollides durant la partida
    [SerializeField]
    TMP_Text scoreText;

    // Nombre de monedes recollides durant la partida actual
    int score = 0;
    // Font d’àudio del gestor que reprodueix els sons de recollida i de derrota
    private AudioSource audioSource;

    // Clip que sona quan el jugador recull una moneda
    [SerializeField]
    private AudioClip coinSound;
    
    // Clip que sona quan acaba la partida
    [SerializeField]
    private AudioClip gameOverSound;

    void Awake()
    {
        // Conserva la primera instància del gestor quan es torna a carregar l’escena
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
            audioSource = GetComponent<AudioSource>();
            ReturnToStart();
        }
        // El gestor persistent rep els panells de l’escena que s’acaba de carregar
        // La visibilitat dels panells es recupera a partir de l’estat de la partida
        // Es destrueix l’objecte que conté el gestor duplicat
        else
        {
            instance.gameOverPanel = gameOverPanel;
            gameOverPanel.SetActive(instance.IsGameOver);

            instance.startPanel = startPanel;
            startPanel.SetActive(instance.isWaitingToStart);

            // Vincula els textos de l’escena nova al gestor persistent i hi mostra la puntuació actual
            instance.resultsText = resultsText;
            instance.scoreText = scoreText;
            scoreText.text = $"Puntuació: {instance.score}";

            Destroy(gameObject);
        }

        // Assigna a cada botó l’acció corresponent del gestor actiu
        restartButton.onClick.AddListener(instance.RestartGame);

        playButton.onClick.AddListener(instance.RestartGame);

        exitButton.onClick.AddListener(instance.ReturnToStart);
    }

    private void Start()
    {
        scoreText.text = $"Puntuació: {score}";
    }
    // Mostra les monedes, els obstacles superats i el temps abans de reproduir el so de derrota i pausar el joc
    public void GameOver()
    {
        if (isGameOver)
        {
            return;
        }

        isGameOver = true;

        // Consulta els comptadors de l’escena per obtenir els obstacles superats i el temps de partida
        ObstacleCounter obstacleCounter = FindFirstObjectByType<ObstacleCounter>();
        ScriptTimeText timeCounter = FindFirstObjectByType<ScriptTimeText>();

        // Separa els minuts complets dels segons restants per mostrar el temps en format MM:SS
        int minuts = Mathf.FloorToInt(timeCounter.ElapsedTime / 60f);
        int segons = Mathf.FloorToInt(timeCounter.ElapsedTime % 60f);

        resultsText.text = $"Monedes: {score}\n" +
                            $"Obstacles superats: {obstacleCounter.ObstaclesPassed}\n" +
                            $"Temps: {minuts:00}:{segons:00}";

        gameOverPanel.SetActive(true);
        audioSource.PlayOneShot(gameOverSound);
        Time.timeScale = 0f;
    }
    // Reinicia la puntuació i recarrega MainScene tant en jugar per primer cop com en reiniciar
    public void RestartGame()
    {
        audioSource.Stop();
        isGameOver = false;
        isWaitingToStart = false;
        Time.timeScale = 1f;
        score = 0;
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    // El botó de sortir torna al menú inicial i posa el joc en pausa
    public void ReturnToStart()
    {
        isGameOver = false;
        isWaitingToStart = true;
        gameOverPanel.SetActive(false);
        startPanel.SetActive(true);
        Time.timeScale = 0f;
    }

    // Afegeix una moneda al marcador i reprodueix el so de recollida
    public void AddPoint()
    {
        score++;
        scoreText.text = $"Puntuació: {score}";
        audioSource.PlayOneShot(coinSound);
    }
}
