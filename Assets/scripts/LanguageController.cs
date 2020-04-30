using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LanguageController : MonoBehaviour
{

    public Text menuCalc;
    public Text menuLocalDB;
    public Text menuServerDB;
    public Text menuMap;
    public Text menuDev;
    public Text menuExit;
    public Text menuCurLang;

    public Text menuCalcSaveRes;
    public Text menuDevTitle;
    public Text menuDev1;
    public Text menuDev2;
    public Text menuDev3;

    public Text menuMapShowOnMap;
    //public Text menuDev3;
    //public Text menuDev3; 
    //public Text menuDev3;

    public string lang;

    // Start is called before the first frame update
    void Start()
    {
        LoadCurLang();
        UpdateInterface();
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void SwitchLang() {
        if (lang.Equals("en"))
        {
            lang = "ru";
            UpdateInterface();
            SaveCurLang();
        }
        else {
            lang = "en";
            UpdateInterface();
            SaveCurLang();
        }
    }

    private void UpdateInterface()
    {
        if (lang.Equals("en"))
        {
            menuCalc.text = "Calculator";
            menuLocalDB.text = "Local DB";
            menuServerDB.text = "Server DB";
            menuMap.text = "Map";
            menuDev.text = "Developers";
            menuExit.text = "Exit";
            menuCurLang.text = "Русский";
            menuCalcSaveRes.text = "Save result";
            menuDevTitle.text = "Developers";
            menuDev1.text = "Mikhail Gureev";
            menuDev2.text = "Alex Elfimov";
            menuDev3.text = "Vlad Philatov";
            menuMapShowOnMap.text = "Show on map";
        }
        else {
            menuCalc.text = "Калькулятор";
            menuLocalDB.text = "Локальная БД";
            menuServerDB.text = "Серверная БД";
            menuMap.text = "Карты";
            menuDev.text = "Разработчики";
            menuExit.text = "Выйти";
            menuCurLang.text = "English";
            menuCalcSaveRes.text = "Сохранить результат";
            menuDevTitle.text = "Разработчики";
            menuDev1.text = "Михаил Гуреев";
            menuDev2.text = "Алексей Елфимов";
            menuDev3.text = "Влад Филатов";
            menuMapShowOnMap.text = "Показать на карте";
        }

    }

    private void LoadCurLang()
    {
        if (PlayerPrefs.HasKey("lang"))
        {
            lang = PlayerPrefs.GetString("lang");
            
        }
        else
        {
            lang = "en";
            PlayerPrefs.SetString("lang", lang);
        }
    }

    private void SaveCurLang()
    {
        PlayerPrefs.SetString("lang", lang);
    }
}
