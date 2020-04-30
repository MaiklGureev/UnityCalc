using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuSwitcher : MonoBehaviour
{
    public GameObject mainMenu, calcScreen, devScreen, mapScreen, localDBScreen, serverDBScreen;
    public GameObject imageMapScreen,map;
    public void MenuSelecter(string selectedMenu)
    {
        switch (selectedMenu)
        {
            case "main":
                {
                    mainMenu.SetActive(true);
                    calcScreen.SetActive(false);
                    devScreen.SetActive(false);
                    localDBScreen.SetActive(false);
                    serverDBScreen.SetActive(false);
                    mapScreen.SetActive(false);
                    break;
                }

            case "calc":
                {
                    mainMenu.SetActive(false);
                    calcScreen.SetActive(true);
                    devScreen.SetActive(false);
                    localDBScreen.SetActive(false);
                    serverDBScreen.SetActive(false);
                    mapScreen.SetActive(false);
                    break;
                }

            case "dev":
                {
                    mainMenu.SetActive(false);
                    calcScreen.SetActive(false);
                    devScreen.SetActive(true);
                    localDBScreen.SetActive(false);
                    serverDBScreen.SetActive(false);
                    mapScreen.SetActive(false);
                    break;
                }

            case "map":
                {
                    mainMenu.SetActive(false);
                    calcScreen.SetActive(false);
                    devScreen.SetActive(false);
                    localDBScreen.SetActive(false);
                    serverDBScreen.SetActive(false);
                    mapScreen.SetActive(true);
                    break;
                }
            case "localDB":
                {
                    mainMenu.SetActive(false);
                    calcScreen.SetActive(false);
                    devScreen.SetActive(false);
                    localDBScreen.SetActive(true);
                    localDBScreen.GetComponent<UpdateItems>().LoadItems();
                    serverDBScreen.SetActive(false);
                    mapScreen.SetActive(false);
                    break;
                }
            case "serverDB":
                {
                    mainMenu.SetActive(false);
                    calcScreen.SetActive(false);
                    devScreen.SetActive(false);
                    localDBScreen.SetActive(false);
                    serverDBScreen.SetActive(true);
                    serverDBScreen.GetComponent<UpdateItems>().LoadItemsFB();
                    mapScreen.SetActive(false);
                    break;
                }
            case "openMap":
                {
                    //latAndLonTextMapScreen.SetActive(false);
                    //buttonMapScreen.SetActive(false);
                    imageMapScreen.SetActive(true);
                    map.GetComponent<GetStaticMap>().GetGPSPosition();
                    map.GetComponent<GetStaticMap>().LoadMapFragment();
                    break;
                }
            case "exit":
                {
                    Application.Quit();
                    break;
                }
        }

        
    }

    public void GoBack() {

        if (!localDBScreen.active) {
            localDBScreen.GetComponent<UpdateItems>().DestroyItems();
            serverDBScreen.GetComponent<UpdateItems>().DestroyItems();
        }

            if (calcScreen.active) {
            calcScreen.SetActive(false);
            mainMenu.SetActive(true);
        }
        else if (devScreen.active)
        {
            devScreen.SetActive(false);
            mainMenu.SetActive(true);
        }
        else if (mapScreen.active)
        {
            mapScreen.SetActive(false);
            mainMenu.SetActive(true);
        }
        else if (localDBScreen.active)
        {
            localDBScreen.SetActive(false);
            mainMenu.SetActive(true);
        }
        else if (serverDBScreen.active)
        {
            serverDBScreen.SetActive(false);
            mainMenu.SetActive(true);
        }
        
    }

    void OnGUI()
    {
        if (Input.GetKeyUp(KeyCode.Escape))
        {
            GoBack();
        }
    }

    private void Start()
    {

    }


}
