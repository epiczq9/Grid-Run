using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public int level;

    private void Awake() {
        Application.targetFrameRate = 60;
    }
    public void LoadLevel() {
        SceneManager.LoadScene(level);
    }
}
