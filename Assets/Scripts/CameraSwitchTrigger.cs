using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraSwitchTrigger : MonoBehaviour
{

    public GameObject cameraManager, activator, goToActivate;
    public int cam;
    private void OnTriggerEnter(Collider other) {
        if (other.gameObject.CompareTag("Player")) {
            cameraManager.GetComponent<CameraManager>().SwitchCamera(cam);
            activator.GetComponent<Activator>().ActivateGO(goToActivate);
        }
    }
}
