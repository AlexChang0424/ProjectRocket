using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Oscillator : MonoBehaviour
{
    Vector3 initialPosition;
    [SerializeField] Vector3 movementVector;
    float movementFactor;
    [SerializeField] float period = 2f;

    // Start is called before the first frame update
    void Start()
    {
        initialPosition = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        if (period <= Mathf.Epsilon) { return; }
        float cycles = Time.time / period; // continually growing over time 
        const float tau = Mathf.PI * 2; // constant value of 6.283
        float rawSinwave = Mathf.Sin(cycles * tau); // going from 1 to -1

        movementFactor = (rawSinwave + 1f) / 2f; // recalculate to go from 0 to 1 so its cleaner

        Vector3 offset = movementVector * movementFactor;
        transform.position = initialPosition + offset;
    }
}
