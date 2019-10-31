using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ResourcePanel : MonoBehaviour
{
    public TextMeshProUGUI lumberText;
    public TextMeshProUGUI brickText;
    public TextMeshProUGUI grainText;
    public TextMeshProUGUI woolText;
    public TextMeshProUGUI oreText;

    public void UpdateResources(Dictionary<Resource,int> resources)
    {
        lumberText.text = resources[Resource.lumber].ToString();
        brickText.text = resources[Resource.brick].ToString();
        grainText.text = resources[Resource.grain].ToString();
        woolText.text = resources[Resource.wool].ToString();
        oreText.text = resources[Resource.ore].ToString();
    }
}
