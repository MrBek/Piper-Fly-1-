using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
public class ChangingPlayer : MonoBehaviour
{
    public Texture[] playerTextures;
    public GameObject[] trails;
    private Renderer renderer;
    public static ChangingPlayer changingPlayer;

    private void Awake()
    {
        if (changingPlayer == null)
            changingPlayer = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        renderer = GetComponent<Renderer>();
    }

    public void ChangeTexture()
    {
        renderer.material.mainTexture = playerTextures[PlayerPrefs.GetInt("enabled_")];

        for (int i = 0; i < trails.Length; i++)
        {
            if (i == PlayerPrefs.GetInt("enabled_"))
                trails[i].SetActive(true);
            else
                trails[i].SetActive(false);
        }
    }
}
