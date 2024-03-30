using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickupController : MonoBehaviour
{
    // Pickup color
    public Material Player1;
    public Material Player2;
    public Material Player3;
    // Pickup color, as a property
    private int PickupColor;
    
    void Awake()
    {
        
    }

    public void SetColor(int color)
    {
        PickupColor = color;
        if (color / 33 == 0)
        {
            GetComponent<MeshRenderer>().material = Player1;
            Debug.Log("Red");
        }
        else if (color / 33 == 1)
        {
            GetComponent<MeshRenderer>().material = Player2;
            Debug.Log("Green");
        }
        else
        {
            GetComponent<MeshRenderer>().material = Player3;
            Debug.Log("Blue");
        }
        
    }

    public int GetColor()
    {
        return PickupColor;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
