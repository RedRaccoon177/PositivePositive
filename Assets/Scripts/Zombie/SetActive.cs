using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetActive : MonoBehaviour
{
    private void Start()
    {
        //Instantiate(gameObject);
    }
    public void SetActiveTrue()
    {
        gameObject.SetActive(true);
    }
    public void SetActiveFalse()
    {
        gameObject.SetActive(false);
    }
}
