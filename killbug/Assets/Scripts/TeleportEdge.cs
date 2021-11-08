using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TeleportEdge : MonoBehaviour
{

    void OnTriggerEnter2D(Collider2D col)
    {
        col.transform.position = new Vector2(0, col.gameObject.transform.position.x);
    }
}
