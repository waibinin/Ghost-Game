using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class GhostMovement : MonoBehaviour
{
   [SerializeField]
   private InputAction mouseClickAction;
   [SerializeField]
   private float playerSpeed = 10f;

   private Coroutine coroutine;

   private Camera mainCamera;
   private Vector3 targetPosition;

   private Quaternion localRotation  = Quaternion.Euler(0f, 0f, 0f);


   private void Awake(){
    mainCamera = Camera.main;
   
   }

   private void Update() {
    
    Vector3 targetDirection = targetPosition - transform.position;
    localRotation = Quaternion.LookRotation(targetDirection);
    transform.rotation = Quaternion.Slerp(transform.rotation, localRotation, Time.deltaTime * playerSpeed);

    Debug.Log("Destination:" + targetPosition);
    Debug.Log("Local Rotation:"+ localRotation);
   }
    

    private void OnEnable(){
        mouseClickAction.Enable();
        mouseClickAction.performed+=Move;
    }

    private void OnDisable()
    {
        mouseClickAction.performed -=Move;
        mouseClickAction.Disable();
       
        
    }

    private void Move(InputAction.CallbackContext context)
    {
       Ray ray = mainCamera.ScreenPointToRay(Mouse.current.position.ReadValue());

       if(Physics.Raycast(ray:ray,hitInfo:out RaycastHit hit)&& hit.collider){
        if(coroutine!=null)StopCoroutine(coroutine);
        coroutine=StartCoroutine(PlayerMoveTowards(hit.point));
        targetPosition=hit.point;

       }

       FindObjectOfType<DialogueManager>().DisplayNextSentence();
            
      
    }

    private IEnumerator PlayerMoveTowards(Vector3 target)
    {
    
        while(Vector3.Distance(transform.position,target)>0.1f)
        {
          Vector3 destination = Vector3.MoveTowards(transform.position, target, playerSpeed * Time.deltaTime);
            
            transform.position= destination;
            

           
             
           // Vector3 relativePoint = transform.InverseTransformPoint(destination);
             // Vector3 relativePoint = (destination-transform.position).normalized;
             
             // transform.rotation *= localRotation;

           
             
            

            yield return null;
        }
        
       
    }

    private void OnDrawGizmos()
    {
        Gizmos.color= Color.red;
       Gizmos.DrawSphere(targetPosition,1);
    }

}
