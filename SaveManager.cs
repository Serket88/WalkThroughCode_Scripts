using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SaveManager : MonoBehaviour
{
    private static bool loadStuff = false;
    private static SaveManager instance;
    public static SaveManager Instance
    {
        get
        {
            if (instance == null)
                instance = GameManager.FindObjectOfType<SaveManager>();

            return instance;
        }
    }

    public GameObject straightModel;
    public GameObject curvedModel;

    public List<SaveableObj> saveableObjects { get; private set; }

    private void Awake()
    {
        Debug.Log("New list screated");
        saveableObjects = new List<SaveableObj>();

        if (loadStuff)
        {
            Debug.Log("GOING TO LOAD STUFF");
            loadStuff = false;
            int numObjects = PlayerPrefs.GetInt("numObjects");
            Debug.Log("Going to load " + numObjects + " objects");
            string infoString = null;
            string trackType = null;
            string[] values;
            Vector3 positionVec;
            Vector3 scaleVec;
            Quaternion rotationQuat;
            GameObject tempTrack = null;

            for (int i = 0; i < numObjects; i++)
            {
                infoString = PlayerPrefs.GetString(i.ToString());
                values = infoString.Split('_');
                trackType = values[0];
                positionVec = StringToVec(values[1]);
                scaleVec = StringToVec(values[2]);
                rotationQuat = StringToQuat(values[3]);

                Debug.Log("track type: " + trackType);
                Debug.Log("position vec: " + positionVec);
                Debug.Log("scale vec: " + scaleVec);
                Debug.Log("rotation: " + rotationQuat);

                if (trackType == "Straight")
                    tempTrack = Instantiate(straightModel, positionVec, rotationQuat);

                else if (trackType == "Curved")
                    tempTrack = Instantiate(curvedModel, positionVec, rotationQuat);

                tempTrack.transform.localScale = scaleVec;
            }
        }
    }

    public void Save()
    {
        Debug.Log("Going to save " + saveableObjects.Count + " objects");
        PlayerPrefs.SetInt("numObjects", saveableObjects.Count);

        for (int i = 0; i < saveableObjects.Count; i++)
            saveableObjects[i].Save(i);

        Debug.Log("All object have been saved...");
    }

    public void Load()
    {
        Debug.Log("Going to load stuff and change scenes");
        loadStuff = true;
        SceneManager.LoadScene(2);
    }

    public Vector3 StringToVec(string positionString)
    {
        positionString = positionString.Substring(1, positionString.Length - 2);
        positionString = positionString.Replace(" ", "");
        string[] values = positionString.Split(',');

        float x = float.Parse(values[0]);
        float y = float.Parse(values[1]);
        float z = float.Parse(values[2]);

        return new Vector3(x, y, z);
    }

    public Quaternion StringToQuat(string quaternionString)
    {
        quaternionString = quaternionString.Substring(1, quaternionString.Length - 2);
        quaternionString = quaternionString.Replace(" ", "");
        string[] values = quaternionString.Split(',');

        float x = float.Parse(values[0]);
        float y = float.Parse(values[1]);
        float z = float.Parse(values[2]);
        float w = float.Parse(values[3]);

        return new Quaternion(x, y, z, w);
    }

    public void Destroy()
    {
        while (saveableObjects.Count > 0)
            saveableObjects[0].DestroyObject();
    }
}
