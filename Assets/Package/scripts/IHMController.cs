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

    private int waveCounter;
    // Start is called before the first frame update
    void OnEnable()
    {
        GameController.onGameStart += toggleStartPanel;
        GameController.onGameReady += toggleStartPanel;
        Tower.onGameOver += toggleGameOverPanel;
        WavesGenerator.onNewWave += updateWaveCounter;
        PlayerController.onManaChange += updateManaBar;
    }
    void OnDisable()
    {
        GameController.onGameStart -= toggleStartPanel;
        GameController.onGameReady += toggleStartPanel;
        Tower.onGameOver += toggleGameOverPanel;
        WavesGenerator.onNewWave -= updateWaveCounter;
        PlayerController.onManaChange -= updateManaBar;
    }
    void Start()
    {
        startPanel.SetActive(false);
        endPanel.SetActive(false);
    }

    // Update is called once per frame
    private void toggleStartPanel(bool value)
    {
        startPanel.SetActive(value);
    }
    public void toggleGameOverPanel(bool visible)
    {
        endPanel.SetActive(visible);
        if (visible)
        {
            endText.text = "Bravo!! Haz aguantado" + waveCounter + " olas";
        }
    }
    public void updateWaveCounter(int currentWave)
    {
        waveText.text = "Wave: " + currentWave;
        waveCounter = currentWave;
    }
    private void updateManaBar(int manaValue)
    {
        manaText.text = "Mana: " + manaValue;
    }
}