using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AutoDestroy : MonoBehaviour
{
    public float lifeTime = 0.5f;

    private void OnEnable()
    {
        Invoke("EndLife", lifeTime);
    }

    public void EndLife()
    {
        Destroy(this.gameObject);
    }
}
