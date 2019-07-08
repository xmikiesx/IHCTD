using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class PlayerControllerBolt : Bolt.EntityBehaviour<IPlayerARState>
{
    public int mana;
    public int initialMana = 100;
    public TowerBolt tower;
    //public GameObject floor;
    public GameObject spellPrefab;
    public float maxDistance;

    public int waveBonusMana = 20;
    public int maxMana = 500;

    public int repairManaCost;
    public int turretManaCost;
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

    public Text manaText;

    public EnemyHandler vuforiaEnemyHandler;
    private IEnumerator coroutineEnemy;
    private bool onInstantiateEnemy;

    public GameObject turretObject;

    void OnEnable()
    {
        GameController.onGameStart += reset;
        WavesGenerator.onNewWave += updateMana;
        PlayerVRControllable.onManaChange += addMana;

    }
    void OnDisable()
    {
        GameController.onGameStart -= reset;
        WavesGenerator.onNewWave -= updateMana;
        PlayerVRControllable.onManaChange -= addMana;
    }

    public override void Attached()
    {
        state.PlayerMana = initialMana;
        Debug.LogWarning("InitMana");
        Debug.LogWarning(state.PlayerMana);
        state.AddCallback("PlayerMana", ManaCallback);
    }

    private void ManaCallback()
    {
        Debug.LogWarning("Mana");
        mana = state.PlayerMana;
        Debug.LogWarning(mana);
        //        manaText.text = "Mana: " + mana;
        if (onManaChange != null)
        {
            onManaChange(state.PlayerMana);
        }

    }


    // Start is called before the first frame update
    void Start()
    {
        sound = GetComponent<AudioSource>();
        vuforiaEnemyHandler = GameObject.Find("EnemyTarget").GetComponent<EnemyHandler>();
    }

    // Update is called once per frame
    public override void SimulateOwner()
    {
        if (Input.GetKeyDown(KeyCode.R))
        {
            repairTower(true);
        }
        if (Input.GetMouseButton(0) && !EventSystem.current.IsPointerOverGameObject())
        {
            Debug.LogWarning("RayCasttt");
            Debug.Log(vuforiaEnemyHandler.isReady);
            Debug.LogWarning("RayCasttt5");
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out hit, maxDistance))
            {
                Debug.LogWarning("RayCasttt2");

                Debug.Log("Toco" + hit.collider.gameObject.name + " en Vector3:" + hit.point);
                Debug.DrawLine(ray.origin, hit.point, Color.red);

                if (hit.collider.gameObject.CompareTag("Enemy") || hit.collider.gameObject.CompareTag("Floor"))
                {
                    if (vuforiaEnemyHandler.isReady)
                    {
                        launchTurret();
                    }
                    else
                    {
                        launchSpell();
                    }
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
        state.PlayerMana = initialMana;
        repairTower(true);
    }
    private void updateMana(int currentWave)
    {
        if ((mana + waveBonusMana) <= maxMana)
        {
           // mana += waveBonusMana;
            //if (onManaChange != null)
            //{
           //     onManaChange(mana);
           // }
        }
    }
    private void addMana(int energy)
    {
        state.PlayerMana += energy;
    }
    private void repairTower(bool value)
    {
        sound.PlayOneShot(repairSound, 1f);
        if (mana >= repairManaCost)
        {
            state.PlayerMana -= repairManaCost;
           // mana -= repairManaCost;

            //tower.repair();
            GameObject.Find("TowerBolt(Clone)").GetComponent<TowerBolt>().repair();
            //if (onManaChange != null)
            //{
            //    onManaChange(state.PlayerMana);
            //}
        }
    }

    private void launchSpell()
    {
        sound.PlayOneShot(destroySound, 1f);
        Spell spell = spellPrefab.GetComponent<Spell>();
        if (mana >= spell.manaCost)
        {
            state.PlayerMana -= spell.manaCost;
           // mana -= spell.manaCost;

            GameObject spellGO = Instantiate(spellPrefab, hit.point, Quaternion.identity) as GameObject;
            Destroy(spellGO, 2f);
           // if (onManaChange != null)
           // {
           //     onManaChange(mana);
           // }
        }
    }

    private void stopInstantiate(bool value)
    {
        onInstantiate = false;
        StopCoroutine(coroutine);
    }
    private void launchTurret()
    {
        //TurretBehaviour turretInvoke = turretObject.GetComponent<TurretBehaviour>();
        if (mana >= turretManaCost)
        {
            GameObject turretInvokeGo = BoltNetwork.Instantiate(turretObject, hit.point, Quaternion.identity);
        }
    }
}