using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Activator : MonoBehaviour
{
    public void ActivateGO(GameObject go) {
        go.SetActive(true);
    }

    public void DeactivateGO(GameObject go) {
        go.SetActive(false);
    }
}
