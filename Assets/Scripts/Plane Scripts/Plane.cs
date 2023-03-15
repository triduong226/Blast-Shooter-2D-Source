using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Plane : MonoBehaviour
{
    public static int health = 3;
    public float planeSpeed;

    public static float shootSpeed = 1;

    private Rigidbody2D myBody;
    private Rigidbody2D myBody2;

    public static int score = 0;

    public Joystick movementJoystick;

    [SerializeField]
    private GameObject bullet;
    private bool canShoot = true;

    public static bool joystickM = false;
    // Start is called before the first frame update
    void Awake()
    {
        myBody = GetComponent<Rigidbody2D> ();
        myBody2 = GetComponent<Rigidbody2D> ();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if(!joystickM){
            PlaneMovement();
        }
        else{
            JoystickMovement();
        }
        
        if(SceneManager.GetActiveScene().name == "Stage 1"){
            if(score==5){
                score++;
                SceneManager.LoadScene("Story 2"); 
            }
        }
        if(SceneManager.GetActiveScene().name == "Stage 2"){
            if(score>=10){
                score++;
                SceneManager.LoadScene("Story 3"); 
            }
        }
        
    }
    //Spawn bullet
    void Update(){
        if(canShoot){
            StartCoroutine(Shoot());
        }
    }
    //Move
    void PlaneMovement(){
        float xAxis = Input.GetAxisRaw("Horizontal") * planeSpeed;
        float yAxis = Input.GetAxisRaw("Vertical") * planeSpeed;
        myBody.velocity = new Vector2(xAxis,yAxis);
    }
    void JoystickMovement(){
        if(movementJoystick.joystickVec.y != 0)
            {
                myBody2.velocity = new Vector2(movementJoystick.joystickVec.x * planeSpeed, movementJoystick.joystickVec.y * planeSpeed);
            }
            else
            {
                myBody2.velocity = Vector2.zero;
            }
    }
    IEnumerator Shoot(){
        canShoot = false;
        Vector3 temp = transform.position;
        temp.y += 0.6f;
        Instantiate(bullet,temp,Quaternion.identity);
        yield return new WaitForSeconds(shootSpeed);
        canShoot = true;
    }
    
}
