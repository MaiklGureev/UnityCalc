using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UpdateItems : MonoBehaviour
{
    public GameObject content;
    public GameObject prefab;
    public GameObject mapScreen;
    public GameObject map;

    public void LoadItems() {
        
        string[,] result = SaveResult.Instance.GetResults();
        int count = result.Length/4;
        for (int a = count-1; a >= 0; a--)
        {
            var instance = GameObject.Instantiate(prefab.gameObject) as GameObject;
            instance.transform.SetParent(content.transform, false);
            instance.GetComponent<SavedItem>().expr = result[a, 0];
            instance.GetComponent<SavedItem>().lat = result[a, 1];
            instance.GetComponent<SavedItem>().lon = result[a, 2];
            instance.GetComponent<SavedItem>().date = result[a, 3];
            instance.GetComponent<SavedItem>().map = map;
            instance.GetComponent<SavedItem>().mapScreen = mapScreen;
            instance.GetComponent<SavedItem>().UpdateText();
        }
    }

    public void LoadItemsFB()
    {
        string[,] result = FirebaseClient.Instance.GetResult();
        int count = result.Length/4;
        for (int a = count-1; a >= 0; a--)
        {
            var instance = GameObject.Instantiate(prefab.gameObject) as GameObject;
            instance.transform.SetParent(content.transform, false);

            instance.GetComponent<SavedItem>().date = result[a, 0];
            instance.GetComponent<SavedItem>().expr = result[a, 1];
            instance.GetComponent<SavedItem>().lat = result[a, 2];
            instance.GetComponent<SavedItem>().lon = result[a, 3];
            
            instance.GetComponent<SavedItem>().map = map;
            instance.GetComponent<SavedItem>().mapScreen = mapScreen;
            instance.GetComponent<SavedItem>().UpdateText();
        }
    }

    public void DestroyItems() {
        if (content.transform.childCount!=0) {
            for (int a = 0; a < content.transform.childCount; a++) {
                Destroy(content.transform.GetChild(a).gameObject);
            } 
        }
    }

    // Update is called once per frame
    void Update()
    {

    }


}
