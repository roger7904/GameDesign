using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;

public class PlayerContorl : MonoBehaviour
{
    public float moveSpeed=0.7f;
    private Rigidbody2D playerRb; 
    private Collider2D playerCollider;
    private Vector2 playerDirection;
    private Joystick joystick;
    Vector2 movement; 

    void Awake(){
        playerRb = GetComponent<Rigidbody2D>();
        playerCollider = GetComponent<Collider2D>();
        joystick=GameObject.FindGameObjectWithTag("Joystick").GetComponent<Joystick>();
    }

    // Update is called once per frame
    void Update()
    {
        movement.x=joystick.Horizontal;
        movement.y=joystick.Vertical;
        if(movement.x==0 && movement.y==0){
            return;
        }
        if(Mathf.Abs(movement.x)>Mathf.Abs(movement.y)){
            if(movement.x<0){
                playerDirection=Vector2.left;
            }else{
                playerDirection=Vector2.right;
            }
        }else{
            if(movement.y<0){
                playerDirection=Vector2.down;
            }else{
                playerDirection=Vector2.up;
            }
        }
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
        RaycastHit2D hit = Physics2D.Raycast(transform.position, playerDirection,1);
        if (hit.collider != null)
        {
            Debug.Log(hit.collider.name);
        }
        playerCollider.enabled=true;
        
    }
}
