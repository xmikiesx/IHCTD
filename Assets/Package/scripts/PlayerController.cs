using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class PlayerController : MonoBehaviour
{
    public int mana;
    public int initialMana = 100;
    public Tower tower;
    //public TurretBehaviour Turret;
    public GameObject floor;
    //public GameObject towerObject;
   // public GameObject turretObject;
   // public GameObject enemyObject;
    public GameObject spellPrefab;
    public float maxDistance;

    // public GameObject parent;


    public int waveBonusMana = 20;
    public int maxMana = 500;

    public int repairManaCost;
    public AudioClip repairSound;
    public AudioClip destroySound;

    private RaycastHit hit;
    private AudioSource sound;
    private Enemy enemyComp;

    public delegate void OnManaChange(int value);
    public static event OnManaChange onManaChange;

    private RaycastHit hit2;
    //public OthersHandler vuforiaOthersHandler;
    private IEnumerator coroutine;
    private bool onInstantiate;


    //public EnemyHandler vuforiaEnemyHandler;
    private IEnumerator coroutineEnemy;
    private bool onInstantiateEnemy;

    void OnEnable()
    {
        GameController.onGameStart += reset;
        WavesGenerator.onNewWave += updateMana;
        //        OthersHandler.onImageTrackeable += trackOthersImageTarget;
        //        OthersHandler.onImageNoTrackeable += stopInstantiate;
        //        EnemyHandler.onEnemyTrackeable += trackEnemyImageTarget;
        //        EnemyHandler.onEnemyNoTrackeable += stopInstantiateENemy;
        //WavesGenerator.onNewWave += updateWaveCounter;
    }
    void OnDisable()
    {
        GameController.onGameStart -= reset;
        WavesGenerator.onNewWave -= updateMana;
        //        OthersHandler.onImageTrackeable -= trackOthersImageTarget;
        //        OthersHandler.onImageNoTrackeable -= stopInstantiate;
        //        EnemyHandler.onEnemyTrackeable -= trackEnemyImageTarget;
        //        EnemyHandler.onEnemyNoTrackeable -= stopInstantiateENemy;
    }
    // Start is called before the first frame update
    void Start()
    {
        sound = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.R))
        {
            repairTower(true);
        }
        /*if (vuforiaOthersHandler.isReady)
        {
            //    trackOthersImageTarget();
            //     tower.transform.position = temp;
            // Physics.Linecast(vuforiaOthersHandler.transform.position, floor.transform.position);
            // Debug.DrawLine(vuforiaOthersHandler.transform.position, floor.transform.position);
            // if (Physics.Raycast(transform.position, (player.position - transform.position), out hit, maxRange)
        }
        else
        {
            //  stopInstantiate(true);
        }*/
       
        /*if(vuforiaEnemyHandler.isReady && vuforiaOthersHandler.isReady) { 
            Debug.DrawLine(vuforiaOthersHandler.transform.position, vuforiaEnemyHandler.transform.position, Color.blue);
            var heading = vuforiaOthersHandler.transform.position - vuforiaEnemyHandler.transform.position;
            var distance = heading.magnitude;
            var direction = heading / distance;
            Debug.Log("Heading :" + heading);
            Debug.Log("Distance :" + distance);
            Debug.Log("Direccion :" + direction);
        }*/
        if (Input.GetMouseButton(0) && !EventSystem.current.IsPointerOverGameObject())
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out hit, maxDistance))
            {
                Debug.Log("Toco" + hit.collider.gameObject.name + " en Vector3:" + hit.point);
                Debug.DrawLine(ray.origin, hit.point, Color.red);

                if (hit.collider.gameObject.CompareTag("Enemy") || hit.collider.gameObject.CompareTag("Floor"))
                {
                    launchSpell();

                    /*if (vuforiaEnemyHandler.isReady)
                    {
                        launchTurret();

                    }
                    else
                    {
                        launchSpell();
                    }*/
                }
                else
                {
                    if (hit.collider.gameObject.CompareTag("Tower"))
                    { 
                        repairTower(true);
                    }
                    else
                    {
                        if (hit.collider.gameObject.CompareTag("Coin"))
                        {
                            Destroy(hit.collider.gameObject);
                        }
                    }
                }
            }

        }
    }
    private void reset(bool value)
    {
        mana = initialMana;
        repairTower(true);
    }
    private void updateMana(int currentWave)
    {
        if ((mana + waveBonusMana) <= maxMana)
        {
            mana += waveBonusMana;
            if (onManaChange != null)
            {
                onManaChange(mana);
            }
        }

    }

    private void repairTower(bool value)
    {
        sound.PlayOneShot(repairSound, 1f);
        if (mana >= repairManaCost)
        {
            mana -= repairManaCost;
            tower.repair();
            if (onManaChange != null)
            {
                onManaChange(mana);
            }
        }
    }

    private void launchSpell()
    {
        sound.PlayOneShot(destroySound, 1f);
        Spell spell = spellPrefab.GetComponent<Spell>();
        if (mana >= spell.manaCost)
        {
            mana -= spell.manaCost;

            GameObject spellGO = Instantiate(spellPrefab, hit.point, Quaternion.identity) as GameObject;
            Destroy(spellGO, 2f);
            if (onManaChange != null)
            {
                onManaChange(mana);
            }
        }
    }
    /*
        private void launchTower()
        {
            Tower towerInvoke = towerObject.GetComponent<Tower>();
            GameObject towertInvokeGo = Instantiate(towerObject, hit.point, Quaternion.identity) as GameObject;
        }
        private void launchTurret()
        {
            TurretBehaviour turretInvoke = turretObject.GetComponent<TurretBehaviour>();
            GameObject turretInvokeGo = Instantiate(turretObject, hit.point, Quaternion.identity) as GameObject;
        }*/
    /*   private void trackOthersImageTarget(bool value)
       {
           onInstantiate = true;
           coroutine = waitForOthersTrackingOK();
           StartCoroutine(coroutine);
       }

       IEnumerator waitForOthersTrackingOK()
       {
           while (onInstantiate)
           {
               Vector3 temp = new Vector3(vuforiaOthersHandler.transform.position.x, floor.transform.position.y, vuforiaOthersHandler.transform.position.z);
               Debug.DrawLine(vuforiaOthersHandler.transform.position, temp, Color.red);
               Instantiate(towerObject, temp, Quaternion.identity);
               yield return new WaitForSeconds(4.0f);
           }
           Debug.Log("Tenemos un imageOthersTarget activo!! ");
           //   Debug.Log(vuforiaOthersHandler.transform.position);

       }*/

    private void stopInstantiate(bool value)
    {
        onInstantiate = false;
        StopCoroutine(coroutine);
    }

/*    private void trackEnemyImageTarget(bool value)
    {
        onInstantiateEnemy = true;
        coroutineEnemy = waitForEnemyTrackingOK();
        StartCoroutine(coroutineEnemy);
    }

    IEnumerator waitForEnemyTrackingOK()
    {
        while (onInstantiateEnemy)
        {
            Vector3 temp = new Vector3(vuforiaEnemyHandler.transform.position.x, floor.transform.position.y, vuforiaEnemyHandler.transform.position.z);
            Debug.DrawLine(vuforiaEnemyHandler.transform.position, temp, Color.red);
            Instantiate(enemyObject, temp, Quaternion.identity);
            GameObject enemy = Instantiate(enemyObject, temp, Quaternion.identity) as GameObject;
            enemy.transform.SetParent(parent.transform);
            enemy.SetActive(true);
            yield return new WaitForSeconds(5.0f);
        }
        Debug.Log("Tenemos un imageOthersTarget activo!! ");
        //   Debug.Log(vuforiaOthersHandler.transform.position);

    }

    private void stopInstantiateENemy(bool value)
    {
        onInstantiateEnemy = false;
        StopCoroutine(coroutineEnemy);
    }*/
}
