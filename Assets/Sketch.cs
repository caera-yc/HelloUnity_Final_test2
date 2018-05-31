using UnityEngine;
using Pathfinding.Serialization.JsonFx; //make sure you include this using
using System.Collections;
using UnityEngine.Networking;

public class Sketch : MonoBehaviour {
    public GameObject myPrefab;
    string _WebsiteURL = "https://ylee998.azurewebsites.net/tables/animal?zumo-api-version=2.0.0";
    //string jsonResponse;



    void Start () {
        //Reguest.GET can be called passing in your ODATA url as a string in the form:
        //http://{Your Site Name}.azurewebsites.net/tables/{Your Table Name}?zumo-api-version=2.0.0
        //The response produce is a JSON string
        //old code string jsonResponse = Request.GET(_WebsiteURL);



        WWW myWww = new WWW(_WebsiteURL);
        while (myWww.isDone == false) ;
        //{ }
        string jsonResponse = myWww.text;

        //Just in case something went wrong with the request we check the reponse and exit if there is no response.
        if (string.IsNullOrEmpty(jsonResponse))
        {
            return;
        }

        //We can now deserialize into an array of objects - in this case the class we created. The deserializer is smart enough to instantiate all the classes and populate the variables based on column name.
        Animal[] animals = JsonReader.Deserialize<Animal[]>(jsonResponse);

        //----------------------
        //YOU WILL NEED TO DECLARE SOME VARIABLES HERE SIMILAR TO THE CREATIVE CODING TUTORIAL

        int i = 0;
        int totalCubes = 30;
        float totalDistance = 2.9f;
        //----------------------

        //We can now loop through the array of objects and access each object individually
        foreach (Animal animal in animals)
        {
            //Example of how to use the object
            Debug.Log("This activities type is: " + animal.ActivitiesType);
            //----------------------
            //YOUR CODE TO INSTANTIATE NEW PREFABS GOES HERE
            float perc = i / (float)totalCubes;
            float sin = Mathf.Sin(perc * Mathf.PI / 2);

            float x = 1.8f + sin * totalDistance;
            float y = 5.0f;
            float z = 0.0f;

            var newCube = (GameObject)Instantiate(myPrefab, new Vector3(x, y, z), Quaternion.identity);

            newCube.GetComponent<CubeScript>().SetSize(.45f * (1.0f - perc));
            newCube.GetComponent<CubeScript>().rotateSpeed = .2f + perc * 4.0f;
            newCube.transform.Find("New Text").GetComponent<TextMesh>().text = animal.ActivitiesType;//"Hullo Again";
            i++;

            //----------------------
        }
	}



    // Update is called once per frame
    void Update () {
	
	}

}
