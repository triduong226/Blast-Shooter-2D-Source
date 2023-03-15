using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    [SerializeField]
    private GameObject enemy1,enemy2,enemy3;

    private BoxCollider2D box;

    void Start(){
        StartCoroutine(EnemySpawner());
    }

    // Start is called before the first frame update
    void Awake()
    {
        box = GetComponent<BoxCollider2D>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    IEnumerator EnemySpawner(){
        yield return new WaitForSeconds (Random.Range(0.5f,3f));
        float minX = -box.bounds.size.x/2f;
        float maxX = box.bounds.size.x/2f;

        Vector3 temp = transform.position;
        temp.x = Random.Range(minX, maxX);
        int n = Random.Range(1,3);
        switch(n){
            case 1: 
                Instantiate (enemy1,temp,Quaternion.identity);
                break;
            case 2:
                Instantiate (enemy2,temp,Quaternion.identity);
                break;
            default:
                Instantiate (enemy3,temp,Quaternion.identity);
                break;
        }
        
            

        StartCoroutine(EnemySpawner());
    }
}
