using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Networking;
using Newtonsoft.Json;
using Unity.VisualScripting;

public class GameRegion : MonoBehaviour
{
    public TMP_Dropdown DropdownRegion;
    private List<Region.RegionData> regions;
    public static int selectedRegionId;
    public static string regionName;
    private void Start()
    {
        StartCoroutine(GetRegion());
        DropdownRegion.onValueChanged.AddListener(delegate { DropdownValueChanged(DropdownRegion); });
    }
    private IEnumerator GetRegion()
    {
        using (UnityWebRequest www = UnityWebRequest.Get("https://localhost:7038/api/APIGame/GetAllRegion"))
        {
            yield return www.SendWebRequest();
            if (www.error!=null)
            {
                Debug.Log(www.error);
            }
            else
            {
                string json = www.downloadHandler.text;
                Region.Response response = JsonConvert.DeserializeObject<Region.Response>(json);
                DropdownRegion.options.Clear();
                regions = new List<Region.RegionData>(response.data);
                foreach (Region.RegionData region in response.data)
                {
                    DropdownRegion.options.Add(new TMP_Dropdown.OptionData(region.name));                    
                }
                if (DropdownRegion.options.Count > 0)
                {
                    DropdownRegion.SetValueWithoutNotify(0);
                    DropdownRegion.RefreshShownValue();
                    DropdownValueChanged(DropdownRegion);
                }
            }
        }
    }
    public void DropdownValueChanged(TMP_Dropdown dropdown)
    {
        int index = dropdown.value;
        if (index<0||index >= regions.Count)
        {
            Debug.LogWarning("Invalid dropdow index selected.");
            return;
        }
        selectedRegionId = regions[index].regionId;
        regionName = regions[index].name;
        Debug.Log("Selected Region ID: "+selectedRegionId);
    }
}
