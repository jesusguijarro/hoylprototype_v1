using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine.UI;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TutorialManager : MonoBehaviour
{

    [Header("Fields")]
    public TextMeshProUGUI title;
    public TextMeshProUGUI description;
    public Image image;
    public Button continueBtn;
    public Button backBtn;
    public Button playBtn;
    public Image progressbar;

    [Header("Tutorial Content")]
    private string[] titles; // Array of title texts
    private string[] descriptions; // Array of description texts
    private string[] imageName; // Array of description texts

    private int progress;
    void Start()
    {
        titles = new string[]
        {
        "Bienvenido al Tutorial",
        "Controles de movimiento",
        "Pausa e Inventario",
        "Interacci�n con personajes",
        "Signo de admiraci�n",
        "Portales"
        };

        descriptions = new string[]
        {
        "Presiona los botones de navegaci�n de debajo para moverte por el tutorial",
        "Utiliza el joystick para moverte por el mundo, adem�s puedes saltar y pegar",
        "Puedes equipar un arma accediendo al inventario y desequiparla haciendo clic en la espada arriba a la izquierda. Visita el men� de pausa para Guardar, Configurar (volumen) o Salir del juego",
        "Encontrar�s personajes al rededor del mundo, interactua con ellos dando clic ya sea en ellos o en el signo de admiraci�n que los acompa�a, !te guiar�n hac�a la aventura!",
        "Una vez que el signo de admiraci�n desaparezca, significa que los personajes ya no tienen nada m�s por contarte",
        "Terminada la interacci�n, los portales ser�n habilitados �elige el camino que quieras recorrer!"
        };

        imageName = new string[]
        {
            "child_happy",
            "navigation",
            "navigation",
            "darklord_happy",
            "admiration_sign",
            "portal"
        };

        progress = 0;

        UpdateContent(); // Set initial content

        playBtn.enabled = false;

        continueBtn.onClick.AddListener(OnContinueClicked);
        backBtn.onClick.AddListener(OnBackClicked);
        playBtn.onClick.AddListener(MoveToGame);
    }

    void UpdateContent()
    {
        // Enable or disable the Back button based on progress
        backBtn.interactable = progress > 0;

        // Enable or disable the Continue button based on progress
        continueBtn.interactable = progress < titles.Length - 1;

        // Update the title and description text
        if (progress < titles.Length && progress < descriptions.Length)
        {
            title.text = titles[progress];
            description.text = descriptions[progress];
            image.sprite = Resources.Load<Sprite>("UI/Icons/TutorialUsage/"+imageName[progress].ToString());
            UpdateProgressBar(); // Update progress bar
        }
        if (progress.Equals(titles.Length - 1))
        {
            Debug.Log("Tutorial ended");
            playBtn.enabled = true;
        }
    }
    void OnContinueClicked()
    {
        progress++;
        UpdateContent();
    }

    void OnBackClicked()
    {
        if (progress > 0)
        {
            progress--;
            UpdateContent();
        }
    }

    void UpdateProgressBar()
    {
        if (progressbar != null)
        {
            float progressPercentage = (float)progress / (titles.Length - 1);
            progressbar.fillAmount = progressPercentage;
        }
    }

    void MoveToGame()
    {
        SceneManager.LoadScene("SampleScene");
        PlayerPrefs.SetInt("PassToGame", 1);
    }
}
