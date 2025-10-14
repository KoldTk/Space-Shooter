using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSpellBackgroundFadeIn : BackgroundFadeIn
{
    private void OnEnable()
    {
        EventDispatcher<bool>.AddListener(Event.UsingSpell.ToString(), ShowBackground);
        EventDispatcher<bool>.AddListener(Event.SpellEnd.ToString(), HideBackground);
    }
    private void OnDisable()
    {
        EventDispatcher<bool>.RemoveListener(Event.UsingSpell.ToString(), ShowBackground);
        EventDispatcher<bool>.RemoveListener(Event.SpellEnd.ToString(), HideBackground);
    }
    private void ShowBackground(bool isUsingSpell)
    {
        ShowBackground();
    }
    private void HideBackground(bool spellEnd)
    {
        HideBackground();
    }    
}
