using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Expirience : MonoBehaviour
{
    public TMP_Text expText;
    public TMP_Text levelText;
    public Slider slider;

    public int exp = 0;
    public int lvl = 1;

    int maxExp;

    // Start is called before the first frame update
    void Start()
    {
        maxExp = lvl * 10;
    }

    // Update is called once per frame
    void Update()
    {
        expText.text = exp.ToString() + "/" + maxExp.ToString();
        levelText.text = lvl.ToString();
        slider.value = exp / maxExp;

        if(exp >= maxExp)
        {
            UpLevel();
        }
    }

    void UpLevel()
    {
        exp -= maxExp;
        lvl++;

    }
}
