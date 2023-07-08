using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroySelf : MonoBehaviour
{
    // Start is called before the first frame update
    public float duration;
    void Awake(){
        StartCoroutine(DestroyObject(duration));
    }

    IEnumerator DestroyObject(float duration){
        print("coroutine began");
        yield return new WaitForSeconds(duration);
        Destroy(gameObject);
    }
}
