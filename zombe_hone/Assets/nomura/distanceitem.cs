using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class distanceitem : MonoBehaviour
{
    [SerializeField]
    private TextMeshProUGUI cardNameText;
    // Update is called once per frame
    public void Updatedistance(int distance)
    {
        cardNameText.text = distance.ToString();
    }
}
