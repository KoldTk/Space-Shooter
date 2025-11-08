using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class RetryMenu : MonoBehaviour
{
    public Button[] buttons;
    private int currentIndex = 0;

    private void OnEnable()
    {
        Time.timeScale = 0;
        HighlightButton(currentIndex);
    }
    private void OnDisable()
    {
        Time.timeScale = 1;
    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            currentIndex = (currentIndex + 1) % buttons.Length;
            HighlightButton(currentIndex);
        }
        else if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            currentIndex = (currentIndex - 1 + buttons.Length) % buttons.Length;
            HighlightButton(currentIndex);
        }
        else if (Input.GetKeyDown(KeyCode.Return))
        {
            buttons[currentIndex].onClick.Invoke(); //Call button event
        }
    }
    private void HighlightButton(int index)
    {
        for (int i = 0; i < buttons.Length; i++)
        {
            //Setup hightlight color here, or using custom highlight
            ColorBlock cb = buttons[i].colors;
            //Disable highlight for non selected
            cb.normalColor = (i == index) ? Color.yellow : Color.white;
            buttons[i].colors = cb;
        }
    }
}
