using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Swiping : MonoBehaviour
{
    public Text outputText;

    private Vector2 startTouchPosition;
    private Vector2 currentTouchPosition;
    private Vector2 endTouchPosition;
    private bool stopTouch = false;

    public float swipeRange;
    public float tapRange;

    // Update is called once per frame
    void Update() {
        Swipe();
    }

    public void Swipe() {
        if(Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Began) {
            startTouchPosition = Input.GetTouch(0).position;
        }
        if (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Moved) {
            currentTouchPosition = Input.GetTouch(0).position;
            Vector2 Distance = currentTouchPosition - startTouchPosition;

            if (!stopTouch) {
                if(Distance.x < -swipeRange) {
                    outputText.text = "Left";
                    stopTouch = true;
                } else if (Distance.x > swipeRange) {
                    outputText.text = "Right";
                    stopTouch = true;
                }
                else if(Distance.y > swipeRange) {
                    outputText.text = "Up";
                    stopTouch = true;
                }
                else if(Distance.y < -swipeRange) {
                    outputText.text = "Down";
                    stopTouch = true;
                }
            }
        }

        if (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Ended) {
            stopTouch = false;
            endTouchPosition = Input.GetTouch(0).position;
            Vector2 Distance = endTouchPosition - startTouchPosition;

            if(Mathf.Abs(Distance.x) < tapRange && Mathf.Abs(Distance.y) < tapRange) {
                outputText.text = "Tap";
            }
        }
    }
}
