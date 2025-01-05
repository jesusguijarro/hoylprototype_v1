
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;
using TMPro;
using UnityEngine.SceneManagement;
public class PostMethod : MonoBehaviour
{
    //public GameObject outputArea;
    public GameObject statusPanel;

    [Header("Register Inputs")]
    public TMP_InputField nameInputField;
    public TMP_InputField ageInputField;
    public TMP_InputField userInputField;
    
    private void Start()
    {        
        GameObject.Find("ContinueButton").GetComponent<Button>().onClick.AddListener(PostData);
    }

    void PostData() => StartCoroutine(PostDataCoroutine());

    [System.Serializable]
    public class Query
    {
        public string query;
    }

    public enum Appearance
    {
        MALE,
        FEMALE,
        OTHER
    }

    IEnumerator PostDataCoroutine()
    {
        TextMeshProUGUI statusText = statusPanel.GetComponentInChildren<TextMeshProUGUI>();        

        string uri = "http://192.168.0.101:8083/graphql"; //http://10.1.142.159:8083/graphql
        // string uri = "http://10.1.142.159:8083/graphql";

        string name = nameInputField.text;
        string ageStr = ageInputField.text;
        //int age = int.Parse(ageInputField.text);
        string username = userInputField.text;
        string appearance = AppearanceSelector.instance.selectedAppearance;
        Appearance appearanceEnumValue;

        if (string.IsNullOrEmpty(name) || string.IsNullOrEmpty(username) || string.IsNullOrEmpty(ageStr) || string.IsNullOrEmpty(appearance))
        {
            statusPanel.SetActive(true);
            statusText.text = "Error: Por favor rellene todos los campos.";
            yield break;
        }

        // Validate username length
        if (username.Length > 12)
        {
            statusPanel.SetActive(true);
            statusText.text = "Error: Ingrese un username más corto (11 caracteres máximo).";
            yield break;
        }

        // Validate age
        int age;
        if (!int.TryParse(ageStr, out age) || age < 7 || age > 9)
        {
            statusPanel.SetActive(true);
            statusText.text = "Error: Ingrese una edad válida (7-9).";
            yield break;
        }

        switch (appearance)
        {
            case "MALE":
                appearanceEnumValue = Appearance.MALE;
                break;
            case "FEMALE":
                appearanceEnumValue = Appearance.FEMALE;
                break;
            default:
                appearanceEnumValue = Appearance.OTHER;
                break;
        }

        string mutation = "mutation { registerPlayer(create: { name: \"" + name + "\", age: " + age + ", username: \"" + username + "\", appearance: " + appearanceEnumValue + " }) { id name age username appearance } }";

        using (UnityWebRequest request = new UnityWebRequest(uri, "POST"))
        {
            Query queryObj = new Query { query = mutation };
            string json = JsonUtility.ToJson(queryObj);
            Debug.Log("JSON Payload: " + json);

            byte[] bodyRaw = System.Text.Encoding.UTF8.GetBytes(json);
            //request.certificateHandler = new BypassCertificate();
            request.uploadHandler = new UploadHandlerRaw(bodyRaw);
            request.uploadHandler.contentType = "application/json; charset=utf-8";
            request.downloadHandler = new DownloadHandlerBuffer();

            yield return request.SendWebRequest();
            if (request.result == UnityWebRequest.Result.ConnectionError || request.result == UnityWebRequest.Result.ProtocolError)
            {                
                statusPanel.SetActive(true);
                Debug.LogError(request.error);
            }
            else
            {
                statusPanel.SetActive(true);
                statusText.text = "Cargando...";
                Debug.Log("Request: " + request.url);
                Debug.Log("Request Headers: " + request.GetRequestHeader("Content-Type"));
                Debug.Log("Request Body: " + json);
                Debug.Log("Response: " + request.downloadHandler.text);                
                statusPanel.SetActive(false);
                PlayerPrefs.SetString("PlayerUsername",username);
                PlayerPrefs.SetString("PlayerAppearance", appearanceEnumValue.ToString());
                SceneManager.LoadScene("Tutorial");
            }
        }
    }
}
