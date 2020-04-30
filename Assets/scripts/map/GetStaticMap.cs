using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GetStaticMap : MonoBehaviour
{
	public RawImage img;
	public GameObject mapScreen;

	string url;

	public float lat;
	public float lon;

	//LocationInfo li;

	public int zoom = 14;

	IEnumerator Map()
	{
		
		url = string.Format("https://static-maps.yandex.ru/1.x/?ll={0},{1}&size=650,450&z={2}&l=map&pt={0},{1},pm2dgl", lon,lat,zoom);
		WWW www = new WWW(url);
		yield return www;
		img.texture = www.texture;
		img.SetNativeSize();

	}
	// Use this for initialization
	void Start()
	{
		img = gameObject.GetComponent<RawImage>();
		//StartCoroutine(Map());
	}

	public void LoadMapFragment() {
		StartCoroutine(Map());
	}

	public void GetGPSPosition() {
		lat = GPS.Instance.lat;
		lon = GPS.Instance.lon;
	}

	public void ZoomPlus() {
		if (zoom < 18) {
			StopAllCoroutines();
			zoom++;
			LoadMapFragment();
		}
	}
	public void ZoomMinus()
	{
		if (zoom > 1)
		{
			StopAllCoroutines();
			zoom--;
			LoadMapFragment();
		}
	}

	

	void OnGUI()
	{
		if (Input.GetKeyUp(KeyCode.Escape))
		{
			mapScreen.SetActive(false);
		}
	}
}
