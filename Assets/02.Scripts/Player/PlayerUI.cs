using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class PlayerUI : MonoBehaviour
{
    #region singleton
    static public PlayerUI instance;
    private void Awake()
    {
        instance = this;
    }
    #endregion
    public Text energyText;
    public Slider energySlider;    
    
}
