using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RemoveThis : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        Invoke("Destroy", 10);
    }

    public void Destrtoy()
    {
        Destroy(this);
    }
}
