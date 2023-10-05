using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class star : MonoBehaviour{

    [SerializeField]
    Transform starPrefab;

    [SerializeField]
    Transform smallPrehab;

    [SerializeField, Range(10, 100)]
    int resolution = 10;

    [SerializeField, Range(1, 100)]
    int radius = 1;

    [SerializeField, Range(0, 3/2)]
    float speed = 1;

    Transform[] stars;

    Transform[] smalls;

    void Awake()
    {
        var position = Vector3.zero;
        var scale = 5* Vector3.one * 2f / 10;
        stars = new Transform[resolution];
        smalls = new Transform[resolution * 3];
        for (int i = 0; i < stars.Length; i++){
            Transform star = stars[i] = Instantiate(starPrefab);
            for (int j = i * 3; j < 3 * i + 3; j++) {
                Transform small = smalls[j] = Instantiate(smallPrehab);
                small.localScale = 5/2 * Vector3.one * 2f / 20;
                position.x = 0;
                position.y = 0;
                small.localPosition = position;
            }
            position.x = 0;
            position.y = 0;
            star.localPosition = position;
            star.localScale = scale;
            star.SetParent(transform, false);
        }
    }

    // Update is called once per frame
    void Update(){
        for (int i = 0; i < stars.Length; i++){
            Transform star = stars[i];
            Vector3 position = star.localPosition;
            position.x += radius* Mathf.Sin(speed * Time.time * i+1) / 100;
            position.y = radius* Mathf.Cos(speed * Time.time * i+1);
            position.z = radius * Mathf.Cos(speed * Time.time * i+1) + i;
            //star.localRotation =Quaternion.Euler(0, 0, Random.Range(0,10));
            star.localPosition = position;
            Vector3 vec = new Vector3(position.x, position.y + 15/2, position.z - 10);
            GameObject.Find("Main Camera").transform.position = vec;
            GameObject.Find("Directional Light").transform.position = vec;
            
            if (Random.Range(0, 250) == 0) GameObject.Find("Directional Light").transform.RotateAround(vec, new Vector3(1, 0, 1), 1);
            //Directional Light
            //if (Random.Range(0,100) == 0) GameObject.Find("Main Camera").transform.RotateAround(vec, new Vector3(0, 0, 1), 1) ;
            //GameObject.Find("Main Camera").transform.rotation = position;
            for (int j = i*3; j < 3*i+3; j++) {
                Transform small = smalls[j];
                Vector3 smallPos = small.localPosition;
                smallPos.x = Mathf.Pow(-1,i) * position.x + 3/2 * Mathf.Sin(speed * Time.time * j + 1);
                smallPos.y = Mathf.Pow(-1, i) * position.y + 25/4 + 3 / 2 * Mathf.Cos(speed * Time.time * j + 1);
                smallPos.z = Mathf.Pow(-1, i) * position.z + 3 / 2 * Mathf.Cos(speed * Time.time * j + 1);
                small.localPosition = smallPos;
            }
        }
    }
}
