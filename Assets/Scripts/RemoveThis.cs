using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RemoveThis : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        Invoke("DestrtoyThis", 10);
    }

    public void DestrtoyThis()
    {
        Destroy(this.gameObject);
    }
}
