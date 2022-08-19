using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class CameraManager : MonoBehaviour
{
    public GameObject[] virtualCameras;

    public void SwitchCamera(int cam) {
        for(int i = 0; i < virtualCameras.Length; i++) {
            if(i == cam) {
                virtualCameras[i].GetComponent<CinemachineVirtualCamera>().Priority = 15;
            } else {
                virtualCameras[i].GetComponent<CinemachineVirtualCamera>().Priority = 10;
            }
        }
    }
}
