using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DropSpeedUp : MonoBehaviour
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
            if(Plane.shootSpeed > 0.5f){
                Plane.shootSpeed-= 0.1f;
                Destroy(gameObject);
                Debug.Log(Plane.shootSpeed.ToString());
            }
            else{
                Debug.Log("Min");
                Destroy(gameObject);
            }
        }
        if(target.tag == "Border"){
            Destroy(gameObject);
        }
    }
}
