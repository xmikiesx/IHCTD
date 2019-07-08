using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Tower : MonoBehaviour
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
        lifeIHM.text = "Life :" + life;
        model3D.SetActive(true);
    }

    // Update is called once per frame
    void Update()
    {

    }
    public void damage(int value)
    {
        life += value;
        healthBar.fillAmount = life / initialLife;
        if (life <= 0)
        {
            life = 0;
            Debug.Log("Auxilioooo Me muero");

            if (onGameOver != null)
            {
                onGameOver(true);
            }
            model3D.SetActive(false);

        }
        lifeIHM.text = "Life :" + life;
        //  Debug.Log(" damage life :" + life);

    }
    public void repair()
    {
        Debug.Log("Repair life");
        life = initialLife;
        lifeIHM.text = "Life :" + life;

        if (!model3D.activeInHierarchy)
        {
            model3D.SetActive(true);
        }
    }

}
