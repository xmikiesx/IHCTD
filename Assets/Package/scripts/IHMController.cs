using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class IHMController : MonoBehaviour
{
    public GameObject startPanel;
    public GameObject endPanel;
    public Text endText;
    public Text waveText;
    public Text manaText;
    public Text scoreText;
    public Text highScoreText;
    public Text winLoseText;

    private int waveCounter;
    private int score;
    // Start is called before the first frame update
    void OnEnable()
    {
        GameController.onGameStart += toggleStartPanel;
        GameController.onGameReady += toggleStartPanel;
      //  Tower.onGameOver += toggleGameOverPanel;
        TowerBolt.onGameOver += toggleGameOverPanel;
        //WavesGenerator.onNewWave += updateWaveCounter;
        //PlayerControllerBolt.onManaChange += updateManaBar;
    }
    void OnDisable()
    {
        GameController.onGameStart -= toggleStartPanel;
        GameController.onGameReady += toggleStartPanel;
       // Tower.onGameOver += toggleGameOverPanel;
        TowerBolt.onGameOver += toggleGameOverPanel;
        //WavesGenerator.onNewWave -= updateWaveCounter;
        //PlayerControllerBolt.onManaChange -= updateManaBar;
    }
    void Start()
    {
        startPanel.SetActive(false);
        endPanel.SetActive(false);
        highScoreText.text = "High Score: " + PlayerPrefs.GetInt("HighScore", 0).ToString();
    }

    // Update is called once per frame
    private void toggleStartPanel(bool value)
    {
        Debug.Log("Ya inicia Cabronnnn  "+value);
        startPanel.SetActive(value);
    }
    public void toggleGameOverPanel(bool visible)
    {
        endPanel.SetActive(visible);
        if (visible)
        {
            endText.text = "Bravo!! Haz aguantado" + waveCounter + " olas";
            if (score > PlayerPrefs.GetInt("HighScore", 0))
            {
                winLoseText.text = "Ganaste!, lograster el mayor puntaje";
            }
            else
            {
                winLoseText.text = "Perdiste, inténtalo de nuevo";
            }
        }
    }
 /*   public void updateWaveCounter(int currentWave)
    {
        waveText.text = "Wave: " + currentWave;
        waveCounter = currentWave;
        //score = currentWave * 10;
        //scoreText.text = "Score:" + score;
        if (score > PlayerPrefs.GetInt("HighScore", 0))
        {
            PlayerPrefs.SetInt("HighScore", score);
        }
    }*/
    private void updateManaBar(int manaValue)
    {
        Debug.LogWarning("wertyuil");
        manaText.text = "Mana: " + manaValue;
    }
}