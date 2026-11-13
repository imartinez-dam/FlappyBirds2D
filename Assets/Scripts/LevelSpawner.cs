using UnityEngine;

public class LevelSpawner : MonoBehaviour
{
    // GameObjects

    [SerializeField]
    GameObject coinPrefab;

    [SerializeField]
    GameObject peachPrefab;

    [SerializeField]
    GameObject dragonPrefab;

    [SerializeField]
    GameObject obstaclePrefab;

    [SerializeField]
    GameObject obstacleTriggerPrefab;

    // Variables per controlar posicions

    [SerializeField]
    float minSpacing = 4f;

    [SerializeField]
    float maxSpacing = 7f;

    [SerializeField]
    float spawnMargin = 1.5f;

    [SerializeField]
    float minY = -1f;

    [SerializeField]
    float maxY = 2f;

    [SerializeField]
    float minDragonSpacing = 15f;

    [SerializeField]
    float maxDragonSpacing = 25f;

    [SerializeField]
    float minObstacleSpacing = 8f;

    [SerializeField]
    float maxObstacleSpacing = 12f;

    // Probabilitat d'un 15% per defecte que aparegui un préssec
    [SerializeField]
    float peachChance = 0.15f;

    // Variables NO Serialitzades per controlar posicions

    float nextObstacleSpawnX;
    float nextDragonSpawnX;
    Camera mainCamera;
    float nextSpawnX;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        mainCamera = Camera.main;

        float rightLimitVisible = mainCamera.transform.position.x + mainCamera.orthographicSize * mainCamera.aspect;

        nextSpawnX = mainCamera.transform.position.x + 4f;

        // Coin inicial

        float rndHeight = Random.Range(minY, maxY);

        Vector3 posicio = new Vector3(nextSpawnX, rndHeight, 0);

        Instantiate(coinPrefab, posicio, Quaternion.identity);

        // Drac inicial

        nextSpawnX += Random.Range(minSpacing, maxSpacing);

        nextDragonSpawnX = rightLimitVisible + spawnMargin + Random.Range(3f, 6f);

        // Obstacle inicial

        nextObstacleSpawnX = rightLimitVisible + spawnMargin + 4f;
    }

    // Update is called once per frame
    void Update()
    {
        float rightLimitVisible = mainCamera.transform.position.x + mainCamera.orthographicSize * mainCamera.aspect;

        // Coins & Peaches

        if (rightLimitVisible + spawnMargin >= nextSpawnX)
        {


            float rndHeight = Random.Range(minY, maxY);

            Vector3 posicio = new Vector3(nextSpawnX, rndHeight, 0);

            GameObject selectedPrefab;

            if (Random.value < peachChance)
            {
                selectedPrefab = peachPrefab;
            }
            else
            {
                selectedPrefab = coinPrefab;
            }

            Instantiate(selectedPrefab, posicio, Quaternion.identity);

            nextSpawnX += Random.Range(minSpacing, maxSpacing);
        }
        // Dracs

        if (rightLimitVisible + spawnMargin >= nextDragonSpawnX)
        {
            Vector3 dragonPosition = new Vector3(nextDragonSpawnX, Random.Range(minY, maxY), 0);

            Instantiate(dragonPrefab, dragonPosition, Quaternion.identity);

            nextDragonSpawnX += Random.Range(minDragonSpacing, maxDragonSpacing);
        }

        // Obstacles i triggers

        if (nextObstacleSpawnX <= rightLimitVisible + spawnMargin)
        {
            // Tria aleatòriament si l'obstacle serà superior o inferior
            bool isUpper = Random.value < 0.5f;

            float obstacleY;

            // Segons si és true o false faig que es generi un obstacle superior o inferior amb la coordenada Y
            if (isUpper)
            {
                obstacleY = 5f;
            }
            else
            {
                obstacleY = -5f;
            }

            Vector3 obstaclePosicio = new Vector3(nextObstacleSpawnX, obstacleY, 0);
            Vector3 triggerPosicio = new Vector3(nextObstacleSpawnX + 1, 0, 0);

            // Crea l'obstacle i el guarda una referència en una instància
            GameObject nouObstacle = Instantiate(obstaclePrefab, obstaclePosicio, Quaternion.identity);
            // Busca el component ScriptObstacle del nouObstacle
            ScriptObstacle obstacleScript = nouObstacle.GetComponent<ScriptObstacle>();
            // Crida el mètode SetDirection amb la decisió presa amb isUpper
            obstacleScript.SetDirection(isUpper);

            Instantiate(obstacleTriggerPrefab, triggerPosicio, Quaternion.identity);

            nextObstacleSpawnX += Random.Range(minObstacleSpacing, maxObstacleSpacing);
        }
    }
}
