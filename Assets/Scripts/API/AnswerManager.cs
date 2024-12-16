using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class AnswerManager : MonoBehaviour
{
    public static AnswerManager Instance { get; private set; }
    private void Awake()
    {
        // Ensure there's only one instance
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject); // Persist across scenes if necessary
        }
        else
        {
            Destroy(gameObject);
        }        
    }
    public class Query
    {
        public string query;
    }
    public void SendAnswerToServer(int questionNumber, int answerValue, string username)
    {
        StartCoroutine(SendAnswerCoroutine(questionNumber, answerValue, username));
    }

    private IEnumerator SendAnswerCoroutine(int questionNumber, int answerValue, string username)
    {
        string uri = "http://192.168.0.101:8083/graphql";
        string mutation = "mutation { saveAnswer(create: { question: " + questionNumber + ", answer: " + answerValue + ", playerUsername: \"" + username + "\" }) {id question answer playerUsername} }";

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
                Debug.LogError("Error: " + request.error);
            }
            else
            {
                Debug.Log("Request: " + request.url);
                Debug.Log("Request Headers: " + request.GetRequestHeader("Content-Type"));
                Debug.Log("Request Body: " + json);
                Debug.Log("Response: " + request.downloadHandler.text);
            }
        }
    }
}
