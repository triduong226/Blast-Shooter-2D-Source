using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy2 : MonoBehaviour
{
    public float enemySpeed;
    [SerializeField]
    private GameObject bullet, bullet1;

    private Rigidbody2D myBody;
    
    [SerializeField]
    private AudioSource audioSouce;
    
    [SerializeField]
    private AudioClip fallClip;
    
    void Start(){
        StartCoroutine(EnemyShoot());
    }
    // Start is called before the first frame update
    void Awake()
    {
        myBody = GetComponent<Rigidbody2D> ();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        myBody.velocity = new Vector2(0f, -enemySpeed);
    }

    IEnumerator EnemyShoot(){
        yield return new WaitForSeconds(Random.Range(1.5f,3f));

        Vector3 temp = transform.position;
        temp.y -= 0.5f;
        Instantiate(bullet,temp,Quaternion.identity);

        Vector3 temp2 = transform.position;
        temp2.y -= 0.5f;
        Instantiate(bullet1,temp2,Quaternion.identity);

        StartCoroutine(EnemyShoot());
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
