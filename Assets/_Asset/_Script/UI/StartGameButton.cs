using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartGameButton : MenuButton
{
    [SerializeField] private GameObject targetMenu;

    public override void ClickEvent()
    {
        SwitchMenuGroup();
    }

    private void SwitchMenuGroup()
    {
        transform.parent.gameObject.SetActive(false);
        targetMenu.SetActive(true);
    }    
}
