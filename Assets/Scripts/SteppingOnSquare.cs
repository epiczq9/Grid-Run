using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class SteppingOnSquare : MonoBehaviour
{
    private AudioSource clickSound;

    private void Start() {
        clickSound = GetComponent<AudioSource>();
    }
    private void OnTriggerEnter(Collider other) {
        if (other.gameObject.CompareTag("Player")) {
            transform.localScale = new Vector3(0.4f, 0.4f, 0.4f);
            clickSound.Play(0);
        }
    }

    private void OnTriggerStay(Collider other) {
        if (other.gameObject.CompareTag("Player")) {
            transform.localScale = new Vector3(0.4f, 0.4f, 0.4f);
        }
    }


    private void OnTriggerExit(Collider other) {
        if (other.gameObject.CompareTag("Player")) {
            transform.DOScale(new Vector3(0.5f, 0.5f, 0.5f), 0.3f);
        }
    }
}
