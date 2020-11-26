using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

[System.Serializable]
public class Node 
{
    public string Spoint;
    public string Epoint;
    public string Distance;
   
    public Node (string A,string B, string Dis)
    {
        
        Spoint = A;
        Epoint = B;
       
        Distance = Dis;
        

    }
    public Node()
    {
       
        Spoint = "";
        Epoint = "";
        Distance = "0";

        


    }

}

