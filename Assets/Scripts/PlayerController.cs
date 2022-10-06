using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float speed = 500;
    private Rigidbody rb;


    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }


    // Update is called once per frame
    void FixedUpdate()
    {
        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input .GetAxis("Vertical");
        
        rb.AddForce(horizontal * speed * Time.deltaTime, 0.0f, vertical * speed * Time.deltaTime);
    }


    void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("PickUp")){
            other.gameObject.SetActive(false);
        }
    }
}
