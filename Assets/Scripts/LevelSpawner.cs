using UnityEngine;

public class LevelSpawner : MonoBehaviour
{
    [SerializeField]
    GameObject coinPrefab;

    [SerializeField]
    GameObject peachPrefab;

    [SerializeField]
    GameObject dragonPrefab;

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

    // Probabilitat d'un 15% per defecte que aparegui un préssec
    [SerializeField]
    float peachChance = 0.15f;
    
    [SerializeField]
    float minDragonSpacing = 15f;

    [SerializeField]
    float maxDragonSpacing = 25f;

    float nextDragonSpawnX;
    Camera mainCamera;
    float nextSpawnX;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        mainCamera = Camera.main;
        
        float rightLimitVisible = mainCamera.transform.position.x + mainCamera.orthographicSize * mainCamera.aspect;
        
        nextSpawnX = mainCamera.transform.position.x + 4f;

        float rndHeight = Random.Range(minY, maxY);

        Vector3 posicio = new Vector3(nextSpawnX, rndHeight, 0);

        Instantiate(coinPrefab, posicio, Quaternion.identity);

        nextSpawnX += Random.Range(minSpacing, maxSpacing);

        nextDragonSpawnX = rightLimitVisible + spawnMargin + Random.Range(3f,6f);
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

            if(Random.value < peachChance)
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
    }
}
