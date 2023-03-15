using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BGScaler : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        var worldHeight = Camera.main.orthographicSize * 2f;
        //Debug.log (worldHeight = 10)
        var worldWidth = worldHeight * Screen.width / Screen.height;
        // 10 * 196 / 327 = Scale.X (6)
        transform.localScale = new Vector3(worldWidth, worldHeight, 0f);
    }

}
