using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class start_startManager : MonoBehaviour
{
    public Image button, UIbackground;
    public Image logo;
    public Sprite[] logos;
    public GameObject startText;

    float time;

    void Start()
    {
        UIbackground.gameObject.SetActive(false);

        //button.gameObject.SetActive(false);
        //logo.gameObject.SetActive(false);
        //startText.gameObject.SetActive(false);
    }

    public void Update()
    {
        // 문구 깜빡깜빡
        if (time < 0.5f)
            startText.GetComponent<Text>().color = new Color(1, 1, 1, 1 - time);
        else
        {
            startText.GetComponent<Text>().color = new Color(1, 1, 1, time);
            if (time > 1f) time = 0;
        }
        time += Time.deltaTime;

        // 카드 회전
        logo.transform.Rotate(Vector3.up * 100 * Time.deltaTime);
        if (logo.transform.eulerAngles.y > 90 && logo.transform.eulerAngles.y < 270)
            logo.sprite = logos[1];
        else
            logo.sprite = logos[0];
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
