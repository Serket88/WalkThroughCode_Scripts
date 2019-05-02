using UnityEngine;

enum TrackType { Straight, Curved }

public class SaveableObj : MonoBehaviour
{
    [SerializeField]
    private TrackType trackType;
    private string saveString;

    // Use this for initialization
    void Start ()
    {
        Debug.Log("Going to add instance to list...");
        SaveManager.Instance.saveableObjects.Add(this);
	}

    // save some object
    public void Save(int id)
    {
        Debug.Log("Going to save object " + id);
        Vector3 position = transform.position;
        Vector3 scale = transform.localScale;
        Quaternion rotation = transform.rotation;

        float posX = position.x;
        float posY = position.y;
        float posZ = position.z;

        float scaleX = scale.x;
        float scaleY = scale.y;
        float scaleZ = scale.z;

        float rotX = rotation.x;
        float rotY = rotation.y;
        float rotZ = rotation.z;
        float rotW = rotation.w;

        PlayerPrefs.SetString(id.ToString(), trackType + "_(" + posX.ToString() + "," + posY.ToString() +
                              "," + posZ.ToString() + ")_(" + scaleX.ToString() + "," + scaleY.ToString() +
                              "," + scaleZ.ToString() + ")_(" + rotX.ToString() + "," + rotY.ToString() +
                              "," + rotZ.ToString() + "," + rotW.ToString() + ")");

        Debug.Log("Information for object " + id + ": " + PlayerPrefs.GetString(id.ToString()));
    }

    // load some set of objects
    public void Load(string value)
    {

    }

    // destroy some object
    public void DestroyObject()
    {
        SaveManager.Instance.saveableObjects.Remove(this);
        Destroy(gameObject);
    }
}
