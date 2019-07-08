using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[BoltGlobalBehaviour]
public class WavesGenerator : Bolt.GlobalEventListener
//public class WavesGenerator : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject enemyPrefab;
    public List<Transform> spawnPoints;
    public int numberPerWave;
    public float generationFrequency;

    public GameObject parent;
    private GameObject parentTest;

    private int currentWave = 0;
    private bool onGenerate;
    private IEnumerator coroutine;
    private int deltaSkeletonImpact = 1;

    public delegate void OnNewWave(int counterValue);
    public static event OnNewWave onNewWave;

    void OnEnable()
    {
        GameController.onGameStart += manageWave;
        Tower.onGameOver += stop;
        TowerBolt.onGameOver += stop;
    }
    void OnDisable()
    {
        GameController.onGameStart -= manageWave;
        Tower.onGameOver -= stop;
        TowerBolt.onGameOver -= stop;
    }
    
    void Start()
    {
        GameObject[] Parent = GameObject.FindGameObjectsWithTag("EnemiesHolder");
        Debug.Log(Parent[0]);
        parentTest = Parent[0];
    }

    private void manageWave(bool value)
    {
        reset();
        onGenerate = true;
        coroutine = launchWaves();
        StartCoroutine(coroutine);
    }
    IEnumerator launchWaves()
    {
        while (onGenerate)
        {
            currentWave++;
            numberPerWave += (int)currentWave / 2;
            deltaSkeletonImpact += (int)currentWave / 2;
            createEnemy();
            //Enviar evento en New Wave
            if (onNewWave != null)
            {
                onNewWave(currentWave);
                Debug.Log("New Wave");
            }
            yield return new WaitForSeconds(generationFrequency);
        }

    }
    private void createEnemy()
    {
        for (int i = 0; i < numberPerWave; i++)
        {
            Debug.Log("spawnPoints.Count");
            int randomIndice = Random.Range(0, spawnPoints.Count);
            Vector3 spawnPos = spawnPoints[randomIndice].position;
            spawnPos.y = 0;
            Debug.Log("Instanciando Enemy");
            GameObject enemy = BoltNetwork.Instantiate(BoltPrefabs.Enemy, spawnPos, Quaternion.identity);
            //GameObject enemy = Instantiate(enemyPrefab, spawnPos, Quaternion.identity) as GameObject;
            //enemy.transform.SetParent(parentTest.transform);
            enemy.GetComponent<Enemy>().life = deltaSkeletonImpact;
        }
    }
    private void stop(bool value)
    {
        StopCoroutine(coroutine);
        onGenerate = false;
        Debug.Log("Quizas no le vasto");
        /*if (parentTest != null)
        {
            foreach (Transform child in parentTest.transform)
            {
                Enemy enemy = child.gameObject.GetComponent<Enemy>();
                enemy.stopAttack();
            }
        }*/
        GameObject[] Enemies = GameObject.FindGameObjectsWithTag("Enemy");
        foreach (GameObject child in Enemies) {
            Enemy enemy = child.gameObject.GetComponent<Enemy>();
            enemy.stopAttack();
        }
    }
    private void reset()
    {
        currentWave = 0;
        //Debug.Log(parentTest);
        /* if (parentTest != null)
         {
             foreach (Transform enemy in parentTest.transform)
             {
                 Destroy(enemy.gameObject);
                 BoltNetwork.Destroy(enemy.gameObject);
             }
         }*/
        GameObject[] Enemies = GameObject.FindGameObjectsWithTag("Enemy");
        foreach (GameObject child in Enemies)
        {
            Enemy enemy = child.gameObject.GetComponent<Enemy>();
            BoltNetwork.Destroy(enemy.gameObject);
        }
    }
}