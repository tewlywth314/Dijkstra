using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LineCollide : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject LineDrawers;
    
   
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    void OnTriggerEnter2D(Collider2D other)
    {
        if(other.tag == "Collide")
        {
            LineDrawer.EndPoint = other.gameObject.transform.parent.name;
            Debug.Log(LineDrawer.EndPoint);
        }
        
    }
    private void OnMouseDown()
    {
        LineDrawers.GetComponent<LineDrawer>().MouseDownFnc();
    }
    private void OnMouseUp()
    {
        LineDrawers.GetComponent<LineDrawer>().MouseUpFnc();
    }
}
