using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MenuGroupManager : MonoBehaviour
{
    [SerializeField] Button[] buttons;
    private int currentIndex = 0;
    private Button targetButton;
    private bool isHighlighted;
    private void OnEnable()
    {
        HighlightButton(currentIndex);
    }

    // Update is called once per frame
    void Update()
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
            //Disable highlight for non selected
            isHighlighted = (i == index);
            buttons[i].image.enabled = isHighlighted;
        }
    }

}
