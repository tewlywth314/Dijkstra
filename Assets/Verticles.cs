using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Verticles
{
    // Start is called before the first frame update
    public string Vname;
    public int Status;
    public int Predecessor;
    public int PathDistance;
    
    public Verticles(string vname)
    {
        Vname = vname;
    }
}
