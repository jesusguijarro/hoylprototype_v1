
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
        statusText.text = "Cargando...";

        string uri = "http://192.168.1.76:8083/graphql"; //http://10.1.142.159:8083/graphql
        // string uri = "http://10.1.142.159:8083/graphql";

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
            statusPanel.SetActive(true);
            statusText.text = "Error: Por favor rellene todos los campos.";
            yield break;
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
                Debug.Log("Request: " + request.url);
                Debug.Log("Request Headers: " + request.GetRequestHeader("Content-Type"));
                Debug.Log("Request Body: " + json);
                Debug.Log("Response: " + request.downloadHandler.text);                
                statusPanel.SetActive(false);
                PlayerPrefs.SetString("PlayerUsername",username);
                PlayerPrefs.SetString("PlayerAppearance", appearanceEnumValue.ToString());
                SceneManager.LoadScene("SampleScene");
            }
        }
    }
}
