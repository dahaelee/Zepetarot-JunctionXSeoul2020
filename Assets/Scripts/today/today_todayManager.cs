using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class today_todayManager : MonoBehaviour
{
    public UI_manager UImanager;
    public Text hashcode;
    public Image UIbackground;
    public GameObject card;

    // card related
    string pickedCard;

    void Awake()
    {
        /*
        // Make More Cards
        for (int i = 0; i < 10; i++)
        {
            //GameObject childObject = Instantiate(card, new Vector3(i * 10.0F, 0, 0), Quaternion.identity) as GameObject;
            GameObject childObject = (GameObject)Instantiate(card);
            childObject.transform.parent = card.transform;
            childObject.GetComponent<RectTransform>().localPosition += Vector3.right * 5.0f;
        }
        */
    }

    void Start()
    {
        UIbackground.gameObject.SetActive(false);

        UImanager.allUIoff();
        //hashcode.GetComponent<Text>().text = user.hashcode.ToString();
        hashcode.GetComponent<Text>().text = "JJJJJJ";
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

    public bool checkDoubleClick(string tempCard)
    {
        if(pickedCard == tempCard)
        {
            // Tell the card to be picked up
            return true;
        }
        else
        {
            if (pickedCard != null)
            {
                // put the previous card back into it's place
                GameObject temp = GameObject.Find(pickedCard);
                temp.GetComponent<RectTransform>().localPosition -= Vector3.down * 150.0f;
            }

            pickedCard = tempCard;
            Debug.Log("Diff");
            return false;
        }
    }

    public void renewCardDeck()
    {
        if (pickedCard != null)
        {
            // put the previous card back into it's place
            GameObject temp = GameObject.Find(pickedCard);
            temp.GetComponent<RectTransform>().localPosition -= Vector3.down * 150.0f;

            pickedCard = null;
        }
    }
}
