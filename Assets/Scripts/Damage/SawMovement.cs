using UnityEngine;

public class SawMovement : MonoBehaviour
{
    public float movementSpeed = 2f; // Velocidad de traslación
    public float rotationSpeed = 360f; // Velocidad de rotación
    public Transform startPoint; // Punto de inicio
    public Transform endPoint; // Punto final

    private Vector3 startPosition; // Posición inicial
    private Vector3 endPosition; // Posición final
    private bool movingForward = true; // Dirección de movimiento

    void Start()
    {
        startPosition = startPoint.position;
        endPosition = endPoint.position;
        transform.position = startPosition; // Posicionar la sierra al inicio
    }

    void Update()
    {
        // Movimiento de traslación
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

        // Rotación continua en su propio eje
        transform.Rotate(Vector3.forward, rotationSpeed * Time.deltaTime, Space.Self);
    }

    private void OnTriggerEnter(Collider other)
    {
        // Verificar si el objeto que toca la sierra tiene el componente de vida
        Player player = other.GetComponent<Player>();
        if (player != null)
        {
            player.TakeDamage(15); // Quitar 15 de vida al jugador
            Debug.Log($"{player.name} tocó la sierra y perdió 10 de vida.");
        }
    }
}
