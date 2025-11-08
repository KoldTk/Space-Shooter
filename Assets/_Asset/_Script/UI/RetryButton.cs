using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RetryButton : MonoBehaviour
{
    public void Retry()
    {
        EventDispatcher<bool>.Dispatch(Event.Retry.ToString(), true);
        transform.parent.gameObject.SetActive(false);
    }    
}
