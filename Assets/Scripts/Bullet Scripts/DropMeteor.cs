using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DropMeteor : MonoBehaviour
{
    public float speed;

    private Rigidbody2D myBody;

    // Start is called before the first frame update
    void Awake()
    {
        myBody = GetComponent<Rigidbody2D> ();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        myBody.velocity = new Vector2(0f, -speed);
    }
    void OnTriggerEnter2D(Collider2D target){
        if(target.tag == "Player"){
            if(Plane.health<3){
                Plane.health+=1;
                GamePlayController.instance._SetHeart(Plane.health);
                Destroy(gameObject);
                Debug.Log(Plane.health.ToString());
            }
            else{
                Debug.Log("Max");
                Destroy(gameObject);
            }
        }
        if(target.tag == "Border"){
            Destroy(gameObject);
        }
    }
}
