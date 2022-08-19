using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BeginGame: MonoBehaviour
{
    public int level;

    private void Start() {
        SceneManager.LoadScene(1);
    }
}
