using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Stat : MonoBehaviour {

    private Image content;

    private float currentFill;

    [SerializeField]
    private float lerpSpeed;

    public float MyMaxValue { get; set; }

    public float MyCurrentValue //Property for accessing from other scripts
    {
        get
        {
            return currentValue;
        }

        set
        {
            if (value > MyMaxValue )
            {
                currentValue = MyMaxValue; //ensures health is not exceeded past the maximum value  
            }

            else if (value < 0)
            {
                currentValue = 0;
            }

            else
            {
                currentValue = value;
            }

            

            currentFill = currentValue / MyMaxValue; //Sets range between 0 and 1
        }
    }

    private float currentValue;

    

    // Use this for initialization
    void Start () {

        
        content = GetComponent<Image>(); //Reference to attached image aka Health Bar.
        Debug.Log(MyCurrentValue);
		
	}
	
	// Update is called once per frame
	void Update () {

        if (currentFill != content.fillAmount) //Smooth fill/depletion of bar.
        {
            content.fillAmount = Mathf.Lerp(content.fillAmount, currentFill, Time.deltaTime * lerpSpeed);
        }

        //content.fillAmount = currentFill;
	}

    public void Initialise(float currentValue, float maxValue)
    {
        MyMaxValue = maxValue;
        MyCurrentValue = currentValue;
    }
}
