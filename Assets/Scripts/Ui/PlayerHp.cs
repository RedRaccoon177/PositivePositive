using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHp : MonoBehaviour
{
    Image playerHp;
    // Start is called before the first frame update
    void Start()
    {
        playerHp = GetComponent<Image>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.D))
        {
            Debug.Log("체력 감소");
            DecreasePlayerHp();
        }
        else if (Input.GetKeyDown(KeyCode.A))
        {
            Debug.Log("체력 증가");
            IncreasePlayerHp();

        }
    }

    public void DecreasePlayerHp()
    {
        playerHp.fillAmount -= 0.2f;
    }
    public void IncreasePlayerHp()
    {
        playerHp.fillAmount += 0.2f;
    }
}
