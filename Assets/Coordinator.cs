using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Runtime.InteropServices;

[System.Serializable]
public class WebEvent
{
    public string tag;
};

[System.Serializable]
public class STransform
{
    public STransform(Quaternion rotation, Vector3 position)
    {
        this.rotation = rotation;
        this.position = position;
    }
    public Quaternion rotation;
    public Vector3 position;
}

[System.Serializable]
public class SceneData : WebEvent
{
    public List<string> names;
    public List<STransform> transforms;
}

[System.Serializable]
public class Disconnected : WebEvent { }

public class Coordinator : MonoBehaviour
{
    [DllImport("__Internal")]
    private static extern void Hello();

    [DllImport("__Internal")]
    private static extern void ConnectToServer();

    [DllImport("__Internal")]
    private static extern string GetData();

    [DllImport("__Internal")]
    private static extern void SendData(string data);

    [DllImport("__Internal")]
    private static extern void ConsoleLog(string s);

    public GameObject player;

    public GameObject playerModel;

    private Dictionary<string, GameObject> others =
        new Dictionary<string,GameObject>();

    float prevSentFrame;

    // Start is called before the first frame update
    void Start()
    {
        prevSentFrame = Time.time;

        ConnectToServer();

        /* REPLACE WITH PLAYER TRANSFORM
        transform.localPosition =
            new Vector3(
                Random.Range(-10.0f, 10.0f),
                0.0f,
                Random.Range(-10.0f, 10.0f)
            );

        SendData(JsonUtility.ToJson(new STransform
        (
            transform.rotation,
            transform.position
        )));
        */
    }

    void UpdateScene(SceneData sceneData)
    {
        for (int i = 0; i < sceneData.names.Count; ++i)
        {
            string name = sceneData.names[i];
            STransform stransform = sceneData.transforms[i];

            if (!others.ContainsKey(name))
            {
                others[name] = Instantiate(playerModel);
            }

            others[name].transform.position = stransform.position;
            others[name].transform.rotation = stransform.rotation;
        }

        foreach (var name in others.Keys)
        {
            if (!sceneData.names.Contains(name))
            {
                Destroy(others[name]);
                others.Remove(name);
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (Time.time - prevSentFrame < 0.1f)
            return;

        prevSentFrame = Time.time;

        // if (moved)
        {
            SendData(JsonUtility.ToJson(new STransform(
                    player.transform.rotation,
                    player.transform.position
                )));
        }

        string ev = GetData();
        WebEvent wev = JsonUtility.FromJson<WebEvent>(ev);
        if (wev.tag == "nothing") return;
        ConsoleLog(ev);
        ConsoleLog(wev.tag);

        if (wev.tag == "SceneData")
        {
            ConsoleLog("updating");
            ConsoleLog(ev);
            UpdateScene(JsonUtility.FromJson<SceneData>(ev));
        }
        else if (wev.tag == "Disconnected")
        {
            ConnectToServer();
        }
    }
}
