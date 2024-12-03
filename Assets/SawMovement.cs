using UnityEngine;

public class SawMovement : MonoBehaviour
{
    // Configuración para el movimiento de traslación
    public float movementSpeed = 2f; // Velocidad del movimiento
    public float movementRange = 5f; // Distancia máxima del movimiento

    // Configuración para la rotación
    public float rotationSpeed = 360f; // Grados por segundo

    private Vector3 startPosition; // Posición inicial de la sierra
    private bool movingForward = true; // Dirección del movimiento

    void Start()
    {
        // Guardar la posición inicial
        startPosition = transform.position;
    }

    void Update()
    {
        // Movimiento de traslación
        float movementStep = movementSpeed * Time.deltaTime;
        if (movingForward)
        {
            transform.position += transform.right * movementStep; // Mover hacia adelante
            if (Vector3.Distance(transform.position, startPosition) >= movementRange)
                movingForward = false; // Cambiar dirección al alcanzar el rango
        }
        else
        {
            transform.position -= transform.right * movementStep; // Mover hacia atrás
            if (Vector3.Distance(transform.position, startPosition) <= 0.1f)
                movingForward = true; // Cambiar dirección al regresar al inicio
        }

        // Rotación continua
        transform.Rotate(Vector3.forward, rotationSpeed * Time.deltaTime);
    }
}
