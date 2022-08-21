using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SafeSpot : MonoBehaviour
{
    public GameObject scoreManager, effectPrefab;
    public Material matActivated;
    public bool activated = false;
    public AudioSource clip;

    private void Start() {
        clip = GetComponent<AudioSource>();
    }

    private void OnTriggerEnter(Collider other) {
        if (other.gameObject.CompareTag("Player")) {
            if (!activated) {
                clip.Play(0);
                activated = true;
                Instantiate(effectPrefab, other.gameObject.transform);
                //scoreManager.GetComponent<Score>().ActivateSpot();
                GetComponent<MeshRenderer>().material = matActivated;
            }
        }
    }
}
