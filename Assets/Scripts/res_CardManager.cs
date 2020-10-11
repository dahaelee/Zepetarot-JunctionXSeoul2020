using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class res_CardManager : MonoBehaviour
{
    public int cardNum;

    public Image card_bgrd;
    public Sprite[] bgrd_list;
    public Image UIbackground;
    public UI_manager UImanager;
    public GameObject API;
    public Text hashcode;
    public Text head, script;

    float time = 0;
    float fade = 0.0f;

    private void Awake()
    {
        dataLoad();
    }

    // Start is called before the first frame update
    void Start()
    {
        UIbackground.gameObject.SetActive(false);
        UImanager.allUIoff();
        hashcode.GetComponent<Text>().text = user.hashcode.ToString();


        List<Dictionary<string,object>> data = CSVReader.Read ("cardData");

        // data[cardNum]["cardNUM"] : 0~4
        // data[cardNum]["cardName"] : 카드이름
        // data[cardNum]["boothName"] : 부스이름
        // data[cardNum]["script"] : 설명



        //랜덤으로 카드종류 정하고 배경 출력
        cardNum = Random.Range(0,5);
        card_bgrd.sprite = bgrd_list[cardNum];

        //Debug.Log((string) data[cardNum]["boothName"]);

        //API.GetComponent<api>().Post((string) data[cardNum]["boothName"]);

        // 설명 출력
        head.text = data[cardNum]["cardName"] + " 카드";
        script.text = (string)data[cardNum]["script"];
        
        //Debug.Log(data[cardNum]["boothName"]);

    }

    public void loadMain()
    {
        StartCoroutine("fadeoutAndMain");
    }

    public void setting()
    {
        StartCoroutine(UImanager.UIon(UI_manager.UIstate.setting));
    }

    public void logout()
    {
        PlayerPrefs.DeleteAll();
        UImanager.UIoff(UI_manager.UIstate.setting);
        StartCoroutine("fadeoutAndLogin");
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

    public void dataLoad()
    {
        user.hashcode = PlayerPrefs.GetString("hashcode", user.hashcode);
    }
}
