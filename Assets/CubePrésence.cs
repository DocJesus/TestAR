using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CubePrésence : MonoBehaviour
{
    BoxCollider collider;

    // Start is called before the first frame update
    void Start()
    {
        collider = GetComponent<BoxCollider>();
    }

    // Update is called once per frame
    void Update()
    {
        transform.Rotate(0, 50 * Time.deltaTime, 0);
    }
}
