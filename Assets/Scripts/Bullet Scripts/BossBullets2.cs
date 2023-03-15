using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossBullets2 : MonoBehaviour
{
    public float speedy;
    private Rigidbody2D myBody;

    [SerializeField]
    private AudioSource audioSouce;
    
    [SerializeField]
    private AudioClip fallClip;
    // Start is called before the first frame update
    void Awake()
    {
        myBody = GetComponent<Rigidbody2D> ();
    }
    void FixedUpdate()
    {
        myBody.velocity = new Vector2(0f, -speedy * 5);
    }
    void OnTriggerEnter2D(Collider2D target){
        if(target.tag == "Player"){
            if(Plane.health >1){
                Destroy(gameObject);
                Plane.health -=1;
                Debug.Log(Plane.health.ToString());
                GamePlayController.instance._SetHeart(Plane.health);
            }
            else{
                GamePlayController.instance._SetHeart(0);
                Destroy(target.gameObject);
                GamePlayController.instance.PlaneDiedShowPanel(Plane.score);
                Plane.score = 0;
                audioSouce.PlayOneShot(fallClip);
            }
        }
        if(target.tag == "Border"){
            Destroy(gameObject);
        }
    }
}
