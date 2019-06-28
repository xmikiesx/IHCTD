using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tower : MonoBehaviour
{
    public int life;
    public int initialLife;
    public TextMesh lifeIHM;
    public GameObject model3D;
    public delegate void OnGameOver(bool value);
    public static event OnGameOver onGameOver;
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
        if (life <= 0)
        {
            life = 0;
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
