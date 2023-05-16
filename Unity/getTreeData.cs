using UnityEngine;
using System;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;


public class TreeDataUI : MonoBehaviour
{
    private String backendURL="https://foi23-if-i-were-a-tree.glitch.me/treedata";
    // private String backendURL="http://localhost:3000/treedata/"
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

    IEnumerator Download(System.Action<TreeData> callback = null)
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
                    callback.Invoke(TreeData.Parse(request.downloadHandler.text));
                }
            }
        }
    }
}







