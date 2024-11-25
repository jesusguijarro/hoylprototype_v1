using UnityEngine;
using UnityEngine.UI;

public class LoadingTextAnimator : MonoBehaviour
{
    private Text loadingText; // Referencia al componente de texto.
    public float animationSpeed = 0.5f; // Tiempo entre cada cambio de texto (en segundos).

    private string baseText = "Cargando"; // Texto base.
    private int dotCount = 0; // N�mero actual de puntos suspensivos.
    private float timer = 0f; // Temporizador para controlar la animaci�n.

    void Start()
    {
        // Buscar el componente Text autom�ticamente en los hijos.
        loadingText = GetComponentInChildren<Text>();

        if (loadingText == null)
        {
            Debug.LogError("No se encontr� un componente Text en los hijos de este GameObject.");
        }
    }

    void Update()
    {
        if (loadingText == null) return;

        // Incrementar el temporizador.
        timer += Time.deltaTime;

        // Si se alcanza el tiempo especificado, actualizar el texto.
        if (timer >= animationSpeed)
        {
            timer = 0f; // Reiniciar el temporizador.

            // Incrementar el n�mero de puntos suspensivos.
            dotCount = (dotCount + 1) % 4; // Ciclo entre 0, 1, 2, 3.

            // Actualizar el texto con los puntos suspensivos.
            loadingText.text = baseText + new string('.', dotCount);
        }
    }
}
