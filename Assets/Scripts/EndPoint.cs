using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndPoint : MonoBehaviour
{
    public GameObject score;
    public AudioSource clip;

    private void Start() {
        //clip = GetComponent<AudioSource>();
    }
    private void OnTriggerEnter(Collider other) {
        if (other.gameObject.CompareTag("Player")) {
            //clip.Play(0);
            score.GetComponent<Score>().timerRunning = false;
        }
    }
}
