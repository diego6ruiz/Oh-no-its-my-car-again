using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro; 

public class RC : MonoBehaviour
{
    int punt = 0;
    private bool isGrabbing = false;
    string activeTag = "";

    public GameObject FireExt, Engine, Seat, Muffler, Toolbox, Wheel, PaintSpray, Key, OnCarWheel, Interior, Lights, Fire, Exhaust1, Exhaust2, Car;
    public TextMeshProUGUI textFireExt, textMuffler, textSeat, textEngine, textToolbox, textWheel, textPaint, textKey;
    private Dictionary<string, GameObject> tagToGameObjectMap;
    public GameObject uiPanel;

    public int Score
    {
        get { return punt; }
        set { punt = value; }
    }

    // Start is called before the first frame update
    void Start()
    {
        tagToGameObjectMap = new Dictionary<string, GameObject>
        {
            {"FireExt", FireExt},
            {"Engine", Engine},
            {"Seat", Seat},
            {"Muffler", Muffler},
            {"Toolbox", Toolbox},
            {"Wheel", Wheel},
            {"PaintSpray", PaintSpray},
            {"Key", Key}
        };

        uiPanel.SetActive(false);
        
    }

    // Update is called once per frame
    void Update()
    {
        Ray camRay = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hitInfo;

        if (Physics.Raycast(camRay, out hitInfo) && Input.GetKeyDown(KeyCode.E))
        {
            string tag = hitInfo.collider.tag;
            
            if (tagToGameObjectMap.ContainsKey(tag) && !isGrabbing)
            {
                HandleGrab(tag, hitInfo.collider.gameObject);
                activeTag = tag;
            }

            if ((tag == "Car") && isGrabbing && tagToGameObjectMap.ContainsKey(activeTag))
            {
                HandlePlace(activeTag, hitInfo.collider.gameObject);
                activeTag = "";
            }
        }

        /* if (Input.GetKeyDown(KeyCode.G))
        {
            isGrabbing = false;
        } */

        if (Input.GetKeyDown(KeyCode.Q))
        {
            uiPanel.SetActive(!uiPanel.activeSelf);
        }

        /* if (Physics.Raycast(camRay, out hitInfo) && Input.GetKeyDown(KeyCode.E))
        {
            if (hitInfo.collider.CompareTag("Tortilla"))
            {
                GameObject.FindObjectOfType<AudioManager>().PlayGrab();
                Destroy(hitInfo.collider.gameObject);
                GameObject.FindObjectOfType<LevelManager>().activateWin();
                print("Felicidades, sobreviviste Villa Nueva");
            }
        } */
    }

    private void UpdateChecklist(string item, bool isComplete)
    {
        switch (item)
        {
            case "FireExt":
                textFireExt.text = isComplete ? "<s>1. Apagar el fuego </s>" : "1. Apagar el fuego ";
                break;
            case "Muffler":
                textMuffler.text = isComplete ? "<s>2. Cambiar el tubo de escape </s>" : "2. Cambiar el tubo de escape ";
                break;
            case "Seat":
                textSeat.text = isComplete ? "<s>3. Nuevos sillones </s>" : "3. Nuevos sillones ";
                break;
            case "Engine":
                textEngine.text = isComplete ? "<s>4. Colocar un motor nuevo </s>" : "4. Colocar un motor nuevo ";
                break;
            case "Toolbox":
                textToolbox.text = isComplete ? "<s>5. Reparar las luces delanteras </s>" : "5. Reparar las luces delanteras ";
                break;
            case "Wheel":
                textWheel.text = isComplete ? "<s>6. Cambiar llanta pinchada </s>" : "6. Cambiar llanta pinchada ";
                break;
            case "PaintSpray":
                textPaint.text = isComplete ? "<s>7. Pintura nueva </s>" : "7. Pintura nueva ";
                break;
            case "Key":
                textKey.text = isComplete ? "<s>8. Tras hacer todo, prender el carro </s>" : "8. Tras hacer todo, prender el carro ";
                break;
        }
    }


    private void HandleGrab(string tag, GameObject objectToDestroy)
    {
        if (tag == "Key" && punt < 7)
        {
            // Maybe play a sound or show a message indicating the key can't be used yet.
            Debug.Log("The key is not available yet.");
            return; // Exit the function if the key is not available.
        }
        GameObject.FindObjectOfType<AudioManager>().PlayGrab();
        Destroy(objectToDestroy);
        Text tagsobj = GameObject.FindObjectOfType<LevelManager>().tags;
        tagsobj.enabled = false;
        //punt++;
        isGrabbing = true;

        if (tagToGameObjectMap.TryGetValue(tag, out GameObject go))
        {
            go.SetActive(true);
        }
    }

    private void HandlePlace(string activeObj, GameObject objectToDeactivate)
    {
        GameObject.FindObjectOfType<AudioManager>().PlayGrab();
        GameObject go;
        if (tagToGameObjectMap.TryGetValue(activeObj, out go) && punt < 7)
        {
            go.SetActive(false);
            if(activeObj == "Engine")
            {
                GameObject.FindObjectOfType<AudioManager>().PlayEngine();
            }
            if(activeObj == "Wheel")
            {
                OnCarWheel.SetActive(true);
            }
            if(activeObj == "Seat")
            {
                Interior.SetActive(true);
            }
            if(activeObj == "Toolbox")
            {
                Lights.SetActive(true);
            }
            if(activeObj == "FireExt")
            {
                Destroy(Fire);
            }
            if (activeObj == "PaintSpray")
            {
                MeshRenderer carRenderer = Car.GetComponent<MeshRenderer>();
                if (activeObj == "PaintSpray")
                {
                    // Find the child object that has the MeshRenderer for the car body.
                    Transform carBodyTransform = Car.transform.Find("sport_car_1_body");
                    if (carBodyTransform != null)
                    {
                        MeshRenderer carBodyRenderer = carBodyTransform.GetComponent<MeshRenderer>();
                        if (carBodyRenderer != null)
                        {
                            // Assuming sport_car_1_body material is at index 0.
                            Material[] materials = carBodyRenderer.materials;
                            materials[1].color = Color.HSVToRGB(0, 0, 0.3f); //albedo 
                            materials[1].SetColor("_EmissionColor", Color.black); //emission


                            carBodyRenderer.materials = materials;
                        }
                        else
                        {
                            Debug.LogWarning("Car body does not have a MeshRenderer component.");
                        }
                    }
                    else
                    {
                        Debug.LogWarning("Car body object not found.");
                    }
                }
            }
            punt++;
            UpdateChecklist(activeObj, true);
        }
        else if (tagToGameObjectMap.TryGetValue(activeObj, out go) && punt >= 7)
        {
            if(activeObj == "Key")
            {
                go.SetActive(false);
                GameObject.FindObjectOfType<AudioManager>().PlayEngineStart();
            }

        }
        isGrabbing = false;
    }
}
