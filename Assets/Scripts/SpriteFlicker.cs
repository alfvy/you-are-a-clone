using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// public class SpriteFlicker : MonoBehaviour
// {
//     public float flickerAmount = 3f;
//     public float flickerInterval = 0.001f;
//     private Color _originalColor;
//     private SpriteRenderer _thisSprite;
//     private SpriteRenderer[] _childrenSprites;
//     // Start is called before the first frame update
//     void Start()
//     {
//         _thisSprite = GetComponent<SpriteRenderer>();
//         if(_thisSprite)
//         {
//             _originalColor = _thisSprite.color;
//             StartCoroutine(Flicker(_thisSprite));
//         } else {
//             _childrenSprites = gameObject.GetComponentsInChildren<SpriteRenderer>();
//             _originalColor = _childrenSprites[0].color;
//             foreach(var sprite in _childrenSprites)
//             {
//                 StartCoroutine(Flicker(sprite));
//             }
//         }
//     }

//     private IEnumerator Flicker(SpriteRenderer sprite)
//     {
//         while(true)
//         {
//             sprite.color += new Color(flickerAmount, flickerAmount, flickerAmount);
//             yield return new WaitForSeconds(flickerInterval);
//             sprite.color = _originalColor;
//             yield return new WaitForSeconds(flickerAmount);
//         }
//     }
// }

public class SpriteFlicker : MonoBehaviour {
    public float minIntensity = 0f;
    public float maxIntensity = 1f;
    [Range(1, 50)]
    public int smoothing = 5;

    private Queue<float> smoothQueue;
    private float lastSum = 0;
    private Color _originalColor;
    private SpriteRenderer _sprite;
    private SpriteRenderer[] _childrenSprites;

    // public void Reset() {
    //     smoothQueue.Clear();
    //     lastSum = 0;
    // }

    void Start() {
        smoothQueue = new Queue<float>(smoothing);
        _sprite = GetComponent<SpriteRenderer>();
        // External or internal light?
        if (_sprite) _originalColor = _sprite.color;
        else if(!_sprite) {
            _childrenSprites = GetComponentsInChildren<SpriteRenderer>();
            _originalColor = _childrenSprites[0].color;
        }
    }

    void Reset()
    {
        smoothQueue = new Queue<float>(smoothing);
    }

    void Update()
    {
        if (_sprite)
        {
            // pop off an item if too big
            while (smoothQueue.Count >= smoothing) {
                lastSum -= smoothQueue.Dequeue();
            }

            // Generate random new item, calculate new average
            float newVal = Random.Range(minIntensity, maxIntensity);
            smoothQueue.Enqueue(newVal);
            lastSum += newVal;

            // Calculate new smoothed average
            float change = lastSum / (float)smoothQueue.Count;
            _sprite.color = new Color(_originalColor.r + change, _originalColor.g + change, _originalColor.b + change);
        } else if(!_sprite) {
            foreach(var sprite in _childrenSprites)
            {
                // pop off an item if too big
                while (smoothQueue.Count >= smoothing) {
                    lastSum -= smoothQueue.Dequeue();
                }

                // Generate random new item, calculate new average
                float newVal = Random.Range(minIntensity, maxIntensity);
                smoothQueue.Enqueue(newVal);
                lastSum += newVal;

                // Calculate new smoothed average
                float change = lastSum / (float)smoothQueue.Count;
                sprite.color = new Color(_originalColor.r + change, _originalColor.g + change, _originalColor.b + change);
            }
        }
    }
}
