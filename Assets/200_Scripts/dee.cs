using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class dee : MonoBehaviour
{
    public Vector3 he;
    // Start is called before the first frame update
    void Start()
    {
        he = this.gameObject.transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        this.gameObject.transform.position = he;
    }
}
