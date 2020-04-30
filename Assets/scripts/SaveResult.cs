using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SaveResult : MonoBehaviour
{
    public string expression;
    public string lat, lon, date;
    static int counter;
    private string[,] array;

    public static SaveResult Instance { get; set; }


    public void SaveData(string expr)
    {
        expression = expr;

        if (PlayerPrefs.HasKey("counter"))
        {
            counter = PlayerPrefs.GetInt("counter");
            counter += 1;
        }
        else
        {
            counter = 1;
            PlayerPrefs.SetInt("counter", counter);
        }

        lat = GPS.Instance.lat.ToString();
        lon = GPS.Instance.lon.ToString();
        date = DateTime.Now.ToString();

        PlayerPrefs.SetString("expression_" + counter, expression);
        PlayerPrefs.SetString("lat_" + counter, lat);
        PlayerPrefs.SetString("lon_" + counter, lon);
        PlayerPrefs.SetString("dateTime_" + counter, date);
        PlayerPrefs.SetInt("counter", counter);
        //GetResults();

        SaveDataToFB(expression, lat, lon, date);

        ReadResults();
    }

    public string[,] ReadResults()
    {
        if (PlayerPrefs.HasKey("counter"))
        {
            counter = PlayerPrefs.GetInt("counter");
        }

        array = new string[counter, 4];

        for (int a = 0; a < counter; a++)
        {
            int index = a + 1;
            array[a, 0] = PlayerPrefs.GetString("expression_" + index);
            array[a, 1] = PlayerPrefs.GetString("lat_" + index);
            array[a, 2] = PlayerPrefs.GetString("lon_" + index);
            array[a, 3] = PlayerPrefs.GetString("dateTime_" + index);
        }

        return array;
    }

    public int GetCount()
    {
        counter = array.Length / 4;
        return counter;
    }

    public string[,] GetResults()
    {
        return array;
    }

    public void SaveDataToFB(string expr, string lat, string lon, string date)
    {
        FirebaseClient.Instance.SaveDataToFB(expr, lat, lon, date);
    }

    private void Start()
    {
        Instance = this;
        ReadResults();
        DontDestroyOnLoad(gameObject);
    }
}
