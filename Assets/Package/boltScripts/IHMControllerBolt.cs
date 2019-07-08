using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class IHMControllerBolt : Bolt.EntityBehaviour<ICanvasState>
{
    public Text waveText;
    public Text manaText;
    public Text scoreText;
    private int waveCounter;
    private int score;
    public override void Attached()
    {
        state.PlayerScore = score;
        state.AddCallback("PlayerScore", ScoreCallback);
        state.AddCallback("WaveText", WaveCallback);

    }

    public void ScoreCallback()
    {
        Debug.LogWarning("Score");
        score = state.PlayerScore;
        //  Debug.LogWarning(mana);
        //        manaText.text = "Mana: " + mana;
        scoreText.text = "Score: " + score;

    }
    public void WaveCallback()
    {
        Debug.LogWarning("Wave");
        waveCounter = state.WaveText;
        //  Debug.LogWarning(mana);
        //        manaText.text = "Mana: " + mana;
        waveText.text = "Wave: " + waveCounter;

    }


    void OnEnable()
    {
        WavesGenerator.onNewWave += updateWaveCounter;
        PlayerControllerBolt.onManaChange += updateManaBar;
        ServerEventListener.onScoreChange += updateScoreBar;
        Enemy.onScoreChange += updateScoreBar;
        Spell.onScoreChange += updateScoreBar;
        GameController.onScoreChange += resetScoreBar;
    }
    void OnDisable()
    {
        WavesGenerator.onNewWave -= updateWaveCounter;
        PlayerControllerBolt.onManaChange -= updateManaBar;
        ServerEventListener.onScoreChange -= updateScoreBar;
        Enemy.onScoreChange -= updateScoreBar;
        Spell.onScoreChange -= updateScoreBar;
        GameController.onScoreChange -= resetScoreBar;
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void updateWaveCounter(int currentWave)
    {
        Debug.Log("Wave: " + currentWave);
        state.WaveText = currentWave;   
        //waveText.text = "Wave: " + state.WaveText;

        //waveCounter = currentWave;
        //score = currentWave * 10;
        //scoreText.text = "Score:" + score;
        if (state.PlayerScore > PlayerPrefs.GetInt("HighScore", 0))
        {
            PlayerPrefs.SetInt("HighScore", state.PlayerScore);
        }
    }
    public void updateManaBar(int manaValue)
    {
        manaText.text = "Mana: " + manaValue;
    }
    public void updateScoreBar(int scoreValue)
    {
        Debug.LogWarning("ScoreValue"+scoreValue);
        state.PlayerScore += scoreValue;
    }
    public void resetScoreBar(int scoreValue)
    {
        Debug.LogWarning("ResetScore");
        state.PlayerScore = scoreValue;
    }

}
