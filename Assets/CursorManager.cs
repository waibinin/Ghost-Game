using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CursorManager : MonoBehaviour
{
    [SerializeField] private Texture2D []cursorTextureArray;
     [SerializeField]private int frameCount;
      [SerializeField] private float frameRate;
    private int currentFrame;
    private float frameTimer;
  
   
    // Start is called before the first frame update
    private void Start()
    {
    Cursor.SetCursor(cursorTextureArray[0],new Vector2(10,10),CursorMode.Auto);
    }

}
