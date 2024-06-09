using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BurgerRecipePanel: MonoBehaviour
{
    public GameObject BulgogiPanel;
    public GameObject CheesePanel;
    public GameObject HotCrispyPanel;
    public GameObject ShrimpPanel;
    public GameObject DoublePanel;

    // Start is called before the first frame update
    void Start()
    {
        Time.timeScale = 0;
        BulgogiPanel.SetActive(false);
        CheesePanel.SetActive(false);
        HotCrispyPanel.SetActive(false);
        ShrimpPanel.SetActive(false);
        DoublePanel.SetActive(false);
    }

    public void Bulgogi()
    {
        Time.timeScale = 1;
        BulgogiPanel.SetActive (true);
        CheesePanel.SetActive(false);
        HotCrispyPanel.SetActive(false);
        ShrimpPanel.SetActive(false);
        DoublePanel.SetActive(false);
    }

    public void Cheese()
    {
        Time.timeScale = 1;
        BulgogiPanel.SetActive(false);
        CheesePanel.SetActive(true);
        HotCrispyPanel.SetActive(false);
        ShrimpPanel.SetActive(false);
        DoublePanel.SetActive(false);
    }

    public void HotCrispy()
    {
        Time.timeScale = 1;
        BulgogiPanel.SetActive(false);
        CheesePanel.SetActive(false);
        HotCrispyPanel.SetActive(true);
        ShrimpPanel.SetActive(false);
        DoublePanel.SetActive(false);
    }

    public void Shrimp()
    {
        Time.timeScale = 1;
        BulgogiPanel.SetActive(false);
        CheesePanel.SetActive(false);
        HotCrispyPanel.SetActive(false);
        ShrimpPanel.SetActive(true);
        DoublePanel.SetActive(false);
    }

    public void Double()
    {
        Time.timeScale = 1;
        BulgogiPanel.SetActive(false);
        CheesePanel.SetActive(false);
        HotCrispyPanel.SetActive(false);
        ShrimpPanel.SetActive(false);
        DoublePanel.SetActive(true);
    }
}
