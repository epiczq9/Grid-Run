using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class EndPoint : MonoBehaviour
{
    public GameObject score;
    public AudioSource clip;
    public Transform squareLiftPosition, ballLiftPosition;

    private void Start() {
        //clip = GetComponent<AudioSource>();
    }
    private void OnTriggerEnter(Collider other) {
        if (other.gameObject.CompareTag("Player")) {
            //clip.Play(0);
            //score.GetComponent<Score>().timerRunning = false;
            //transform.DOMove(squareLiftPosition.position, 5f);
            //other.gameObject.GetComponent<BallBehaviour>().enabled = false;
            //other.gameObject.transform.DOMove(ballLiftPosition.position, 5f);
        }
    }
}
