using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class BallBehaviour : MonoBehaviour
{
    public bool isMoving = false;
    private Vector3 origPos, targetPos;
    public float timeToMove = 0.05f;
    public bool collided = false;
    float rayLength = 1f;
    public bool safe;
    public Vector3 startingBallPosition;
    public GameObject ballPrefab;
    public GameObject gameManager;
    public bool respawning = false;
    public GameObject finalSquare;

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
                //finish.GetComponent<Finish>().MoveUp();
                transform.DOMove(new Vector3(-26, -6, -12), 5f);
                finalSquare.transform.DOMove(new Vector3(-26, -6, -11), 5f);
            } else if (hit.collider.CompareTag("Laser")) {
                Death();
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
        }
        if (other.gameObject.CompareTag("Laser") && !safe) {
            respawning = true;
            collided = true;
            Death();
        }
    }

    private void OnTriggerExit(Collider other) {
        if (other.gameObject.CompareTag("Safe") || other.gameObject.CompareTag("Start")) {
            safe = false;
        }
    }

    public void Death() {
        //Destroy(gameObject);
        //Respawn();
        transform.position = startingBallPosition;
        origPos = startingBallPosition;
        targetPos = startingBallPosition;
        //gameManager.GetComponent<GameManager>().LoadLevel();
        respawning = false;
    }

    public void Respawn() {
        //Instantiate(ballPrefab, startingBallPosition);
    }
    
    public void Finished() {
    }
}
