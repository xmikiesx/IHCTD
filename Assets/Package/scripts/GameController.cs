using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
    public Tower tower;
    public MyHandler vuforiaHandler;
    public delegate void OnGameStart(bool value);
    public static event OnGameStart onGameStart;
    public delegate void OnGameReady(bool value);
    public static event OnGameReady onGameReady;
    private bool gameOn;



    void OnEnable()
    {
        Tower.onGameOver += endGame;
    }
    void OnDisable()
    {
        Tower.onGameOver -= endGame;
    }
    // Start is called before the first frame update
    void Start()
    {
        trackImageTarget();
    }

    // Update is called once per frame
    void Update()
    {
        /*if(gameOn){
            if (Input.GetKeyDown(KeyCode.R)){
                tower.repair();
            }
            if (Input.GetKeyDown(KeyCode.O)){
                tower.damage(-2);
            }
        }*/
    }
    void endGame(bool value)
    {
        //  Debug.Log("GAMEOVER desde GameController");
        gameOn = false;
    }
    private void trackImageTarget()
    {
        /* if (vuforiaHandler.isReady)
         {
             if (onGameStart != null)
             {
                 onGameStart();
                 Debug.Log("GAMESTART desde GameController");
             }
            gameOn = true;
         }*/
        StartCoroutine(waitForTrackingOK());
    }
    IEnumerator waitForTrackingOK()
    {
        Debug.Log("Entre a la corutina");
        while (!vuforiaHandler.isReady)
        {
            yield return new WaitForSeconds(0.5f);
        }
        if (onGameReady != null)
        {
            onGameReady(true);
            //  Debug.Log("GAMEREADY desde GameController");
        }
        Debug.Log("Tenemos un imageTarget activo!! ");
    }

    public void launchGame()
    {
        gameOn = true;
        Debug.Log("El juego se ha iniciado");
        if (onGameStart != null)
        {
            onGameStart(false);
            Debug.Log("GAMESTART desde GameController");
        }
    }
    public void reset()
    {
        Debug.Log("Reiniciaste");
        launchGame();
    }
}