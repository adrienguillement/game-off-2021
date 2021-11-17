using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KillzoneTop : MonoBehaviour
{
    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.tag == "bulletPlayer")
        {
            Destroy(col.gameObject);
        }
    }
}