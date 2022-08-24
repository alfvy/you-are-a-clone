using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CloneManager : MonoBehaviour
{
    public List<Clone> clones {get; private set;}
    public Clone currentClone;

    // Start is called before the first frame update
    void Start()
    {
        clones = FindObjectsOfType<Clone>().ToList();
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.E))
        {
            currentClone = clones.FirstOrDefault(c => c.controlled);
            bool found = false;
            clones.ForEach(c => c.controlled = false);
            foreach(var clone in clones)
            {
                Vector3 heading = clone.transform.position - currentClone.transform.position;
		        if(AngleDir(transform.forward, heading, transform.up) == 1)
                {
                    currentClone = clone;
                    clone.controlled = true;
                    clone.rb.velocity = Vector2.zero;
                    found = true;
                    break;
                }
            }
            clones = clones.OrderBy(
            x => Vector2.Distance(this.transform.position, x.transform.position)
            ).ToList();
            if(!found) clones.Any(c=> c.controlled = true);
        }

        if(Input.GetKeyDown(KeyCode.Q))
        {
            currentClone = clones.FirstOrDefault(c => c.controlled);
            bool found = false;
            clones.ForEach(c => c.controlled = false);
            foreach(var clone in clones)
            {
                Vector3 heading = clone.transform.position - currentClone.transform.position;
		        if(AngleDir(transform.forward, heading, transform.up) == -1)
                {
                    currentClone = clone;
                    clone.controlled = true;
                    clone.rb.velocity = Vector2.zero;
                    found = true;
                    break;
                }
            }
            clones = clones.OrderBy(
            x => Vector2.Distance(this.transform.position, x.transform.position)
            ).ToList();
            if(!found) clones.Any(c=> c.controlled = true);
        }
    }

    float AngleDir(Vector3 fwd, Vector3 targetDir, Vector3 up) 
    {
		Vector3 perp = Vector3.Cross(fwd, targetDir);
		float dir = Vector3.Dot(perp, up);
		
		if (dir > 0f) {
			return 1f;
		} else if (dir < 0f) {
			return -1f;
		} else {
			return 0f;
		}
    }

    public void AddClone(GameObject newClone)
    {
        var clone = newClone.GetComponent<Clone>();
        clone.controlled = false;
        clones.Add(clone);
        clone.name = $"Clone {clones.Count - 1}";
        clone.text.text = (clones.Count - 1).ToString();
        clones = clones.OrderBy(
            x => Vector2.Distance(this.transform.position, x.transform.position)
            ).ToList();
    }
}
