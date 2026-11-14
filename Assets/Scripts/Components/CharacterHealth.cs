using System.Collections;
using System.Collections.Generic;
using System.Net;
using UnityEngine;
using UnityEngine.Networking;

public class CharacterHealth : MonoBehaviour
{
    public Settings settings;
    public ShootAbility shootAbility;
    private int _health = int.MaxValue;
    private bool _coroutineInProcess = false;
    public int visibleHealth;
    
    public int Health
    {
        get => _health;
        set
        {
            _health = value;
            WriteStatistics();
            if (_health <= 0) Destroy(gameObject);
        }
    }

    private void WriteStatistics()
    {
        var jsonString = JsonUtility.ToJson(shootAbility.stats);
        Debug.Log(jsonString);
        PlayerPrefs.SetString("Stats", jsonString);
    }

    private void Start()
    {
        Health = settings.HeroHealth;
        _coroutineInProcess = false;
        Debug.Log(JsonUtility.ToJson(settings));
    }

    private void Update()
    {
        if (_health < 20 && !_coroutineInProcess)
        {
            StartCoroutine(loadHealth());
        }
    }

    private void FixedUpdate()
    {
        visibleHealth = _health;
    }

    private IEnumerator loadHealth()
    {
        _coroutineInProcess = true;
        UnityWebRequest webRequest = UnityWebRequest.Get("https://raw.githubusercontent.com/bloodkael/DotsOld/refs/heads/master/RemoteData/UserData.json");
        yield return webRequest.SendWebRequest();

        if (webRequest.isDone == false)
        {
            Debug.Log(webRequest.error);
        }
        else
        {
            string jsonString = webRequest.downloadHandler.text;
            JsonUtility.FromJsonOverwrite(jsonString, settings);
            Health = settings.HeroHealth;
        }
        _coroutineInProcess = false;
    }
}
