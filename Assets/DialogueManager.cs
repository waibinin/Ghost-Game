using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class DialogueManager : MonoBehaviour
{
    public Queue<string> sentences;
    public TextMeshProUGUI nameText;
    public TextMeshProUGUI dialogueText;


    // Start is called before the first frame update
    void Start()
    {
        sentences = new Queue <string>();
          nameText.enabled=false;
          dialogueText.enabled=false;
    }

    /*void FixedUpdate()
    {
        if (Input.GetMouseButtonDown(0) && dialogueText.enabled)
        {
            DisplayNextSentence();
        }
    }*/

    public void StartDialogue(Dialogue dialogue){

        nameText.enabled=true;
        dialogueText.enabled=true;
        nameText.text = dialogue.name;

        sentences.Clear();

        foreach (string sentence in dialogue.sentences)
        {
            sentences.Enqueue(sentence);
        }

        DisplayNextSentence();

    }

    public void DisplayNextSentence(){
        if (sentences.Count == 0)
        {
            EndDialogue();
            return;
        }
    string sentence = sentences.Dequeue();
   StopAllCoroutines();
   StartCoroutine(TypeSentence(sentence));
    Debug.Log("Tiro frase");

   IEnumerator TypeSentence (string sentence){
    dialogueText.text = "";
    foreach (char letter in sentence.ToCharArray())
    {
        dialogueText.text+= letter;
        yield return null;
    }
   }   

    }

 public void EndDialogue(){
    Debug.Log("End of conversation");
   nameText.enabled=false;
    dialogueText.enabled=false;
 }
  
}
