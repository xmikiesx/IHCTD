using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class TowerBolt : Bolt.EntityBehaviour<ITowerState>
{
    public float life;
    public float initialLife;
    public TextMesh lifeIHM;
    public GameObject model3D;
    public delegate void OnGameOver(bool value);
    public static event OnGameOver onGameOver;
    public Image healthBar;
    // Start is called before the first frame update
    void Start()
    {
        life = initialLife;
       // lifeIHM.text = "Life :" + life;
       // model3D.SetActive(true);
    }


    public override void Attached()
    {
        state.TowerLife = initialLife;
        Debug.LogWarning("InitLife");
        Debug.LogWarning(state.TowerLife);

        state.AddCallback("TowerLife", LifeCallback);
    }
    private void LifeCallback()
    {
        Debug.LogWarning("Mana");
        life = state.TowerLife;
        healthBar.fillAmount = state.TowerLife / initialLife;

        Debug.LogWarning(life);

        if (state.TowerLife <= 0)
        {
            state.TowerLife = 0;
            Debug.Log("Auxilioooo Me muero");

            if (onGameOver != null)
            {
                onGameOver(true);
            }
            model3D.SetActive(false);

        }
        else
        {
            if (!model3D.activeInHierarchy)
            {
                model3D.SetActive(true);
            }
        }
        lifeIHM.text = "Life :" + state.TowerLife;
        //        manaText.text = "Mana: " + mana;
    }
    public void damage(int value)
    {
        state.TowerLife += value;
        //   life += value;
        //healthBar.fillAmount = state.TowerLife / initialLife;

        /*if (state.TowerLife <= 0)
        {
            state.TowerLife = 0;
            Debug.Log("Auxilioooo Me muero");

            if (onGameOver != null)
            {
                onGameOver(true);
            }
            model3D.SetActive(false);

        }
        lifeIHM.text = "Life :" + state.TowerLife;*/
        //  Debug.Log(" damage life :" + life);

    }
    public void repair()
    {
        Debug.Log("Repair life");
        state.TowerLife = initialLife;
        //life = initialLife;
        /*lifeIHM.text = "Life :" + state.TowerLife;
        //healthBar.fillAmount = state.TowerLife / initialLife;
        if (!model3D.activeInHierarchy)
        {
            model3D.SetActive(true);
        }*/
    }
}
