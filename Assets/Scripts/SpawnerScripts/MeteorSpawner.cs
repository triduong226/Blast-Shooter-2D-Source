using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeteorSpawner : MonoBehaviour
{
    [SerializeField]
    private GameObject meteor;

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
        Instantiate (meteor,temp,Quaternion.identity);
        
            

        StartCoroutine(EnemySpawner());
    }
}
