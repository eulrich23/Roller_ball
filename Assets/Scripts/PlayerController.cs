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
    private float movementX;
    private float movementY;


    // Start is called before the first frame update
    void Start()
    {
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

    void SetCountText()
    {
        countText.text = "Count: " + count.ToString();

        if(count >= 8)
        {
            winTextObject.SetActive(true);
        }
    }

    void FixedUpdate()
    {
        Vector3 movement = new Vector3(movementX, 0.0f, movementY);

        rb.AddForce(movement * speed);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("PickUp"))
        {
            other.gameObject.SetActive(false);
            count = count + 1;

            SetCountText();
            if(other.gameObject.GetComponent<Renderer>().material.GetColor("_BaseColor") == Color.red)
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

            if(other.gameObject.GetComponent<Renderer>().material.GetColor("_BaseColor") == Color.cyan && render.material.GetColor("_BaseColor") == Color.cyan)
            {
                other.gameObject.SetActive(false);
            }
        }
    }
}
