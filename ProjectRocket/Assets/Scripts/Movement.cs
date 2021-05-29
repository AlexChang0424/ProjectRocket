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
    [SerializeField] ParticleSystem mainThruster;
    [SerializeField] ParticleSystem leftThruster;
    [SerializeField] ParticleSystem rightThruster;
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
            Thrusting();
        }
        else
        {
            NotThrusting();
        }
    }
    void ProcessRotation()
    {
        if (Input.GetKey(KeyCode.A))
        {
            LeftRotate();
        }
        else if (Input.GetKey(KeyCode.D))
        {
            RightRotate();
        }
        else
        {
            NotRotating();
        }
    }
    void Thrusting()
    {
        rb.AddRelativeForce((Vector3.up) * thrustSpeed * Time.deltaTime);
        if (!audioSource.isPlaying)
        {
            audioSource.PlayOneShot(mainEngine);
        }
        if (!mainThruster.isPlaying)
        {
            mainThruster.Play();
        }
    }
    void NotThrusting()
    {
        audioSource.Stop();
        mainThruster.Stop();
    }
    void LeftRotate()
    {
        ApplyRotation(rotateSpeed);
        if (!rightThruster.isPlaying)
        {
            rightThruster.Play();
        }
    }
    void RightRotate()
    {
        ApplyRotation(-rotateSpeed);
        if (!leftThruster.isPlaying)
        {
            leftThruster.Play();
        }
    }
    void NotRotating()
    {
        leftThruster.Stop();
        rightThruster.Stop();
    }
    void ApplyRotation(float rotationThisFrame)
    {
        rb.freezeRotation = true; // freezing rotation so we can manually rotate
        transform.Rotate((Vector3.forward) * rotationThisFrame * Time.deltaTime);
        rb.freezeRotation = false; // unfreeze rotation so the physic system can take over
    }
}
