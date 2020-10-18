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
    Renderer render;

    private Rigidbody rb;
    private int count;
    private Vector3 movement;
    private Vector3 originalPosition;
    private float movementX;
    private float movementY;



    // Start is called before the first frame update
    void Start()
    {
        originalPosition = transform.position;
        render = GetComponent<Renderer>();
        rb = GetComponent<Rigidbody>();
        count = 0;

        SetCountText();

        winTextObject.SetActive(false);
    }

    void OnMove(InputValue movementValue)
    {

        Vector2 movementVector = movementValue.Get<Vector2>();

        movementX = movementVector.x;
        movementY = movementVector.y;
    }

    void OnDash()
    {
        if (rb.velocity.x < 7f && rb.velocity.z < 7f && rb.velocity.x > -7f && rb.velocity.z > -7f)
        {
            rb.AddForce(movement * (speed * 20));
        }

    }

    void SetCountText()
    {
        countText.text = "Count: " + count.ToString() + " / 18";

        if (count >= 18)
        {
            winTextObject.SetActive(true);
            Invoke("Application.Quit()", 3);

        }
    }

    void FixedUpdate()
    {
        movement = new Vector3(movementX, 0.0f, movementY);

        rb.AddForce(movement * speed);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("PickUp"))
        {
            other.gameObject.SetActive(false);
            count = count + 1;

            SetCountText();
            if (other.gameObject.GetComponent<Renderer>().material.GetColor("_BaseColor") == Color.red)
            {
                render.material.SetColor("_BaseColor", Color.red);
            }

            if (other.gameObject.GetComponent<Renderer>().material.GetColor("_BaseColor") == Color.cyan)
            {
                render.material.SetColor("_BaseColor", Color.cyan);
            }
        }

        if (other.gameObject.CompareTag("PassThrough"))
        {
            if (other.gameObject.GetComponent<Renderer>().material.GetColor("_BaseColor") == Color.red && render.material.GetColor("_BaseColor") == Color.red)
            {
                other.gameObject.SetActive(false);
            }

            if (other.gameObject.GetComponent<Renderer>().material.GetColor("_BaseColor") == Color.cyan && render.material.GetColor("_BaseColor") == Color.cyan)
            {
                other.gameObject.SetActive(false);
            }
        }

        if (other.gameObject.CompareTag("Respawn"))
        {
            transform.position = originalPosition;
        }
    }
}