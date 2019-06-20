using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TImeDeltaTest : MonoBehaviour
{

    void Update()
    {
        print("Update: "+Time.deltaTime);
    }
    void FixedUpdate()
    {
        print("Update: "+Time.deltaTime);
    }
}
