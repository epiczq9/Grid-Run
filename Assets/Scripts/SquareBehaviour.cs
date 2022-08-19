using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class SquareBehaviour : MonoBehaviour
{
    public Material matStart, matDanger, matStepped;
    public MeshRenderer meshRenderer;
    private AudioSource clickSound;

    private void Start() {
        meshRenderer = GetComponent<MeshRenderer>();
        matStart = meshRenderer.material;
        clickSound = GetComponent<AudioSource>();
    }
    private void OnTriggerEnter(Collider other) {
        if (other.gameObject.CompareTag("Laser")) {
            //meshRenderer.material.color = dangerColor;
            meshRenderer.material = matDanger;
        } else if (other.gameObject.CompareTag("Player")) {
            transform.localScale = new Vector3(0.4f, 0.4f, 0.4f);
            meshRenderer.material = matStepped;
            clickSound.Play(0);
        }
    }
    
    private void OnTriggerStay(Collider other) {
        if (other.gameObject.CompareTag("Player")) {
            transform.localScale = new Vector3(0.4f, 0.4f, 0.4f);
            meshRenderer.material = matStepped;
        }
    }


    private void OnTriggerExit(Collider other) {
        if (other.gameObject.CompareTag("Laser")) {
            //meshRenderer.material.color = startingColor;
            meshRenderer.material = matStart;
        }
        if (other.gameObject.CompareTag("Player")) {
            if(meshRenderer.material != matDanger) {
                RestoreSquare();
            }
        }
    }

    public void RestoreSquare() {
        transform.DOScale(new Vector3(0.5f, 0.5f, 0.5f), 0.3f);
        meshRenderer.material = matStart;
    }
}
