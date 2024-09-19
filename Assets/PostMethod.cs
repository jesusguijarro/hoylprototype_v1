
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;
using TMPro;
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
        //bool errorPanel = false;
        //outputArea.enabled = false;
        //outputArea.text = "Loading...";

        //TMP_InputField inputField = outputArea.GetComponent<TMP_InputField>();
        //inputField.text = "Loading...";

        TextMeshProUGUI statusText = statusPanel.GetComponentInChildren<TextMeshProUGUI>();
        statusText.text = "Cargando...";

        string uri = "http://localhost:8083/graphql";

        string name = nameInputField.text;
        string ageStr = ageInputField.text;
        int age = int.Parse(ageInputField.text);
        string username = userInputField.text;
        string appearance = AppearanceSelector.instance.selectedAppearance;
        Appearance appearanceEnumValue;

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

        if (string.IsNullOrEmpty(name) || string.IsNullOrEmpty(username) || string.IsNullOrEmpty(ageStr) || string.IsNullOrEmpty(appearance))
        {
            //outputArea.SetActive(true);
            statusPanel.SetActive(true);
            //outputArea.text = "Error: Please fill in all fields.";
            //inputField.text = "Error: Please fill in all fields.";
            statusText.text = "Error: Por favor rellene todos los campos.";
            yield break;
        }

        // Name, username and age works
        // string mutation = "mutation { registerPlayer(create: { name: \"" + name + "\", age: " + age + ", username: \"" + username + "\", appearance: MALE }) { id name age username appearance } }";

        string mutation = "mutation { registerPlayer(create: { name: \"" + name + "\", age: " + age + ", username: \"" + username + "\", appearance: " + appearanceEnumValue + " }) { id name age username appearance } }";

        using (UnityWebRequest request = new UnityWebRequest(uri, "POST"))
        {
            Query queryObj = new Query { query = mutation };
            string json = JsonUtility.ToJson(queryObj);
            Debug.Log("JSON Payload: " + json);

            byte[] bodyRaw = System.Text.Encoding.UTF8.GetBytes(json);
            request.uploadHandler = new UploadHandlerRaw(bodyRaw);
            request.uploadHandler.contentType = "application/json; charset=utf-8";
            request.downloadHandler = new DownloadHandlerBuffer();

            yield return request.SendWebRequest();
            if (request.result == UnityWebRequest.Result.ConnectionError || request.result == UnityWebRequest.Result.ProtocolError)
            {
                //outputArea.SetActive(true);
                statusPanel.SetActive(true);
                Debug.LogError(request.error);
            }
            else
            {
                Debug.Log("Request: " + request.url);
                Debug.Log("Request Headers: " + request.GetRequestHeader("Content-Type"));
                Debug.Log("Request Body: " + json);
                Debug.Log("Response: " + request.downloadHandler.text);
                //outputArea.SetActive(false);
                statusPanel.SetActive(false);
                //inputField.text = request.downloadHandler.text;
                //outputArea.text = request.downloadHandler.text;
            }
        }
    }
}
