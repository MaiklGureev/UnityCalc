using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GPS : MonoBehaviour
{
    public static GPS Instance { get; set; }
    public float lat = -1, lon = -1;
    public string status;
    public Text coordinate;

    // Start is called before the first frame update
    void Start()
    {
        Instance = this;
        Input.location.Start();
        //status = "Location start";
        //Debug.Log(status);
        DontDestroyOnLoad(gameObject);

    }

    
    private void Update()
    {
        UpdateLocation();
        coordinate.text = string.Format("Lat: {0} \n Lon: {1} ",
        lat.ToString(), lon.ToString());

    }

    public void UpdateLocation()
    {
        if (Input.location.isEnabledByUser)
        {
            lat = Input.location.lastData.latitude;
            lon = Input.location.lastData.longitude;
            //status = "Search...";
            //Debug.Log(status);

        }
        else
        {
            //status = "User has not enabled GPS";
            //Debug.Log(status);
        }
    }

}
