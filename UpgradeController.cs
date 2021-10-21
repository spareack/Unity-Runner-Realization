using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UpgradeController : MonoBehaviour
{
    public GameObject UpdatePanel1;

    public void OpenUpdatePanel1()
    {
        UpdatePanel1.SetActive(true);
    }

    public void CloseUpdatePanel1()
    {
        UpdatePanel1.SetActive(false);
    }
}
