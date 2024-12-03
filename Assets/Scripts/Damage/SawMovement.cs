using UnityEngine;

public class SawMovement : MonoBehaviour
{
    public float movementSpeed = 2f; // Velocidad de traslaci�n
    public float rotationSpeed = 360f; // Velocidad de rotaci�n
    public Transform startPoint; // Punto de inicio
    public Transform endPoint; // Punto final

    private Vector3 startPosition; // Posici�n inicial
    private Vector3 endPosition; // Posici�n final
    private bool movingForward = true; // Direcci�n de movimiento

    void Start()
    {
        startPosition = startPoint.position;
        endPosition = endPoint.position;
        transform.position = startPosition; // Posicionar la sierra al inicio
    }

    void Update()
    {
        // Movimiento de traslaci�n
        float movementStep = movementSpeed * Time.deltaTime;
        if (movingForward)
        {
            transform.position = Vector3.MoveTowards(transform.position, endPosition, movementStep);
            if (Vector3.Distance(transform.position, endPosition) <= 0.1f)
                movingForward = false;
        }
        else
        {
            transform.position = Vector3.MoveTowards(transform.position, startPosition, movementStep);
            if (Vector3.Distance(transform.position, startPosition) <= 0.1f)
                movingForward = true;
        }

        // Rotaci�n continua en su propio eje
        transform.Rotate(Vector3.forward, rotationSpeed * Time.deltaTime, Space.Self);
    }

    private void OnTriggerEnter(Collider other)
    {
        // Verificar si el objeto que toca la sierra tiene el componente de vida
        Player player = other.GetComponent<Player>();
        if (player != null)
        {
            player.TakeDamage(15); // Quitar 15 de vida al jugador
            Debug.Log($"{player.name} toc� la sierra y perdi� 10 de vida.");
        }
    }
}
