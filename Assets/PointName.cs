using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class PointName : MonoBehaviour
{
    public TextMeshProUGUI PointText;
    // Start is called before the first frame update
    void Start()
    {
        PointText.text = "";
    }

    // Update is called once per frame
    void Update()
    {
        PointText.text = this.gameObject.name;
    }
}
