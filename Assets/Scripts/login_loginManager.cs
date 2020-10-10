using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class login_loginManager : MonoBehaviour
{
    public InputField input;
    public Image UIbackground;

    void Start()
    {
        UIbackground.gameObject.SetActive(false);

        // 알파벳 입력 시 대문자로만 나오도록
        input.onValidateInput += delegate (string text, int charIndex, char addedChar) {
            return changeUpperCase(addedChar);
        };
    }

    void Update()
    {
        
    }

    char changeUpperCase(char _cha)
    {
        char tmpChar = _cha;
        string tmpString = tmpChar.ToString();
        tmpString = tmpString.ToUpper();
        tmpChar = System.Convert.ToChar(tmpString);
        return tmpChar;
    }

    public void nextScene()
    {
        UIbackground.gameObject.SetActive(true);

        /*
         * 입력받은 해쉬코드로 api를 호출하고 존재하지 않는 해쉬코드일 시 팝업 띄우기
         * 존재하면 dataSave하고 loadScene 
         */

        dataSave();
        StartCoroutine("fadeoutAndMain");
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

    public void dataSave()
    {
        PlayerPrefs.SetInt("isNew", 0);

        user.hashcode = input.text;
        PlayerPrefs.SetString("hashcode", user.hashcode);
        
        PlayerPrefs.Save();
    }
}
