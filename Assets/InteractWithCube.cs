using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractWithCube : MonoBehaviour
{
    int cubeMask;

    // Start is called before the first frame update
    void Start()
    {
        cubeMask = LayerMask.GetMask("Cubes");
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            RaycastHit hit;
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);


            if (Physics.Raycast(ray, out hit, 200, cubeMask))
            {
                Material cubeMaterial = hit.collider.GetComponent<MeshRenderer>().material;

                if (hit.collider.transform.tag == "Cube" && cubeMaterial.color != Color.red)
                {
                    cubeMaterial.color = Color.red;
                }
                else if (hit.collider.transform.tag == "Cube" && cubeMaterial.color == Color.red)
                {
                    cubeMaterial.color = Color.white;
                }

            }
        }
    }
}
