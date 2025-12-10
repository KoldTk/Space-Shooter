using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChapterSelectButton : MenuButton
{
    [SerializeField] private GameObject targetMenu;
    [SerializeField] private string chapter;
    public override void ClickEvent()
    {
        SwitchMenuGroup();
        ChapterSelect();
    }
    private void ChapterSelect()
    {
        GameManager.Instance.selectedChapter = chapter;
    }
    private void SwitchMenuGroup()
    {
        transform.parent.gameObject.SetActive(false);
        targetMenu.SetActive(true);
    }
}
