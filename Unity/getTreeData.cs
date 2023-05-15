using UnityEngine;
using System;
using Newtonsoft.Json;

public class TreeDataUI : MonoBehaviour
{
    private String backendURL="http://localhost:3000/treedata/"
    // private TreeData myTreeData;
    private IList<TreeData> treeDataList;

    void Start()
    {
        StartCoroutine(Download(result => {
            Debug.Log(result);
            JArray a = JArray.Parse(result);
            treeDataList = a.ToObject<IList<TreeData>>();
            foreach (TreeData tree in treeDataList) 
            {
                Debug.Log ($"Tree {tree.treeFamilyName} is size:{tree.treeSize} and colour:{tree.treeColour}");
            }
        }));
    }

    IEnumerator Download(System.Action<PlayerData> callback = null)
    {
        using (UnityWebRequest request = UnityWebRequest.Get(backendURL))
        {
            yield return request.SendWebRequest();

            if (request.isNetworkError || request.isHttpError)
            {
                Debug.Log(request.error);
                if (callback != null)
                {
                    callback.Invoke(null);
                }
            }
            else
            {
                if (callback != null)
                {
                    callback.Invoke(PlayerData.Parse(request.downloadHandler.text));
                }
            }
        }
    }
}

string json = @"{
  'd': [
    {
      'Name': 'John Smith'
    },
    {
      'Name': 'Mike Smith'
    }
  ]
}";

JObject o = JObject.Parse(json);

JArray a = (JArray)o["d"];

IList<Person> person = a.ToObject<IList<Person>>();

Console.WriteLine(person[0].Name);
// John Smith

Console.WriteLine(person[1].Name);
// Mike Smith




