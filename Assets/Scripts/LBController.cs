using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class LBController : MonoBehaviour
{
    public static LBController _lb_Controller;
    public string _name;
    public string _token;


    private void Awake()
    {
        if (_lb_Controller)
            Destroy(gameObject);
        _lb_Controller = this;
        DontDestroyOnLoad(gameObject);
    }
    // Start is called before the first frame update
    void Start()
    {


        //PlayerPrefs.DeleteAll();
        if (PlayerPrefs.HasKey("Token"))
        {
            _token = PlayerPrefs.GetString("Token");
            _name = PlayerPrefs.GetString("Name");

        }
        else
        {

        }
    }

    // Update is called once per frame
    void Update()
    {

    }
}
