using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Timers;
using DG.Tweening;

public class LaserController : MonoBehaviour
{
    AudioSource siu;
    public bool laserActivated = false;
    public float xPos = 1;
    public Transform laserStart, laserEnd;
    private Transform laserMovingTo;
    public float speedToMove = 1.5f;
    public float interval;
    public bool dangerous;
    void Start() {
        siu = GetComponent<AudioSource>();
        TimersManager.SetLoopableTimer(this, interval, ActivateLaser);
    }

    // Update is called once per frame
    void Update() {
        if (laserActivated) {
            Move();
        }

        if(xPos == 1) {
            laserMovingTo = laserStart;
        } else {
            laserMovingTo = laserEnd;
        }
    }

    public void ActivateLaser() {
        laserActivated = true;
    }

    public void Move() {
        siu.Play(0);
        transform.DOMove(laserMovingTo.position, speedToMove).SetEase(Ease.Linear);
        laserActivated = false;
        xPos *= -1;
    }
}

