using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using TMPro;

public class PlayerController : MonoBehaviour
{
    public float speed = 0;
    public TextMeshProUGUI countText;
    public GameObject winTextObject;
    public GameObject loseTextObject;
    public GameObject welcomeTextObject;
    public GameObject PickUp2;
    public GameObject Holes;
    public AudioClip coinSound;
    public AudioClip gameOver;
    public AudioClip youWin;
    private int count; 
    private Rigidbody rb;
    private float movementX;
    private float movementY;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        count = 0;
        SetCountText();
        welcomeTextObject.SetActive(true);
        winTextObject.SetActive(false);
        loseTextObject.SetActive(false);
        PickUp2.SetActive(false);

    }

    void OnMove(InputValue movementValue)
    {
        welcomeTextObject.SetActive(false);
        Vector2 movementVector = movementValue.Get<Vector2>();

        movementX = movementVector.x;
        movementY = movementVector.y;
    }

    void SetCountText()
    {
        countText.text = "Count: " + count.ToString();
        if (count >= 17)
        {
            winTextObject.SetActive(true);
            PlayYouWin();
            speed = 0;
        }
    }

    void FixedUpdate()
    {
        Vector3 movement = new Vector3(movementX, 0.0f, movementY);
        rb.AddForce(movement*speed);

    }

    private void OnTriggerEnter(Collider other) //other = name of the collider
    {
        if (other.gameObject.CompareTag("PickUp"))
        {
            other.gameObject.SetActive(false); // We desactivate the object
            count += 1;
        }

        if (count >= 12)
        {
            PickUp2.SetActive(true);
        }

        if (other.gameObject.CompareTag("PickUp2"))
        {
            other.gameObject.SetActive(false);
            count += 1;
        }

        if (other.gameObject.CompareTag("Hole"))
        {
            EndGame();
        }


        SetCountText();
        PlayCoinSound();

    }

    void EndGame()
    {
        if (count < 17)
        {
            speed = 0;
            loseTextObject.SetActive(true);
            PlayGameOver();

        }

    }

    void PlayCoinSound()
    {
        if (coinSound != null)
        {
            AudioSource.PlayClipAtPoint(coinSound, transform.position);
        }
    }

    void PlayGameOver()
    {
        if (gameOver != null)
        {
            AudioSource.PlayClipAtPoint(gameOver, transform.position);
        }
    }
    void PlayYouWin()
    {
        if (youWin != null)
        {
            AudioSource.PlayClipAtPoint(youWin, transform.position);
        }
    }

}
