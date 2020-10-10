using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;

using System;
using System.Text;
using System.Net;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json;

public class api : MonoBehaviour
{
    public WWW result;
    public String imgUrl;
    public String boothNum, hashcode;
    public RawImage testImage;

    void Start()
    {
        result = Post("PHOTOBOOTH_ONE_163");
    }

    public WWW Post(String boothNum)
    {
        WWW www;

        string url = "https://render-api.zepeto.io/v2/graphics/zepeto/booth/" + boothNum + "?permanent=false";
        Hashtable postHeader = new Hashtable();
        postHeader.Add("authority", "render-api.zepeto.io");
        postHeader.Add("Accept", "application/json");
        postHeader.Add("Content-Type", "application/json");

        String jsonStr = "{\"type\":\"booth\",\"width\":1024,\"target\":{\"hashCodes\":[\""+ user.hashcode +"\"]}}";

        var formData = System.Text.Encoding.UTF8.GetBytes(jsonStr);

        www = new WWW(url, formData, postHeader);
        StartCoroutine(WaitForRequest(www));
        return www;
    }

    IEnumerator WaitForRequest(WWW data)
    {
        yield return data;
        JObject jobj = JObject.Parse(result.text);
        imgUrl = jobj["url"].ToString();
        StartCoroutine("CoLoadImageTexture");
    }

    IEnumerator CoLoadImageTexture()
    {
        UnityWebRequest www = UnityWebRequestTexture.GetTexture(imgUrl);
        yield return www.SendWebRequest();

        if (www.isNetworkError || www.isHttpError)
            Debug.Log(www.error);
        else
            testImage.texture = ((DownloadHandlerTexture)www.downloadHandler).texture;
    }
}
