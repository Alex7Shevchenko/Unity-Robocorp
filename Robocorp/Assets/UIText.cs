using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIText : MonoBehaviour
{
    [SerializeField] int sprayType;
    [SerializeField] UIText[] scripts = new UIText[0];
    public TMP_Text[] texts = new TMP_Text[0];

    public bool pressed, highlighted;

    int reseter;
    private void Update()
    {
        Highlighted();
        Pressed();
    }

    private void Highlighted()
    {
        if(highlighted && !pressed)
        {
            reseter = 0;

            for (int i = 0; i < texts.Length; i++)
            {
                texts[i].color = new Color32(155, 255, 155, 25);
            }
        }
        else
        {
            if (pressed) return;
            for (int i = 0; i < texts.Length; i++)
            {
                texts[i].color = new Color32(0, 0, 0, 255);
            }
        }
    }

    private void Pressed()
    {
        if (pressed && reseter == 0)
        {
            reseter++;

            for (int i = 0; i < texts.Length; i++)
            {
                texts[i].color = new Color32(0, 255, 0, 255);
            }

            for (int i = 0;i < scripts.Length; i++)
            {
                scripts[i].pressed = false;
            }
        }
    }

    public int Status()
    {
        if (pressed) return sprayType;

        return 1;
    }
}
