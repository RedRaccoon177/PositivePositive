using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHp : MonoBehaviour
{
    public Image playerHp;
    PlayerHPObserver playerHPObserver;
    // Start is called before the first frame update
    void Start()
    {
        playerHPObserver = GameObject.FindWithTag("Player").GetComponent<PlayerHPObserver>();
        if (playerHPObserver != null )
        {
            playerHPObserver.OnHealthChanged += HPUpdate;
        }
    }

    private void OnDestroy()
    {
        if (playerHPObserver != null)
        {
            playerHPObserver.OnHealthChanged -= HPUpdate;
        }
    }

    public void HPUpdate(float HP)
    {
        playerHp.fillAmount = HP;
    }
}
