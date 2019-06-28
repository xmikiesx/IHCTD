using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[BoltGlobalBehaviour]
public class WavesGenerator : Bolt.GlobalEventListener
{
    // Start is called before the first frame update
    public GameObject enemyPrefab;
    public List<Transform> spawnPoints;
    public int numberPerWave;
    public float generationFrequency;

    public GameObject parent;

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
    }
    void OnDisable()
    {
        GameController.onGameStart -= manageWave;
        Tower.onGameOver -= stop;

    }
    void Start()
    {

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
                Debug.Log("onGameStart desde GameController");
            }
            yield return new WaitForSeconds(generationFrequency);
        }

    }
    private void createEnemy()
    {
        for (int i = 0; i < numberPerWave; i++)
        {
            int randomIndice = Random.Range(0, spawnPoints.Count);
            Vector3 spawnPos = spawnPoints[randomIndice].position;
            spawnPos.y = 0;
            Debug.Log("Instanciando Enemy");
            //GameObject enemy = BoltNetwork.Instantiate(BoltPrefabs.Enemy, spawnPos, Quaternion.identity);
            
            GameObject enemy = Instantiate(enemyPrefab, spawnPos, Quaternion.identity) as GameObject;
            enemy.transform.SetParent(parent.transform);
            enemy.GetComponent<Enemy>().life = deltaSkeletonImpact;
        }
    }
    private void stop(bool value)
    {
        StopCoroutine(coroutine);
        onGenerate = false;

        foreach (Transform child in parent.transform)
        {
            Enemy enemy = child.gameObject.GetComponent<Enemy>();
            enemy.stopAttack();
        }
    }
    private void reset()
    {
        currentWave = 0;
        /*
        foreach (Transform enemy in parent.transform)
        {
            Destroy(enemy.gameObject);
        }*/
    }
}