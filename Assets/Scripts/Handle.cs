using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Handle : MonoBehaviour
{

    public GameObject MouseImage;
    public GameObject PowerRageBoundary;
    public Camera mainCamera;
    // Start is called before the first frame update
    void Start()
    {
        Instantiate(PowerRageBoundary);
        Instantiate(MouseImage);
        PowerRageBoundary = GameObject.Find("boundary(Clone)");
        MouseImage = GameObject.Find("target(Clone)");
    }

    // Update is called once per frame
    void Update()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit))
        {
            MouseImage.transform.position = new Vector3(hit.point.x, 0.06f, hit.point.z);
        }
        //Cursor.visible = false; // Òþ²ØÊó±ê
        //Vector3 mousePosition = Input.mousePosition;
        //mousePosition.z = 0.5f;
        //Vector3 worldPosition = mainCamera.ScreenToWorldPoint(mousePosition);
        //MouseImage.transform.position = worldPosition;
    }

}
