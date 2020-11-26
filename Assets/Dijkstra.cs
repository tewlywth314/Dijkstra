using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
   

public class Dijkstra : MonoBehaviour
{
    // Start is called before the first frame update
    public List<Node> EdgesList;
    public List<Verticles> VerticlesList;
    

    private readonly int INFINITY = 10000;
    private readonly int TEMPORARY = 1;
    private readonly int PERMANENT= 2;
    private readonly int NIL = -1;

    public GameObject ModeSystemObj;
    public GameObject DistanceBox;
    public TextMeshProUGUI Distancetext;
    int[,] adj;

    public int CurrentVertex;
    private void Start()
    {
        
    }
    private void Update()
    {
        
    }
    public void SetUpList()
    {
        
        EdgesList = new List<Node>();
        EdgesList = ModeSystemObj.GetComponent<ModeSystem>().AllNode;
        VerticlesList = new List<Verticles>();
        foreach (GameObject vertexs in GameObject.FindGameObjectsWithTag("Vertex"))
        {
            VerticlesList.Add(new Verticles(vertexs.name));
        }
        Debug.Log(VerticlesList);
       

        adj = new int[VerticlesList.Count, VerticlesList.Count];
       
    }
    public void DoDijkstra(int s)
    {
        for (int i = 0; i < VerticlesList.Count; i++)
        {
            VerticlesList[i].Status = TEMPORARY;
            VerticlesList[i].Predecessor = NIL;
            VerticlesList[i].PathDistance = INFINITY;
        }
        VerticlesList[s].PathDistance = 0;
        while (true)
        {
            CurrentVertex = MinPathDistance();
            if (CurrentVertex == NIL)
            {
                return;
            }
            VerticlesList[CurrentVertex].Status = PERMANENT;

            for (int i = 0; i < VerticlesList.Count; i++)
            {
                if (IsConnected(CurrentVertex, i) && VerticlesList[i].Status == TEMPORARY)
                {
                    if (VerticlesList[CurrentVertex].PathDistance + adj[CurrentVertex, i] < VerticlesList[i].PathDistance)
                    {
                        VerticlesList[i].Predecessor = CurrentVertex;
                        VerticlesList[i].PathDistance = VerticlesList[CurrentVertex].PathDistance + adj[CurrentVertex, i];
                    }
                }
            }
        }
    }
    public int MinPathDistance()
    {
        int min = INFINITY;
        int Pc = NIL;
        for(int i = 0; i< VerticlesList.Count; i++)
        {
            if(VerticlesList[i].Status == TEMPORARY && VerticlesList[i].PathDistance < min)
            {
                min = VerticlesList[i].PathDistance;
                Pc = i;
            }
        }
        return Pc;
    }
    public bool IsConnected(int c, int v)
    {
        foreach(Node Edge in EdgesList)
        {
            if(Edge.Spoint == VerticlesList[c].Vname && Edge.Epoint == VerticlesList[v].Vname)
            {
                adj[c, v] = int.Parse(Edge.Distance);
                return true;
            }
        }
        return false;
    }
    public void FindShortestPath(string StartPath, string EndPath)
    {
        SetUpList();
        ResetLineColor();
        int S = GetIndex(StartPath);
        int E = GetIndex(EndPath);
        int i, u;
        int[] path = new int[VerticlesList.Count+1];
        int Distance = 0;
        int Count = 0;
        string Ans = "";
        DoDijkstra(S);
        while(E != S)
        {
            Count++;
            path[Count] = E;
            u = VerticlesList[E].Predecessor;
            Distance += adj[u, E];
            E = u;
            
        }
        Count++;
        path[Count] = S;

        for(i = Count;i >= 1; i--)
        {

            Ans += VerticlesList[path[i]].Vname + ",";
            ColoredLine(i, path);

        }
        DistanceBox.SetActive(true);
        Distancetext.text = string.Format("({0}) = {1}", Ans, Distance);
        Debug.Log(Ans);
        Debug.Log("Distance is : " + Distance);

    }
    public int GetIndex(string Vxname)
    {
        for(int i = 0; i < VerticlesList.Count; i++)
        {
            if(Vxname == VerticlesList[i].Vname)
            {
                return i;
            }
        }
        throw new System.InvalidOperationException("Invalid Vertex");
    }
    public void ColoredLine(int i,int[] path)
    {
        foreach(GameObject LineDrawer in GameObject.FindGameObjectsWithTag("Collide"))
        {
            if(LineDrawer.GetComponent<LineDrawer>().ThisNode.Spoint == VerticlesList[path[i]].Vname && LineDrawer.GetComponent<LineDrawer>().ThisNode.Epoint == VerticlesList[path[i - 1]].Vname)
            {
                LineDrawer.GetComponent<LineDrawer>().LineColor();
                if(i-1 < 0)
                {
                    break;
                }
            }
        }
    }
    public void ResetLineColor()
    {
        foreach (GameObject LineDrawer in GameObject.FindGameObjectsWithTag("Collide"))
        {
            LineDrawer.GetComponent<LineDrawer>().ResetColor();
        }
    }


}
