using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShockwaveDestroyEnemies : MonoBehaviour
{
    void OnParticleCollision(GameObject obj)
    {
        Destroy(obj);
        Destroy(gameObject);
    }
}
