using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Tags : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnMouseEnter()
    {
        Text tagsobj = GameObject.FindObjectOfType<LevelManager>().tags;
        tagsobj.enabled = true;
        tagsobj.text = gameObject.name;


    }

    private void OnMouseExit()
    {
        Text tagsobj = GameObject.FindObjectOfType<LevelManager>().tags;
        tagsobj.enabled = false;
    }
}
