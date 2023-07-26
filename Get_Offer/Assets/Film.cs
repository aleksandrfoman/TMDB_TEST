using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;

public class Film : MonoBehaviour
{
    [SerializeField]
    private RawImage image;
    [SerializeField]
    private TMP_Text title, overwiew;
    public void Init(string img,string title, string overview)
    {
        StartCoroutine(DownloadImage(img));
        this.title.text = title;
        this.overwiew.text = overview;
    }

    private IEnumerator DownloadImage(string img)
    {
        UnityWebRequest www = UnityWebRequestTexture.GetTexture("http://image.tmdb.org/t/p/w500"+img);
        yield return www.SendWebRequest();

        if (www.result != UnityWebRequest.Result.Success)
        {
            Debug.Log(www.error);
        }
        else
        {
            Texture myTexture = ((DownloadHandlerTexture)www.downloadHandler).texture;
            image.texture = myTexture;
        }
    }
}
