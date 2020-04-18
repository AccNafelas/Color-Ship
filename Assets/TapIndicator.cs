using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TapIndicator : MonoBehaviour
{
    public void Activate()
    {
        this.gameObject.SetActive(true);
    }

    public void Deactivate()
    {
        this.gameObject.SetActive(false);
    }
}
