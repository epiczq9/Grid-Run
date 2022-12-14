using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using Timers;

public class BallBehaviour : MonoBehaviour
{
    public bool isMoving = false;
    private Vector3 origPos, targetPos;
    public float timeToMove = 0.05f;
    public bool collided = false;
    float rayLength = 1f;
    public bool safe;
    public Vector3 startingBallPosition;
    public GameObject hitEffectPrefab;
    public GameObject gameManager;
    public bool respawning = false;
    public GameObject finalSquare;
    public Transform ballLiftPos, squareLiftPos;
    public GameObject redScreen;
    public float timeToFade = 1f;
    public int nextLevel;
    public GameObject gameMngr;

    public Text outputText;

    private Vector2 startTouchPosition;
    private Vector2 currentTouchPosition;
    private Vector2 endTouchPosition;
    private bool stopTouch = false;

    public float swipeRange;
    public float tapRange;

    // Start is called before the first frame update
    void Start() {
        startingBallPosition = transform.position;
        safe = true;
    }

    // Update is called once per frame
    void Update() {

        if (Input.GetAxis("Horizontal") < 0 && !isMoving) {
            StartCoroutine(MovePlayer(Vector3.left));
            //Move(Vector3.left);
        }
        if (Input.GetAxis("Horizontal") > 0 && !isMoving) {
            StartCoroutine(MovePlayer(Vector3.right));
            //Move(Vector3.right);
        }
        if (Input.GetAxis("Vertical") < 0 && !isMoving) {
            StartCoroutine(MovePlayer(Vector3.down));
            //Move(Vector3.down);
        }
        if (Input.GetAxis("Vertical") > 0 && !isMoving) {
            StartCoroutine(MovePlayer(Vector3.up));
            //Move(Vector3.up);
        }

        Swipe();

        if (redScreen.GetComponent<Image>().color.a > 0) {
            Color color = redScreen.GetComponent<Image>().color;
            color.a -= timeToFade * Time.deltaTime;
            redScreen.GetComponent<Image>().color = color;

        }
    }

    public void Swipe() {
        if (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Began) {
            startTouchPosition = Input.GetTouch(0).position;
        }
        if (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Moved) {
            currentTouchPosition = Input.GetTouch(0).position;
            Vector2 Distance = currentTouchPosition - startTouchPosition;

            if (!stopTouch) {
                if (Distance.x < -swipeRange && !isMoving && Mathf.Abs(Distance.x) > Mathf.Abs(Distance.y)) {
                    outputText.text = "Left";
                    StartCoroutine(MovePlayer(Vector3.left));
                    stopTouch = true;
                } else if (Distance.x > swipeRange && !isMoving && Mathf.Abs(Distance.x) > Mathf.Abs(Distance.y)) {
                    outputText.text = "Right";
                    StartCoroutine(MovePlayer(Vector3.right));
                    stopTouch = true;
                } else if (Distance.y > swipeRange && !isMoving && Mathf.Abs(Distance.x) < Mathf.Abs(Distance.y)) {
                    outputText.text = "Up";
                    StartCoroutine(MovePlayer(Vector3.up));
                    stopTouch = true;
                } else if (Distance.y < -swipeRange && !isMoving && Mathf.Abs(Distance.x) < Mathf.Abs(Distance.y)) {
                    outputText.text = "Down";
                    StartCoroutine(MovePlayer(Vector3.down));
                    stopTouch = true;
                }
            }
        }

        if (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Ended) {
            stopTouch = false;
            endTouchPosition = Input.GetTouch(0).position;
            Vector2 Distance = endTouchPosition - startTouchPosition;

            if (Mathf.Abs(Distance.x) < tapRange && Mathf.Abs(Distance.y) < tapRange) {
                outputText.text = "Tap";
            }
        }
    }

    private IEnumerator MovePlayer(Vector3 direction) {
        isMoving = true;

        float elapsedTime = 0f;

        origPos = transform.position;
        targetPos = transform.position + direction;

        while (!collided) {
            Ray ray = new Ray(origPos, direction);
            Debug.DrawRay(ray.origin, ray.direction, Color.red);
            if (!Physics.Raycast(ray, out RaycastHit hit, rayLength) && !respawning) {
                transform.position = Vector3.Lerp(origPos, targetPos, (elapsedTime / timeToMove));
                if (transform.position == targetPos) {
                    origPos = transform.position;
                    targetPos = transform.position + direction;
                    elapsedTime = 0;
                }
            } else if (hit.collider.CompareTag("Player") || hit.collider.CompareTag("Safe") || hit.collider.CompareTag("Start") || hit.collider.CompareTag("Square")) {
                transform.position = Vector3.Lerp(origPos, targetPos, (elapsedTime / timeToMove));
                if (transform.position == targetPos) {
                    origPos = transform.position;
                    targetPos = transform.position + direction;
                    elapsedTime = 0;
                }
            } else if (hit.collider.CompareTag("Wall")) {
                transform.position = origPos;
                collided = true;
            } else if (hit.collider.CompareTag("Finish")) {
                transform.position = origPos;
                collided = true;
                transform.DOMove(ballLiftPos.position, 5f);
                finalSquare.transform.DOMove(squareLiftPos.position, 5f);
                GetComponent<BallBehaviour>().enabled = false;
                TimersManager.SetTimer(this, 3f, SwitchScene);
                
            } else if (hit.collider.CompareTag("Laser") && !safe) {
                Death();
                collided = true;
            } else if (hit.collider.CompareTag("Block") && !safe) {
                Block();
                collided = true;
            }


            elapsedTime += Time.deltaTime;
            yield return null;
        }

        isMoving = false;
        collided = false;
    }

    /*
    private void OnCollisionEnter(Collision collision) {
        if (collision.gameObject.CompareTag("Laser") && !safe) {
            Death();
        }
    }*/

    private void OnTriggerEnter(Collider other) {
        if (other.gameObject.CompareTag("Safe") || other.gameObject.CompareTag("Start")) {
            safe = true;
        } else if (other.gameObject.CompareTag("Laser") && !safe) {
            respawning = true;
            collided = true;
            Death();
        }
        if (other.gameObject.CompareTag("Block")){
            Block();
            collided = true;
        }
    }

    private void OnTriggerExit(Collider other) {
        if (other.gameObject.CompareTag("Safe") || other.gameObject.CompareTag("Start")) {
            safe = false;
        }
    }

    private void OnTriggerStay(Collider other) {
        if (other.gameObject.CompareTag("Safe") || other.gameObject.CompareTag("Start")) {
            safe = true;
        }
    }

    public void Death() {
        //Destroy(gameObject);
        //Respawn();
        Instantiate(hitEffectPrefab, this.transform);
        RedScreenActivate();
        transform.position = startingBallPosition;
        origPos = startingBallPosition;
        targetPos = startingBallPosition;
        //gameManager.GetComponent<GameManager>().LoadLevel();
        respawning = false;
    }

    public void Block() {
        transform.position = startingBallPosition;
        origPos = startingBallPosition;
        targetPos = startingBallPosition;
        respawning = false;
    }

    public void RedScreenActivate() {
        Color color = redScreen.GetComponent<Image>().color;
        color.a = 0.5f;
        redScreen.GetComponent<Image>().color = color;
    }

    public void SwitchScene() {
        gameMngr.GetComponent<GameManager>().LoadLevel();
    }

}
