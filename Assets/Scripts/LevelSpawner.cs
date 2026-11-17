using UnityEngine;

public class LevelSpawner : MonoBehaviour
{
    // Prefabs dels objectes que es generen durant la partida

    // Prefab PickupCoin que suma una moneda al marcador quan el jugador la recull
    [SerializeField]
    GameObject coinPrefab;

    // Prefab HealthPeach que permet recuperar una vida quan el jugador en té menys del màxim
    [SerializeField]
    GameObject peachPrefab;

    // Prefab Dragon que s’activa quan l’ocell s’hi acosta i li resta una vida si el toca
    [SerializeField]
    GameObject dragonPrefab;

    // Prefab obstacle_0 amb un collider sòlid i un trigger per comptar-lo com a superat
    [SerializeField]
    GameObject obstaclePrefab;

    // Variables per controlar les posicions

    // Distància horitzontal mínima entre dos premis consecutius
    [SerializeField]
    float minSpacing = 4f;

    // Distància horitzontal màxima entre dos premis consecutius
    [SerializeField]
    float maxSpacing = 7f;

    // Marge addicional a la dreta de la càmera que anticipa la generació dels objectes
    [SerializeField]
    float spawnMargin = 1.5f;

    // Altura mínima de generació de les monedes, els préssecs i els dracs
    [SerializeField]
    float minY = -1f;

    // Altura màxima de generació de les monedes, els préssecs i els dracs
    [SerializeField]
    float maxY = 2f;

    // Distància horitzontal mínima entre dos dracs consecutius
    [SerializeField]
    float minDragonSpacing = 15f;

    // Distància horitzontal màxima entre dos dracs consecutius
    [SerializeField]
    float maxDragonSpacing = 25f;

    // Distància horitzontal mínima entre dos obstacles consecutius
    [SerializeField]
    float minObstacleSpacing = 8f;

    // Distància horitzontal màxima entre dos obstacles consecutius
    [SerializeField]
    float maxObstacleSpacing = 12f;

    // Probabilitat del 15% que un premi generat sigui un préssec, amb la primera moneda fixada a l’inici
    [SerializeField]
    float peachChance = 0.15f;

    // Referència a la càmera i posicions de la propera generació

    // Coordenada X on es generarà el proper obstacle
    float nextObstacleSpawnX;
    // Coordenada X on es generarà el proper drac
    float nextDragonSpawnX;
    // Càmera principal que serveix per calcular el límit dret visible
    Camera mainCamera;
    // Coordenada X on es generarà la propera moneda o el proper préssec
    float nextSpawnX;


    // Calcula les posicions inicials de generació a partir de la càmera
    void Start()
    {
        mainCamera = Camera.main;

        // Coordenada X de la vora dreta visible de la càmera
        float rightLimitVisible = mainCamera.transform.position.x + mainCamera.orthographicSize * mainCamera.aspect;

        nextSpawnX = mainCamera.transform.position.x + 4f;

        // Moneda inicial

        // Altura aleatòria que es farà servir per situar la primera moneda
        float rndHeight = Random.Range(minY, maxY);

        // Posició de la primera moneda, situada quatre unitats per davant del centre inicial de la càmera
        Vector3 posicio = new Vector3(nextSpawnX, rndHeight, 0);

        Instantiate(coinPrefab, posicio, Quaternion.identity);

        // Posició inicial de generació dels dracs

        nextSpawnX += Random.Range(minSpacing, maxSpacing);

        nextDragonSpawnX = rightLimitVisible + spawnMargin + Random.Range(3f, 6f);

        // Posició inicial de generació dels obstacles

        nextObstacleSpawnX = rightLimitVisible + spawnMargin + 4f;
    }

    // Calcula la vora dreta visible i comprova per separat si toca generar un premi, un drac o un obstacle
    void Update()
    {
        // Coordenada X de la vora dreta visible de la càmera en aquest fotograma
        float rightLimitVisible = mainCamera.transform.position.x + mainCamera.orthographicSize * mainCamera.aspect;

        // Monedes i préssecs

        // Genera el següent premi quan la seva posició queda dins del marge de generació
        if (rightLimitVisible + spawnMargin >= nextSpawnX)
        {


            // Altura aleatòria del premi dins del rang configurat
            float rndHeight = Random.Range(minY, maxY);

            // Posició del premi a partir de la coordenada X prevista i l’altura aleatòria
            Vector3 posicio = new Vector3(nextSpawnX, rndHeight, 0);

            // Prefab del premi que es crearà, que pot ser una moneda o un préssec
            GameObject selectedPrefab;

            // Tria un préssec segons la probabilitat configurada i genera una moneda en cas contrari
            if (Random.value < peachChance)
            {
                selectedPrefab = peachPrefab;
            }
            else
            {
                selectedPrefab = coinPrefab;
            }

            Instantiate(selectedPrefab, posicio, Quaternion.identity);

            // Avança la posició del següent premi una distància aleatòria dins dels límits configurats
            nextSpawnX += Random.Range(minSpacing, maxSpacing);
        }
        // Dracs

        // Genera el drac quan la seva posició queda dins del marge de generació
        if (rightLimitVisible + spawnMargin >= nextDragonSpawnX)
        {
            // Posició del drac amb la coordenada X prevista i una altura aleatòria
            Vector3 dragonPosition = new Vector3(nextDragonSpawnX, Random.Range(minY, maxY), 0);

            Instantiate(dragonPrefab, dragonPosition, Quaternion.identity);

            nextDragonSpawnX += Random.Range(minDragonSpacing, maxDragonSpacing);
        }

        // Obstacles mòbils

        if (nextObstacleSpawnX <= rightLimitVisible + spawnMargin)
        {
            // Tria aleatòriament si l'obstacle serà superior o inferior
            bool isUpper = Random.value < 0.5f;

            // Altura inicial de l’obstacle, assignada a 5 o −5 segons el costat
            float obstacleY;

            // Assigna la coordenada Y segons si l’obstacle apareix a la part superior o inferior
            if (isUpper)
            {
                obstacleY = 5f;
            }
            else
            {
                obstacleY = -5f;
            }

            // Posició del nou obstacle a partir de les coordenades X i Y calculades
            Vector3 obstaclePosicio = new Vector3(nextObstacleSpawnX, obstacleY, 0);

            // Desa l’objecte creat a partir del prefab per poder configurar-ne el moviment
            GameObject nouObstacle = Instantiate(obstaclePrefab, obstaclePosicio, Quaternion.identity);
            // Obté el component ScriptObstacle de la instància creada
            ScriptObstacle obstacleScript = nouObstacle.GetComponent<ScriptObstacle>();
            // Configura el sentit inicial del moviment segons la posició de l’obstacle
            obstacleScript.SetDirection(isUpper);

            nextObstacleSpawnX += Random.Range(minObstacleSpacing, maxObstacleSpacing);
        }
    }
}
