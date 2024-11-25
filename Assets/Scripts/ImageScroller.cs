using UnityEngine;

public class ImageScroller : MonoBehaviour
{
    public RectTransform[] images; // Las im�genes que se mover�n.
    public float scrollSpeed = 100f; // Velocidad de desplazamiento.
    public float progressBarWidth = 1500f; // Ancho de la barra de progreso.

    private float imageWidth; // Ancho de cada imagen (se calcula autom�ticamente).

    void Start()
    {
        if (images.Length == 0)
        {
            Debug.LogError("No hay im�genes asignadas al script.");
            return;
        }

        // Calcular el ancho de una imagen (usando el RectTransform del primer elemento).
        imageWidth = images[0].rect.width;

        // Posicionar las im�genes desde la izquierda de la barra.
        for (int i = 0; i < images.Length; i++)
        {
            float xOffset = i * imageWidth; // Desplazamiento horizontal de cada imagen.
            images[i].anchoredPosition = new Vector2(-progressBarWidth / 2 + xOffset, images[i].anchoredPosition.y);
        }
    }

    void Update()
    {
        if (images.Length == 0) return;

        // Desplazar cada imagen.
        for (int i = 0; i < images.Length; i++)
        {
            images[i].anchoredPosition += Vector2.right * scrollSpeed * Time.deltaTime;

            // Si la imagen sale completamente del �rea visible (derecha), moverla directamente al inicio.
            if (images[i].anchoredPosition.x > progressBarWidth / 2)
            {
                // Reubicar en el extremo izquierdo del �rea visible.
                images[i].anchoredPosition = new Vector2(-progressBarWidth / 2 - imageWidth, images[i].anchoredPosition.y);
            }
        }
    }
}
