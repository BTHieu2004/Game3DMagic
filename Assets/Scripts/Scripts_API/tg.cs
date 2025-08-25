using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class tg : MonoBehaviour
{
    public GameObject text;
    public static string name;
    public static string regionname;
    public void gan()
    {
        text.GetComponentsInChildren<TextMeshProUGUI>()[0].text = name;
        text.GetComponentsInChildren<TextMeshProUGUI>()[1].text = regionname;
    }
}
