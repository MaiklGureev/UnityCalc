using Firebase;
using Firebase.Database;
using Firebase.Unity.Editor;
using System;
using System.Collections.Generic;
using UnityEngine;

public class FirebaseClient : MonoBehaviour
{
    private  DatabaseReference reference;
    private  string[,] result;

    public static FirebaseClient Instance { get; set; }

    void Start()
    {
        Instance = this;
        DontDestroyOnLoad(gameObject);
        InitializeFirebase();
    }

   

    private void InitializeFirebase()
    {
        //FirebaseApp.DefaultInstance.SetEditorDatabaseUrl("https://unitycalc.firebaseio.com/");
        reference = FirebaseDatabase.DefaultInstance.RootReference;
        ReadResult();
    }

    public void SaveDataToFB(string expr, string lat, string lon, string date)
    {
        if (reference != null)
        {
            SaveResultFB saveResultFB = new SaveResultFB(expr, lat, lon, date);
            string json = JsonUtility.ToJson(saveResultFB);
            reference.Push().SetRawJsonValueAsync(json);
            //reference.Child("main_table").Child(json.GetHashCode().ToString()).SetRawJsonValueAsync(json);
        }
        else
        {
            Debug.LogError("reference does not instance");
        }

        ReadResult();
    }

    public void ReadResult()
    {
        reference.GetValueAsync().ContinueWith(task =>
         {
             if (task.IsFaulted)
             {
                 // Handle the error...
             }
             else if (task.IsCompleted)
             {
                 DataSnapshot snapshot = task.Result;
                 result = new string[snapshot.ChildrenCount, 4];

                 int a = 0, b = 0;
                 foreach (var child in snapshot.Children)
                 {
                     b = 0;
                     foreach (var i in child.Children)
                     {
                         result[a, b] = i.Value.ToString();
                         b++;
                         //Debug.Log(i.Value);
                     }
                     a++;
                 }
             }
         });
    }

    public string[,] GetResult() {
        return result;
    }



    [Serializable]
    public class SaveResultFB
    {
        public string expr;
        public string lat;
        public string lon;
        public string date;

        public SaveResultFB()
        {
        }

        public SaveResultFB(string expr, string lat, string lon, string date)
        {
            this.Expr = expr;
            this.Lat = lat;
            this.Lon = lon;
            this.Date = date;
        }

        public string Date { get => date; set => date = value; }
        public string Lon { get => lon; set => lon = value; }
        public string Lat { get => lat; set => lat = value; }
        public string Expr { get => expr; set => expr = value; }
    }

}
