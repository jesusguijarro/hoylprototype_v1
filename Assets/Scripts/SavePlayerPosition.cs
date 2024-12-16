using UnityEngine;

public class SavePlayerPosition : MonoBehaviour
{
    private Player player;
    private int positionIndex = 0; // �ndice para el seguimiento de las posiciones
    private float saveInterval = 5f; // Intervalo de tiempo para guardar la posici�n
    private float lastSaveTime = 0f;

    void Start()
    {
        player = FindAnyObjectByType<Player>(); // Aseg�rate de que el jugador est� correctamente asignado

        if (PlayerPrefs.HasKey("playerStarted"))
        {
            LoadPlayerPosition(); // Cargar la �ltima posici�n guardada
        }

        if (!PlayerPrefs.HasKey("playerStarted"))
        {
            PlayerPrefs.SetInt("playerStarted", 1);
            PlayerPrefs.Save();
        }
    }

    void Update()
    {
        // Guardar la posici�n cada cierto tiempo
        if (Time.time - lastSaveTime >= saveInterval)
        {
            SavePosition();
            lastSaveTime = Time.time;
        }
    }

    public void SavePosition()
    {
        Vector3 playerPosition = player.transform.position;

        // Guardar la posici�n con un �ndice incremental
        PlayerPrefs.SetFloat("playerPosition_" + positionIndex + "_X", playerPosition.x);
        PlayerPrefs.SetFloat("playerPosition_" + positionIndex + "_Y", playerPosition.y);
        PlayerPrefs.SetFloat("playerPosition_" + positionIndex + "_Z", playerPosition.z);
        PlayerPrefs.Save();

        // Debug.Log("Player position saved at index: " + positionIndex);

        // Incrementar el �ndice para la pr�xima posici�n guardada
        positionIndex++;
    }

    public void ResetPlayerPosition()
    {
        PlayerPrefs.DeleteAll();
        PlayerPrefs.Save();
        Debug.Log("PlayerPrefs reset!");
    }

    public void LoadPlayerPosition()
    {
        player.gameObject.SetActive(false);

        // Buscar la pen�ltima posici�n guardada
        int targetIndex = positionIndex - 2; // �ndice de la pen�ltima posici�n guardada

        if (targetIndex >= 0) // Aseg�rate de que el �ndice es v�lido
        {
            Vector3 savedPosition = new Vector3(
                PlayerPrefs.GetFloat("playerPosition_" + targetIndex + "_X"),
                PlayerPrefs.GetFloat("playerPosition_" + targetIndex + "_Y"),
                PlayerPrefs.GetFloat("playerPosition_" + targetIndex + "_Z")
            );
            player.transform.position = savedPosition;
            Debug.Log("Player position loaded at index: " + targetIndex);
        }
        else
        {
            Debug.LogError("No saved position found for the specified index.");
        }

        player.gameObject.SetActive(true);
    }
}
