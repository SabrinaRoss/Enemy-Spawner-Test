using System.Collections.Generic;
using UnityEngine;


public enum SpawnPointNum
{
    POINT1, POINT2, POINT3, POINT4
}

public class SpawnerScript : MonoBehaviour
{
    public GameObject RedEnemy;
    public GameObject BlueEnemy;
    public GameObject BrownEnemy;
    public GameObject GreenEnemy;
    public GameObject YellowEnemy;

    public List<GameObject> listAllEnemyTypes = new List<GameObject>();

    public Light dirLight;
    private TimeOfDayScript timeScript;

    public float spawnerTime = 1.0f;
    public float spawnerDelay = 1.0f;
    public float spawnRadius = 3.0f;
    public int numberOfTypes = 5;
    public SpawnPointNum spawnPoint = SpawnPointNum.POINT4;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        if (dirLight != null)
        {
            timeScript = dirLight.GetComponent<TimeOfDayScript>();
        }
        listAllEnemyTypes.Add(RedEnemy);
        listAllEnemyTypes.Add(BlueEnemy);
        listAllEnemyTypes.Add(BrownEnemy);
        listAllEnemyTypes.Add(GreenEnemy);
        listAllEnemyTypes.Add(YellowEnemy);
    }

    // Update is called once per frame
    void Update()
    {
        if (dirLight != null) { print(dirLight.name); }

        spawnerTime -= Time.deltaTime;
        if (spawnerTime <= 0.0f)
        {
            spawnerTime = spawnerDelay;
            SpawnEnemy();
        }
    }
    void SpawnEnemy()
    {
        GameObject enemyType = getEnemyType();

        Vector3 location = RandomiseSpawnLocation();
        GameObject enemy = Instantiate(enemyType.gameObject, location, Quaternion.identity);
        Debug.Log($"Spawning {enemyType.name}, {location}");
        bool isAllowed = ApplyTimeOfDayEffects(enemy);
        if (isAllowed) {
            EnemyScript enemyScript = enemy.GetComponentInChildren<EnemyScript>();
            enemy.transform.localScale = enemy.transform.localScale * 0.25f;
            if (enemy.GetComponent<Rigidbody>() == null) // I forgot to add a RigidBody as a component into the objects for enemies so making it here to save time
            {
                Rigidbody rigidBody = enemy.AddComponent<Rigidbody>();
                rigidBody.useGravity = true;
            }
            float randomValue = UnityEngine.Random.value;
            if (randomValue >= enemyScript.spawnRate)
            {
                Destroy(enemy);
                return;
            }
        }
        else { Destroy(enemy); }
    }

    bool ApplyTimeOfDayEffects(GameObject enemy)
    {
        if (timeScript == null) { return false; }
        EnemyScript enemyScript = enemy.GetComponentInChildren<EnemyScript>();

        switch (timeScript.time)
        {
            case TimeOfDay.MORNING:
                if (enemyScript.enemyClass == EnemyClass.ARCHERS) { enemyScript.spawnRate += UnityEngine.Random.Range(0.2f, 0.4f); }
                if (enemy.name.Contains("BrownEnemy")) { enemyScript.spawnRate -= UnityEngine.Random.Range(0.1f, 0.3f);  }
                break;
            case TimeOfDay.AFTERNOON:
                if (enemyScript.enemyClass == EnemyClass.ASSASSINS) { return false; }
                if (enemyScript.enemyClass == EnemyClass.GRUNTS) { enemyScript.attackPower++; }
                else { enemyScript.spawnRate += UnityEngine.Random.Range(-0.2f, 0.2f); }
                break;
            case TimeOfDay.NIGHT:
                if (enemyScript.enemyClass == EnemyClass.ASSASSINS) { enemyScript.speed += UnityEngine.Random.Range(0, 2); }
                break;
        }
        return true;
    }
    Vector3 RandomiseSpawnLocation()
    {
        Vector3 spawnNoise = new Vector3(UnityEngine.Random.Range(-spawnRadius, spawnRadius), 0.0f, UnityEngine.Random.Range(-spawnRadius, spawnRadius));
        return transform.position + spawnNoise;
    }

    GameObject getEnemyType()
    {
        int getEnemyNumber = -1;
        List<GameObject> tempEnemy = new List<GameObject>();
        GameObject finalEnemyType = listAllEnemyTypes[0]; // just for the compiler to be happy
        switch (spawnPoint)
        {
            case SpawnPointNum.POINT1:
                // while not neccesary I am going to design this like there will be more enemy types in the future
                foreach(GameObject enemyType in listAllEnemyTypes)
                {
                    EnemyScript enemyScript = enemyType.GetComponentInChildren<EnemyScript>();
                    if (enemyScript.enemyClass == EnemyClass.ARCHERS) { tempEnemy.Add(enemyType); }
                }
                getEnemyNumber = UnityEngine.Random.Range(0, tempEnemy.Count);
                finalEnemyType = tempEnemy[getEnemyNumber];
                break;
            case SpawnPointNum.POINT2:
                foreach (GameObject enemyType in listAllEnemyTypes)
                {
                    EnemyScript enemyScript = enemyType.GetComponentInChildren<EnemyScript>();
                    if (enemyScript.enemyClass == EnemyClass.GRUNTS) { tempEnemy.Add(enemyType); }
                }
                getEnemyNumber = UnityEngine.Random.Range(0, tempEnemy.Count);
                finalEnemyType = tempEnemy[getEnemyNumber];
                break;
            case SpawnPointNum.POINT3:
                finalEnemyType = listAllEnemyTypes[0]; // this is the index for Red Enemies
                break;
            case SpawnPointNum.POINT4:
                getEnemyNumber = UnityEngine.Random.Range(0, numberOfTypes);
                finalEnemyType = listAllEnemyTypes[getEnemyNumber];
                break;
        }
        return finalEnemyType;
    }
}

