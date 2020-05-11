using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI.Extensions;

public class VisibleCharacterUI : MonoBehaviour
{
    public GameObject[] characters;
    public static VisibleCharacterUI visibleCharacterUI;

    private void Awake()
    {
        if (visibleCharacterUI == null)
            visibleCharacterUI = this;
    }

    public void SetVisible(int j)
    {
        for (int i = 0; i < characters.Length; i++)
        {
            characters[i].SetActive(false);
            
            if (i == j && i < 11 && i > 0)
            {
                characters[j].SetActive(true);
                characters[j + 1].SetActive(true);
                characters[j - 1].SetActive(true);
                i++;
            }else if (i == j && i == 0)
            {
                characters[j].SetActive(true);
                characters[j + 1].SetActive(true);
                i++;

            }else if (i == j && i == 11)
            {
                characters[j].SetActive(true);
                characters[j - 1].SetActive(true);
            }
        }
    }
}
