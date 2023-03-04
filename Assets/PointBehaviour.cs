using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PointBehaviour : MonoBehaviour
{
    public GameObject Camara2;
     public Dialogue dialogue;
    AudioSource charlita;
    // Start is called before the first frame update
    void Start()
    {
        charlita = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

 void OnTriggerEnter(Collider other)
    {
        Camara2.SetActive(true);
        charlita.Play();
        Debug.Log("Activate");
        TriggerDialogue();
        
     
    }

 void OnTriggerExit(Collider other)
    {
        Camara2.SetActive(false);
       FindObjectOfType<DialogueManager>().EndDialogue();
        
    }

    public void TriggerDialogue(){
        FindObjectOfType<DialogueManager>().StartDialogue(dialogue);
    }
}
