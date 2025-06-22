using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Flotacion : MonoBehaviour
{
    public float floatStrength = 10.0f; // La fuerza del movimiento
    public float speed = 5.0f; // La velocidad de oscilación

    private Vector3 startPos;

    // Start is called before the first frame update
    void Start()
    {
        startPos = transform.position; // Guarda la posición inicial del objeto
    }

    // Update is called once per frame
    void Update()
    {
        // Calcula la posición Y utilizando la función sinusoidal para crear el movimiento oscilante
        float newY = startPos.y + Mathf.Sin(Time.time * speed) * floatStrength;
        transform.position = new Vector3(transform.position.x, newY, transform.position.z);
    }
}
