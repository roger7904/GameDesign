using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerContorl : MonoBehaviour
{
    public float moveSpeed=1f;
    
    private Rigidbody2D playerRb; 
    private  Collider2D playerCollider;

    public Joystick joystick;

    Vector2 movement; 

    void Awake(){
        playerRb = GetComponent<Rigidbody2D>();
        playerCollider = GetComponent<Collider2D>();
    }
    // Update is called once per frame
    void Update()
    {
        movement.x=joystick.Horizontal;
        movement.y=joystick.Vertical;
    }

    void FixedUpdate(){
        if(Mathf.Abs(movement.x)>Mathf.Abs(movement.y)){
            playerRb.MovePosition(playerRb.position+new Vector2(movement.x*moveSpeed,0)*Time.fixedDeltaTime);
        }else{
            playerRb.MovePosition(playerRb.position+new Vector2(0,movement.y*moveSpeed)*Time.fixedDeltaTime);
        }
    }

    public void cast(){
        Debug.Log("call cast function");
        playerCollider.enabled=false;
        //判斷角色面對方向
        Vector2 direction = Vector2.up + Vector2.left+Vector2.down+Vector2.right;
        RaycastHit2D hit = Physics2D.Raycast(transform.position, direction);
        if (hit.collider != null)
        {
            Debug.Log(hit.collider.name);
        }
        playerCollider.enabled=true;
        
    }
}
