using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Score : MonoBehaviour
{
    public Text scoreText;
    public int activatedSpots = 0;
    //public GameObject[] safeSpots;
    public int safeSpotCount;
    public GameObject exitGate;
    public Image scoreBarFill;
    private float timePassed = 0;
    public bool timerRunning = true;
    void Start() {
        safeSpotCount = GameObject.FindGameObjectsWithTag("Safe").Length;
        scoreText = GetComponent<Text>();
    }

    void Update() {
        if(timerRunning) {
            timePassed += Time.deltaTime;
            scoreText.text = "Time: " + timePassed.ToString("F2");
        }
        
    }

    public void ActivateSpot() {
        activatedSpots++;
        scoreBarFill.fillAmount = (float)activatedSpots / (float)safeSpotCount;
        Debug.Log(scoreBarFill.fillAmount);
    }
}
