using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThrowHook : MonoBehaviour
{

    public GameObject hook;
    GameObject curHook;
    Vector2 dest;

    public bool IsHookEnabled()
    {
        return curHook != null;
    }

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            dest = Camera.main.ScreenToWorldPoint(Input.mousePosition);

            curHook = Instantiate(hook, transform.position, Quaternion.identity);

            if (curHook != null)
            {
                curHook.GetComponent<RopeScript>().dest = dest;
            }
        }

        if (Input.GetMouseButtonUp(0))
        {
            Destroy(curHook);
            gameObject.GetComponent<Player>().SetBoost(false);
        }
    }

}
