using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBullet3 : MonoBehaviour
{
    public float speed;

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

    // Update is called once per frame
    void FixedUpdate()
    {
        myBody.velocity = new Vector2(1f, -speed * 5);
    }
    void OnTriggerEnter2D(Collider2D target){
        if(target.tag == "Player"){
            if(Plane.health >1){
                Destroy(gameObject);
                Plane.health -=1;
                GamePlayController.instance._SetHeart(Plane.health);
                Debug.Log(Plane.health.ToString());
                
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
        if(target.tag == "Border2"){
            speed *= -1;
        }
    }
}
