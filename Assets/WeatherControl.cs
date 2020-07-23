
using UnityEngine;
using System.Collections;
using SimpleJSON;
using UnityEngine.Networking;
using UnityEngine.UI;
using System.Threading;


public class WeatherControl : MonoBehaviour
{
    private ParticleSystem particules;
    private string myWeatherLabel;
    private string currentIP;
    private string currentCity;
    private string conditionName = "";
    [SerializeField]
    private Light weatherLight;

    void Start()
    {
        particules = GetComponent<ParticleSystem>();
        StartCoroutine(SendRequest());

    }

    private void Update()
    {
        if (conditionName != "")
        {
            if (conditionName.Equals("scattered clouds"))
            {
                weatherLight.color = (Color.white / 2) + Color.black;
            }
            else if (conditionName.Equals("overcast clouds"))
            {
                weatherLight.color = (Color.white / 4) + Color.black;

            }
            else if (conditionName.Equals("light rain") && !particules.isPlaying)
            {
                var emission = particules.emission;
                emission.rateOverTime = 40f;
                weatherLight.color = (Color.white / 2) + Color.black;
                particules.Play();
            }
            else if (conditionName.Equals("moderate rain") && !particules.isPlaying)
            {
                var emission = particules.emission;
                emission.rateOverTime = 80f;
                weatherLight.color = (Color.white / 4) + Color.black;
                particules.Play();
            }
            else if (conditionName.Equals("heavy rain") && !particules.isPlaying)
            {

                var emission = particules.emission;
                emission.rateOverTime = 120f;
                weatherLight.color = Color.black;
                particules.Play();
            }
            else if (!conditionName.Equals("light rain") && !conditionName.Equals("moderate rain") && !conditionName.Equals("heavy rain") && particules.isPlaying)
            {
                particules.Stop();
            }
        }
    }

    IEnumerator SendRequest()
    {
        currentIP = NetworkManager.singleton.networkAddress;

        WWW cityRequest = new WWW("http://www.geoplugin.net/json.gp?ip=" + currentIP);
        yield return cityRequest;

        if (cityRequest.error == null || cityRequest.error == "")
        {
            var N = JSON.Parse(cityRequest.text);
            currentCity = N["geoplugin_city"].Value;

            Debug.Log("city = " + currentCity);
        }

        else
        {
            Debug.Log("WWW error: " + cityRequest.error);
        }

        WWW request = new WWW("http://api.openweathermap.org/data/2.5/weather?q=" + "Paris" + "&APPID=0f8932fd51ffda81066f9e73ca2eb896");
        yield return request;

        if (request.error == null || request.error == "")
        {
            var N = JSON.Parse(request.text);


            string temp = N["main"]["temp"].Value;
            temp = temp.Replace('.', ',');
            float finalTemp = Mathf.Round((float.Parse(temp) - 273.0f) * 10) / 10;
            conditionName = N["weather"][0]["description"].Value;

            myWeatherLabel =
                " Temperature: " + finalTemp + " C"
                + " Current Condition: " + conditionName;

            Debug.Log("Weather = " + myWeatherLabel);
        }
        else
        {
            Debug.Log("WWW error: " + request.error);
        }

    }
}
