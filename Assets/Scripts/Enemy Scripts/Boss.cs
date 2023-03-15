using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss : MonoBehaviour
{
    public static Boss instance;
    public float bossSpeed;
    [SerializeField]
    private GameObject bullet1, bullet2, bullet3;

    private Rigidbody2D myBody;


    void Awake(){
        MakeInstace();
        myBody = GetComponent<Rigidbody2D> ();
    }
    void MakeInstace(){
        if(instance == null){
            instance = this;
        }
    }
    
    void Start(){
        StartCoroutine(BossShoot1());
    }

    IEnumerator BossShoot1(){
        yield return new WaitForSeconds(Random.Range(0.2f,0.7f));
        if(HealthBar.instance.currentHealth <=50){
            Vector3 temp1 = transform.position;
            temp1.y -= 0.5f;
            Instantiate(bullet1,temp1,Quaternion.identity);

            Vector3 temp2 = transform.position;
            temp2.y += 0.5f;
            Instantiate(bullet1,temp2,Quaternion.identity);

            Vector3 temp3 = transform.position;
            temp3.y -= 0.5f;
            Instantiate(bullet2,temp3,Quaternion.identity);

            Vector3 temp4 = transform.position;
            temp4.y += 0.5f;
            Instantiate(bullet2,temp4,Quaternion.identity);
        }
        float i = Random.Range(-3,3);
        Vector3 temp5 = transform.position;
        temp5.x = i;
        Instantiate(bullet3,temp5,Quaternion.identity);

        StartCoroutine(BossShoot1());
    }
    void OnTriggerEnter2D(Collider2D target){
        if(target.tag == "Player"){
            if(Plane.health>1){
                Plane.health -=1;
                GamePlayController.instance._SetHeart(Plane.health);
            }
            else{
                Plane.health=0;
                Destroy(target.gameObject);
                GamePlayController.instance.PlaneDiedShowPanel(Plane.score);
            }
        }
    }
}
