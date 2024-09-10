using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;
using TMPro;
public class PostMethod : MonoBehaviour
{
    public TMP_InputField outputArea;

    private void Start()
    {
        GameObject.Find("PostButton").GetComponent<Button>().onClick.AddListener(PostData);
    }

    void PostData() => StartCoroutine(PostDataCoroutine());

    [System.Serializable]
    public class Query
    {
        public string query;
    }

    IEnumerator PostDataCoroutine()
    {
        outputArea.text = "Loading...";
        string uri = "http://localhost:8083/graphql";
        string mutation = "mutation { registerPlayer(create: { name: \"Jesus\", age: 21, username: \"jesusguijarro\", appearance: MALE }) { id name age username appearance } }";
                
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
                outputArea.text = request.error;
            else
                outputArea.text = request.downloadHandler.text;
        }
    }
}
