using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class ModeSystem : MonoBehaviour
{
    
    public enum Mode {Edit,Create,Draw}
    public static Mode PMode;
    public GameObject Point;


    public GameObject PointName;
    public GameObject DijkstraUI;
    public InputField Name;
    public InputField EndVertex;

    public List<Node> AllNode;

    private Vector3 mousePos;
    private Vector3 mousePosGen;

    public GameObject DijkstraObj;
    // Start is called before the first frame update
    void Start()
    {
        PMode = Mode.Create;
    }

    // Update is called once per frame
    void Update()
    {
        mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        mousePosGen = new Vector3(mousePos.x, mousePos.y, 0);
        if (PMode == Mode.Create && Input.GetMouseButtonDown(0))
        {
            PointName.SetActive(true);
            
            Time.timeScale = 0f;
           
        }
        //if(PMode != Mode.Create)
        //{
        //    PointName.SetActive(false);
        //}
    }
    public void EditMode()
    {
        PMode = Mode.Edit;
        Debug.Log("EditMode");
    }
    public void CreateMode()
    {
        PMode = Mode.Create;
        Debug.Log("CreateMode");
    }
    public void DrawMode()
    {
        PMode = Mode.Draw;
        Debug.Log("DrawMode");
    }
    IEnumerator CreatePoint()
    {
        yield return Time.timeScale = 1f;
        string Pointname = Name.text;
        GameObject A = Instantiate(Point, mousePosGen, Quaternion.identity);
        A.name = Pointname;
        PointName.SetActive(false);
    }
    IEnumerator  CancelCreate()
    {
        PointName.SetActive(false);
        yield return Time.timeScale = 1f;
    }



    IEnumerator Analyze()
    {
        yield return Time.timeScale = 1f;
        DijkstraObj.GetComponent<Dijkstra>().FindShortestPath("A", EndVertex.text);
        DijkstraUI.SetActive(false);

    }
    IEnumerator CancelAnalyze()
    {
        DijkstraUI.SetActive(false);
        yield return Time.timeScale = 1f;
    }




    public void OkBut()
    {
        StartCoroutine(CreatePoint());
    }
    public void CancelBut()
    {
        StartCoroutine(CancelCreate());
    }
    public void GenerateEdgeList()
    {
        AllNode = new List<Node>();
        foreach (GameObject LDrawer in GameObject.FindGameObjectsWithTag("Collide"))
        {
            AllNode.Add(LDrawer.GetComponent<LineDrawer>().ThisNode);
            
        }
        DijkstraUI.SetActive(true);
    }
    public void ShortestPath()
    {

        StartCoroutine(Analyze());
    }
    public void CancelShortestPath()
    {

        StartCoroutine(CancelAnalyze());
    }
    public void Reset()
    {
        SceneManager.LoadScene("Main", LoadSceneMode.Single);
    }


}
