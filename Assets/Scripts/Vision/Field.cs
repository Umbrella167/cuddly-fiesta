using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Field : MonoBehaviour
{
    public GameObject field_plan;
    // Start is called before the first frame update
    void Start()
    {
        
    }
    
    // Update is called once per frame
    void Update()
    {
        
    }
    void create_field(SSL_WrapperPacket packet)
    {
        var eometry_msg = packet.Geometry;

    }
}
