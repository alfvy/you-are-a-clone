using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParralaxTrigger : MonoBehaviour
{
    public List<SpriteRenderer> parralaxObjects;
    public SpriteRenderer greenFeild;

    public float parralaxScale, distance;

    void Update()
    {
        if(!GameManager.CloneManager.currentClone) return;
        distance = GameManager.CloneManager.currentClone.transform.position.x - transform.position.x;
        parralaxScale = 1 - (distance / 20);
        // if the player is to the right of this object dim the green feild and brighten the parralax objects
        // based on distance from the player
        greenFeild.color = new Color(greenFeild.color.r, greenFeild.color.g, greenFeild.color.b, parralaxScale);
        foreach (var parralaxObject in parralaxObjects)
        {
            parralaxObject.color = new Color(parralaxObject.color.r, parralaxObject.color.g, parralaxObject.color.b, 1 - parralaxScale);
        }
    }
}
