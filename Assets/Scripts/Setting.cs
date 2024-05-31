using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Setting : MonoBehaviour
{
    public GameObject SettingPanel;

    void Start()
    {
        SettingPanel.SetActive(false);
    }

    public void ShowSettingPanel()
    {
        SettingPanel.SetActive(true);
    }
}
