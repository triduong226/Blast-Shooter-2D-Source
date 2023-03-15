using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Meteor : MonoBehaviour
{
    public float meteorSpeed;
    public static Meteor instance;
    public GameObject drop1;

    private Rigidbody2D myBody;
    
    [SerializeField]
    private AudioSource audioSouce;
    
    [SerializeField]
    private AudioClip fallClip;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }
    void MakeInstace(){
        if(instance == null){
            instance = this;
        }
    }
    // Update is called once per frame
    void Awake()
    {
        MakeInstace();
        myBody = GetComponent<Rigidbody2D> ();
    }

    void FixedUpdate()
    {
        myBody.velocity = new Vector2(0f, -meteorSpeed);
    }
    void OnTriggerEnter2D(Collider2D target){
        if(target.tag == "Player"){
            if(Plane.health>1){
                Destroy(gameObject);
                Plane.health -=1;
                GamePlayController.instance._SetHeart(Plane.health);
                
            }
            else{
                Plane.health=0;
                GamePlayController.instance._SetHeart(Plane.health);
                Destroy(target.gameObject);
                GamePlayController.instance.PlaneDiedShowPanel(Plane.score);
                audioSouce.PlayOneShot(fallClip);
            }
        }
        if(target.tag == "Border"){
            Destroy(gameObject);
        }
    }
}
