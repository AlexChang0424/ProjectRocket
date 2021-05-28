using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
    // PARAMETERS - for tunning, typically set in the editor
    // CACHE - e.g. reference for readability or speed
    // STATE - private instance (member) variables
    [SerializeField] float thrustSpeed = 100f;
    [SerializeField] float rotateSpeed = 100f;
    [SerializeField] AudioClip mainEngine;
    Rigidbody rb;
    AudioSource audioSource;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        audioSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        ProcessThrust();
        ProcessRotation();
    }

    void ProcessThrust()
    {
        if (Input.GetKey(KeyCode.Space))
        {
            rb.AddRelativeForce((Vector3.up) * thrustSpeed * Time.deltaTime);
            if (!audioSource.isPlaying)
            {
                audioSource.PlayOneShot(mainEngine);
            }
        }
        else
        {
            audioSource.Stop();
        }
    }

    void ProcessRotation()
    {
        if (Input.GetKey(KeyCode.A))
        {
            ApplyRotation(rotateSpeed);
        }
        else if (Input.GetKey(KeyCode.D))
        {
            ApplyRotation(-rotateSpeed);
        }
    }

    void ApplyRotation(float rotationThisFrame)
    {
        rb.freezeRotation = true; // freezing rotation so we can manually rotate
        transform.Rotate((Vector3.forward) * rotationThisFrame * Time.deltaTime);
        rb.freezeRotation = false; // unfreeze rotation so the physic system can take over
    }
}
