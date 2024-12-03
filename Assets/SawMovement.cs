using UnityEngine;

public class SawMovement : MonoBehaviour
{
    // Configuraci�n para el movimiento de traslaci�n
    public float movementSpeed = 2f; // Velocidad del movimiento
    public float movementRange = 5f; // Distancia m�xima del movimiento

    // Configuraci�n para la rotaci�n
    public float rotationSpeed = 360f; // Grados por segundo

    private Vector3 startPosition; // Posici�n inicial de la sierra
    private bool movingForward = true; // Direcci�n del movimiento

    void Start()
    {
        // Guardar la posici�n inicial
        startPosition = transform.position;
    }

    void Update()
    {
        // Movimiento de traslaci�n
        float movementStep = movementSpeed * Time.deltaTime;
        if (movingForward)
        {
            transform.position += transform.right * movementStep; // Mover hacia adelante
            if (Vector3.Distance(transform.position, startPosition) >= movementRange)
                movingForward = false; // Cambiar direcci�n al alcanzar el rango
        }
        else
        {
            transform.position -= transform.right * movementStep; // Mover hacia atr�s
            if (Vector3.Distance(transform.position, startPosition) <= 0.1f)
                movingForward = true; // Cambiar direcci�n al regresar al inicio
        }

        // Rotaci�n continua
        transform.Rotate(Vector3.forward, rotationSpeed * Time.deltaTime);
    }
}
