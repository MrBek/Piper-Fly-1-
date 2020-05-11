using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class setBolAnim : MonoBehaviour
{

    public Animator allAnim;
    public bool animBool;
    public string Boolname;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void setAnimBoolAll()
    {
        allAnim.SetBool(Boolname, animBool);
    }
}
