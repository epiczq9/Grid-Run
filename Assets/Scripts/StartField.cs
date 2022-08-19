using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartField : MonoBehaviour
{
    private Vector3 newStartingPosition;
    public GameObject backBlock;
    private void Start() {
        newStartingPosition = new Vector3(transform.position.x, transform.position.y, -1);
    }
    private void OnTriggerEnter(Collider other) {
        if (other.gameObject.CompareTag("Player")) {
            other.gameObject.GetComponent<BallBehaviour>().startingBallPosition = newStartingPosition;
            backBlock.SetActive(true);
        }
    }
}
