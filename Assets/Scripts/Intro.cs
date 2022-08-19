using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;
using Timers;

public class Intro : MonoBehaviour
{
    public GameObject vCam1, vCam2, vCam3;
    public GameObject score, ball, laser;


    void Start() {
        ball.GetComponent<BallBehaviour>().enabled = false;
        vCam1.GetComponent<CinemachineVirtualCamera>().Priority = 10;
        vCam3.GetComponent<CinemachineVirtualCamera>().Priority = 15;
        //TimersManager.SetTimer(this, 2, ChangeCamera);
        TimersManager.SetTimer(this, 4, BeginGame);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ChangeCamera() {
        vCam2.GetComponent<CinemachineVirtualCamera>().Priority = 10;
        vCam3.GetComponent<CinemachineVirtualCamera>().Priority = 15;
    }

    public void BeginGame() {
        score.SetActive(true);
        ball.GetComponent<BallBehaviour>().enabled = true;
        laser.SetActive(true);
    }
}
