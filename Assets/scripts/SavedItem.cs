using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SavedItem : MonoBehaviour
{
    public Text text;
    public Text textShow;

    public Button buttonShow;
    public GameObject map;
    public GameObject mapScreen;

    public string lat, lon;
    public string expr, date;


    public void OpenMapWithMarker() {

        mapScreen.SetActive(true);
        map.GetComponent<GetStaticMap>().lat = float.Parse(lat);
        map.GetComponent<GetStaticMap>().lon = float.Parse(lon);
        map.GetComponent<GetStaticMap>().LoadMapFragment();
       
    }

    public void UpdateText() {
        text.text = string.Format("{0}\nLat: {1}\nLon: {2}\nDate: {3}\n", expr, lat, lon, date); 
    }
    private void Start()
    {
        if (GameObject.FindGameObjectWithTag("LangControl").GetComponent<LanguageController>().lang.Equals("en"))
        {
            textShow.text = "Show on map";
        }
        else
        {
            textShow.text = "Показать на карте";
        }
    }
}
