using System.Collections;
using UnityEngine;
using TMPro;

public class TypeWriterEffect : MonoBehaviour
{
    public TextMeshProUGUI textComponent; // El componente de texto
    public float typingSpeed = 0.05f;     // Velocidad de escritura

    private string fullText;             // Texto completo
    private string currentText = "";     // Texto que se mostrará progresivamente

    private void Start()
    {
        // Guardar el texto completo y vaciar el contenido mostrado
        fullText = textComponent.text;
        textComponent.text = "";

        // Iniciar la corutina de escritura
        StartCoroutine(TypeText());
    }

    private IEnumerator TypeText()
    {
        foreach (char letter in fullText.ToCharArray())
        {
            currentText += letter;
            textComponent.text = currentText;
            yield return new WaitForSeconds(typingSpeed); // Espera entre cada letra
        }
    }
}
