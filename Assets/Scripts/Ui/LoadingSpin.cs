using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoadingSpin : MonoBehaviour
{
    [SerializeField]
    float spin;

    // Update is called once per frame
    void Update()
    {
        transform.rotation = Quaternion.Euler(0,0, transform.rotation.eulerAngles.z + spin);
    }
}
