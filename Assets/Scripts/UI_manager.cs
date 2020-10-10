using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UI_manager : MonoBehaviour
{
    public Image[] UIs;
    public static UIstate currentState = UIstate.None;

    public enum UIstate
    {
        UIbackground,
        setting,
        None
    }

    public void allUIoff()
    {
        for (int i = 0; i < UIs.Length; i++)
            UIs[i].gameObject.SetActive(false);

        currentState = UIstate.None;
    }

    public IEnumerator UIon(UIstate uistate)
    {
        currentState = uistate;
        UIs[(int)UIstate.UIbackground].gameObject.SetActive(true);
        UIs[(int)uistate].gameObject.SetActive(true);
        for (int i = 0; i < 5; i++)
        {
            UIs[(int)uistate].rectTransform.localScale = new Vector3((float)(0.95 + i * 0.01), (float)(0.95 + i * 0.01), (float)(0.95 + i * 0.01));
            yield return 0;
        }
    }

    public void UIoff(UIstate index)
    {
        UIs[(int)index].gameObject.SetActive(false);
        currentState = UIstate.None;
    }
}
