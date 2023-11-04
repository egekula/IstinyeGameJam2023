using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class DialogueManager : MonoBehaviour
{
    [SerializeField] private string[] dialogueSentences;
    [SerializeField] private TextMeshProUGUI sentencesText;
    [SerializeField] private int indexOfSentences;
    [SerializeField] private GameObject continueButton;
    private void Update()
    {
        if (sentencesText.text == dialogueSentences[indexOfSentences])
        {
            continueButton.SetActive(true);
        }
        if (indexOfSentences == dialogueSentences.Length - 1)
        {
            //indexOfSentences++;
            if (indexOfSentences == dialogueSentences.Length)
            {
                continueButton.SetActive(true);
                //sentencesText.text = "";
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            StartCoroutine(DisplayDialogue());
        }
    }

    IEnumerator DisplayDialogue()
    { 
        foreach (char letter in dialogueSentences[indexOfSentences].ToCharArray())
        { 
            sentencesText.text += letter; 
            yield return new WaitForSeconds(0.02f);
        }
    }
    public void NextDialogue()
    {
        if (indexOfSentences<dialogueSentences.Length-1)
        {
            continueButton.SetActive(false);
            indexOfSentences++;
            sentencesText.text = "";
            StartCoroutine(DisplayDialogue());
        }
        else
        {
            sentencesText.text="";
            continueButton.SetActive(false);
        }
    }
}
