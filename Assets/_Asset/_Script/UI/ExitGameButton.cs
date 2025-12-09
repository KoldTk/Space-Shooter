using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExitGameButton : MenuButton
{
    public override void ClickEvent()
    {
        Application.Quit();
    }
}
