using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class testingDEEP : MonoBehaviour
{
    // Start is called before the first frame update
    public void OpenChannel()
    {
        Application.OpenURL("ZEPETO://home/feed?");
    }
}
