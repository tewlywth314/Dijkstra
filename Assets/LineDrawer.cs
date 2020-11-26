using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class LineDrawer : MonoBehaviour
{
    // Start is called before the first frame update
    private LineRenderer LineRend;
    private Vector2 mousePos;
    
    private Vector2 followPos;
    private bool Isclicked = false;
    public GameObject Point;
    public GameObject LineCollider;
    public GameObject LineDrawerprefab = null;

    public static string StartPoint = null;
    public static string EndPoint = null;
    public TextMeshProUGUI LabelTMPro;
    



    public bool Followed = false;
    public Node ThisNode ;
    public GameObject LineDis;

    public bool EditLine = false;
   


    void Start()
    {
        LineRend = GetComponent<LineRenderer>();
        LineRend.positionCount = 2;
        LineDis.SetActive(false);
        ThisNode = new Node(Point.name, Point.name, "0");
        Debug.Log(ThisNode);
     

    }
    

    // Update is called once per frame
    void Update()
    {
        
        
        if (ModeSystem.PMode == ModeSystem.Mode.Draw)
        {
            LineDis.SetActive(true);
            if (ThisNode.Spoint != null && ThisNode.Epoint != null)
            {
                LabelTMPro.text = string.Format("{0} -> {1} : {2}", ThisNode.Spoint, ThisNode.Epoint, ThisNode.Distance);
            }

        }
        else
        {
            LineDis.SetActive(false);
            LabelTMPro.text = "";
        }
        if (Input.GetMouseButton(0) && Isclicked == true)
        {
            EditLine = true;
        }


            mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        followPos = new Vector3(mousePos.x, mousePos.y, 0);

        if (EditLine == true)
        {
            mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            
            LineRend.SetPosition(0, new Vector3(this.transform.position.x, this.transform.position.y, 0f));
            LineRend.SetPosition(1, new Vector3(mousePos.x, mousePos.y,0f));
            
           
            

            Vector2 startPos = LineRend.GetPosition(0);
            Vector2 endPos = LineRend.GetPosition(LineRend.positionCount - 1);


            float lineWidth = LineRend.endWidth;
            float lineLength = (startPos- endPos).magnitude;
            
            LineCollider.GetComponent<BoxCollider2D>().size = new Vector2(lineLength , lineWidth);


            Vector2 midPoint = (startPos + endPos) / 2;
            Vector2 NmidPoint = new Vector2(midPoint.x, midPoint.y + 25);
            LineCollider.transform.position = midPoint;
            LineDis.transform.position = NmidPoint;

            float angle = Mathf.Atan2((endPos.y - startPos.y), (endPos.x - startPos.x));
            

            LineCollider.transform.eulerAngles = new Vector3(0, 0, angle *= Mathf.Rad2Deg) ;
            


        }
        if (Input.GetMouseButton(0) && Followed == true)
        {
            Point.transform.position = followPos;
        }
    }
    
    private void OnMouseDown()
    {
        MouseDownFnc();
    }
    private void OnMouseUp()
    {

        MouseUpFnc();
    }

    public void changeDistance()
    {
        ThisNode.Distance = LineDis.GetComponent<TMP_InputField>().text;
        ThisNode.Epoint = EndPoint;
    }
    public void AddNode()
    {
        GameObject LD = Instantiate(LineDrawerprefab, Point.transform.position, Quaternion.identity);
        LD.transform.parent = Point.transform;
        LD.GetComponent<LineDrawer>().Point = Point;
    }
    public void MouseDownFnc()
    {
        if (ModeSystem.PMode == ModeSystem.Mode.Draw)
        {
            Isclicked = true;
            EditLine = true;
            Point.GetComponent<SpriteRenderer>().color = new Color(0, 255, 255);
            StartPoint = Point.name;
            Debug.Log(StartPoint);
            Debug.Log("Yeet");
        }
        else if (ModeSystem.PMode == ModeSystem.Mode.Edit)
        {
            Followed = true;
        }
    }
    public void MouseUpFnc()
    {
        Followed = false;

        Isclicked = false;
        EditLine = false;

    }
    public void LineColor()
    {
        Gradient grad = new Gradient();
        float alpha = 1.0f;
        grad.SetKeys(
            new GradientColorKey[] { new GradientColorKey(Color.red, 0.0f), new GradientColorKey(Color.red, 1.0f) },
            new GradientAlphaKey[] { new GradientAlphaKey(alpha, 0.0f), new GradientAlphaKey(alpha, 1.0f) }
        );
        LineRend.colorGradient = grad;
    }
    public void ResetColor()
    {
        Gradient grad = new Gradient();
        float alpha = 1.0f;
        grad.SetKeys(
            new GradientColorKey[] { new GradientColorKey(Color.black, 0.0f), new GradientColorKey(Color.black, 1.0f) },
            new GradientAlphaKey[] { new GradientAlphaKey(alpha, 0.0f), new GradientAlphaKey(alpha, 1.0f) }
        );
        LineRend.colorGradient = grad;
    }




}
