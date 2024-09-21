using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;
using TMPro;

public class GetMethod : MonoBehaviour
{
    public TMP_InputField outputArea;
   
    void Start()
    {        
        //GameObject.Find("GetButton").GetComponent<Button>().onClick.AddListener(GetData);
    }
    
    void GetData() => StartCoroutine(GetDataCoroutine());

    IEnumerator GetDataCoroutine()
    {        
        outputArea.text = "Loading...";
        string uri = "http://localhost:8083/graphql";
        string query = "query { players { id name age username appearance } }";

        using (UnityWebRequest request = new UnityWebRequest(uri, "POST"))
        {
            string json = $"{{ \"query\": \"{query}\" }}";
            Debug.Log("JSON Payload: " + json); // just a log
            byte[] bodyRaw = System.Text.Encoding.UTF8.GetBytes(json);
            request.uploadHandler = new UploadHandlerRaw(bodyRaw);
            request.uploadHandler.contentType = "application/json; charset=utf-8"; // new line
            request.downloadHandler = new DownloadHandlerBuffer();

            yield return request.SendWebRequest();
            if (request.result == UnityWebRequest.Result.ConnectionError || request.result == UnityWebRequest.Result.ProtocolError)
                outputArea.text = request.error;
            else
                outputArea.text = request.downloadHandler.text;
        }
    }
}


