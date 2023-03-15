using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class YellowBullets : MonoBehaviour
{
    public static float speed = 1;

    private Rigidbody2D myBody;

    public int damage;

    public GameObject drop1, drop2,drop3;

    [SerializeField]
    private AudioSource audioSouce;
    
    [SerializeField]
    private AudioClip hitClip;
    // Start is called before the first frame update
    void Start()
    {
    
    }

    // Start is called before the first frame update
    void Awake()
    {
        myBody = GetComponent<Rigidbody2D> ();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        myBody.velocity = new Vector2(0f,speed);
    }
    void OnTriggerEnter2D(Collider2D target){
        if(target.tag == "Enemy"){
            audioSouce.PlayOneShot(hitClip);
            Destroy(target.gameObject);
            Destroy(gameObject);
            Plane.score++;
            if(GamePlayController.instance != null){
                GamePlayController.instance._SetScore(Plane.score);
            }
        }
        if(target.tag == "Meteor"){
            Vector3 temp = target.gameObject.transform.position;
            temp.y -= 0.5f;
            int i = Random.Range(1,15);
            switch(i){
                default: 
                    Destroy(target.gameObject);
                    Destroy(gameObject);
                    break;
                case 1:
                    Instantiate(drop1, temp, Quaternion.identity);
                    Destroy(target.gameObject);
                    Destroy(gameObject);
                    break;
                case 2:
                    Instantiate(drop2, temp, Quaternion.identity);
                    Destroy(target.gameObject);
                    Destroy(gameObject);
                    break;
                case 3:
                    Instantiate(drop3, temp, Quaternion.identity);
                    Destroy(target.gameObject);
                    Destroy(gameObject);
                    break;
            }
            
            
        }
        if(target.tag == "Boss"){
            if(HealthBar.instance.currentHealth <= damage){
                audioSouce.PlayOneShot(hitClip);
                HealthBar.instance.currentHealth = 0;
                Destroy(target.gameObject);
                Destroy(gameObject);
                Plane.score += 100;
                
                if(GamePlayController.instance != null){
                    GamePlayController.instance._SetScore(Plane.score);
                }
                GamePlayController.instance.PlaneWonShowPanel(Plane.score);
                Plane.score = 0;

            }
            else{
                audioSouce.PlayOneShot(hitClip);
                Plane.score += 3;
                audioSouce.PlayOneShot(hitClip);
                if(GamePlayController.instance != null){
                    GamePlayController.instance._SetScore(Plane.score);
                }
                HealthBar.instance.takeDamage(damage);
                Destroy(gameObject);
            }
        }
        if(target.tag == "Border"){
            Destroy(gameObject);
        }
    }
    
}
