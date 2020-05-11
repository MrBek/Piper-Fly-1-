using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIScrollItems : MonoBehaviour
{
    public Material[] material;
    Renderer renderer;
    public static UIScrollItems uIScrollItems;
    public TrailRenderer[] trails;
    
    private void Start()
    {
        if (uIScrollItems == null)
            uIScrollItems = this;
        
        renderer = GetComponent<Renderer>();
    }

    private void Update()
    {
        if (transform.name != "1")
        {
            if (PlayerPrefs.GetInt("charBuy_" + transform.name) == 1)
            {
                renderer.material = material[1];

            }
            else
            {
                renderer.material = material[0];
            }
        }
        else
        {
            renderer.material = material[0];
        }
    }

    public void SetTexture(int i)
    {
        renderer.material = material[i];
    }
}
