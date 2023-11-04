using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Dialogue : MonoBehaviour
{
    public TextMeshProUGUI textComp;
    public string[] lines;
    public float textSpeed;
    private int index;

    private void Start()
    {
        textComp.text = string.Empty;
        StartDialogue();
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            if (textComp.text == lines[index])
            {
                NextLine();
            }
            else
            {
                StopAllCoroutines();
                textComp.text = lines[index];
            }
        }
    }
    

    void StartDialogue()
    {
        index = 0;
        StartCoroutine("TypeLine");
    }

    IEnumerator TypeLine()
    {
        foreach (char c in lines[index].ToCharArray())
        {
            textComp.text += c;
            yield return new WaitForSeconds(textSpeed);
        }
    }

    void NextLine()
    {
        if (index < lines.Length - 1)
        {
            index++;
            textComp.text = string.Empty;
            StartCoroutine("TypeLine");
        }
        else
        {
            gameObject.SetActive(false);
        }
    }
}
