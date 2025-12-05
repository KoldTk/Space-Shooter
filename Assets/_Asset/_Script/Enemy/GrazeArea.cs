using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrazeArea : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
       if (collision.CompareTag("Player"))
        {
            GameManager.Instance.evadePoint += 1f;
            EventDispatcher<bool>.Dispatch(Event.StatusChange.ToString(), true);
        }    
    }
}
