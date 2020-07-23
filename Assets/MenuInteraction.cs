using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MenuInteraction : MonoBehaviour
{
    [SerializeField]
    private GameObject infoPanel;
    private Text avancementText;
    private Animator anim;
    private bool isShow;

    private GameObject[] cubes;

    // Start is called before the first frame update
    void Start()
    {
        isShow = false;
        anim = GetComponent<Animator>();
        avancementText = infoPanel.GetComponentInChildren<Text>();
        cubes = GameObject.FindGameObjectsWithTag("Cube");
    }

    // Update is called once per frame
    void Update()
    {
        if (isShow)
        {
            int nbRed = 0;
            int nb = 0;

            for (int i = 0; i < cubes.Length; i++)
            {
                if (cubes[i].GetComponent<MeshRenderer>().enabled)
                {
                    nb++;
                    if (cubes[i].GetComponent<MeshRenderer>().material.color == Color.red)
                        nbRed++;
                }

            }

            avancementText.text = "Number of Cubes: " + nb + "\n" + "Number of RedCubes: " + nbRed;
        }
    }

    public void slidePanel()
    {
        if (isShow == false)
        {
            anim.SetBool("PanelShow", true);
            isShow = true;
        }
        else
        {
            anim.SetBool("PanelShow", false);
            isShow = false;
        }
    }

}
