using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class GET : MonoBehaviour
{
    private string bearer = "Bearer eyJhbGciOiJIUzI1NiJ9.eyJhdWQiOiIwMDI0MzMzNmUyZjk0OWVkYmEwNWZjNjU1ZGE0NTEwZSIsInN1YiI6IjVhYzFjM2IxMGUwYTI2NGE1NzA1NmEwMSIsInNjb3BlcyI6WyJhcGlfcmVhZCJdLCJ2ZXJzaW9uIjoxfQ.uy3Lj5gCGGhxulu3ocPzJVh10f7KE_x1IDSE16CGzKw";
    private string url = "https://api.themoviedb.org/3/discover/movie?include_adult=false&include_video=false&language=en-US&page=1&sort_by=popularity.desc";
    [SerializeField]
    private Data data;
    [SerializeField]
    private Film filmPrefab;
    [SerializeField]
    private Transform parentFilm;

    private void Start()
    {
        StartCoroutine(Req());
    }
    private IEnumerator WebReqUnity(string url)
    {
        UnityWebRequest www = UnityWebRequest.Get(url);
        www.SetRequestHeader("accept", "application/json");
        www.SetRequestHeader("Authorization", bearer);

        yield return www.SendWebRequest();

        if (string.IsNullOrEmpty(www.error))
        {
            Debug.Log(www.downloadHandler.text);
            data = JsonUtility.FromJson<Data>(www.downloadHandler.text);
        }
        else
        {
            Debug.LogError(www.error);
        }
    }
    private IEnumerator Req()
    {
        yield return WebReqUnity(url);
        for (int i = 0; i < data.results.Length; i++)
        {
            Instantiate(filmPrefab, parentFilm).Init(data.results[i].backdrop_path, data.results[i].title, data.results[i].overview);
        }
    }


}
