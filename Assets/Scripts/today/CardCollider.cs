using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CardCollider : MonoBehaviour
{
    public GameObject totManager;
    public GameObject darkBG;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    public void OnMouseUpAsButton()
    {
        this.GetComponent<RectTransform>().localPosition += Vector3.down * 150.0f;
        bool doubleClicked = totManager.GetComponent<today_todayManager>().checkDoubleClick(this.gameObject.name);

        if (doubleClicked) 
        { 
            Vector3 targetPoint = new Vector3(360, 600, 0);
            this.gameObject.transform.parent = GameObject.Find("Canvas").transform;
            StartCoroutine(CardSizeUp(this.transform, transform.position, targetPoint, 0.5f));
        }

    }

    IEnumerator CardSizeUp(Transform thisTransform, Vector3 startPos, Vector3 endPos, float time)
    {
        float i = 0.0f;
        float rate = 1.0f / time;
        while (i < 1.0)
        {
            yield return new WaitForEndOfFrame();
            i = i + (Time.deltaTime * rate);
            thisTransform.position = Vector3.Lerp(startPos, endPos, i);
            if(thisTransform.localScale.x < 4f)
                thisTransform.localScale += new Vector3(0.9f, 0.9f, 0);
        }
        darkBG.SetActive(true);
    }
}
