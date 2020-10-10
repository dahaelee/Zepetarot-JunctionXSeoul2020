using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class start_startManager : MonoBehaviour
{
    public Image button, UIbackground;

    void Start()
    {
        UIbackground.gameObject.SetActive(false);
    }

    public void nextScene()
    {
        UIbackground.gameObject.SetActive(true);

        int isNew = PlayerPrefs.GetInt("isNew", 1);
        if (isNew == 0) // 재접속이면 바로 main 씬
        {
            StartCoroutine("fadeoutAndMain");
        }
        else // 첫 접속이면 login 씬 
        {
            PlayerPrefs.DeleteAll();
            StartCoroutine("fadeoutAndLogin");
        }
    }

    public IEnumerator fadeoutAndMain()
    {
        float fade_time = 0.5f, start = 0.5f, end = 0.7f, time = 0f;
        Color fade_color = UIbackground.color;

        while (fade_color.a < end)
        {
            time += Time.deltaTime / fade_time;
            fade_color.a = Mathf.Lerp(start, end, time);
            UIbackground.color = fade_color;
            yield return null;
        }

        SceneManager.LoadScene("main");
    }

    public IEnumerator fadeoutAndLogin()
    {
        float fade_time = 0.5f, start = 0.5f, end = 0.7f, time = 0f;
        Color fade_color = UIbackground.color;

        while (fade_color.a < end)
        {
            time += Time.deltaTime / fade_time;
            fade_color.a = Mathf.Lerp(start, end, time);
            UIbackground.color = fade_color;
            yield return null;
        }

        SceneManager.LoadScene("login");
    }
}
