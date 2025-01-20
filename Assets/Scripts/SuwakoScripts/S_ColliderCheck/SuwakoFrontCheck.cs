using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SuwakoFrontCheck : MonoBehaviour
{
    SuwakoController suwako;

    private void Start()
    {
        suwako = GetComponentInParent<SuwakoController>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Ground" || collision.tag == "Wall")
        {
            if (suwako.RiORLe == -1)
            {
                suwako.RiORLe = 1;
                transform.parent.rotation = new Quaternion(0, 180, 0, 0);
            }
            else if (suwako.RiORLe == 1)
            {
                transform.parent.rotation = new Quaternion(0, 0, 0, 0);
                suwako.RiORLe = -1;
            }
            suwako.rb.velocity = new Vector2(-suwako.rb.velocity.x, suwako.rb.velocity.y);
        }
    }
}
