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

private float sliderLastX = 0f;
public float sliderX = 0f;
   

   private void Awake(){
    mainCamera = Camera.main;
   
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

        // transform.rotation = Quaternion.Slerp(transform.rotation,localRotation,playerSpeed * Time.deltaTime);
        transform.rotation = transform.rotation * localRotation; 
      
    }

    private IEnumerator PlayerMoveTowards(Vector3 target)
    {
     
    
        while(Vector3.Distance(transform.position,target)>0.1f)
        {
          Vector3 destination = Vector3.MoveTowards(transform.position, target, playerSpeed * Time.deltaTime);
            transform.position= destination;

           
             localRotation = Quaternion.Euler(destination);
              print("Destination:");
             Debug.Log(destination);
             print("Local Rotation:");
             Debug.Log(localRotation);
            

            yield return null;
        }
        
       
    }

    private void OnDrawGizmos()
    {
        Gizmos.color= Color.red;
       Gizmos.DrawSphere(targetPosition,1);
    }

}
