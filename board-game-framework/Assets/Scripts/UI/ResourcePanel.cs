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

    public void UpdateResources(Resources resources)
    {
        lumberText.text = resources.lumber.ToString();
        brickText.text = resources.brick.ToString();
        grainText.text = resources.grain.ToString();
        woolText.text = resources.wool.ToString();
        oreText.text = resources.ore.ToString();
    }
}
