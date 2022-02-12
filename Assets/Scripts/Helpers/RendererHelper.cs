using UnityEngine;

public class RendererHelper : MonoBehaviour
{
    private bool added;

    public virtual void Awake()
    {
        AddEvents(transform);
    }

    private void AddEvents(Transform transform)
    {
        if (!transform.GetComponent<Renderer>())
        {
            transform.GetChildren().BreakableForEach(() => added, child =>
            {
                AddEvents(child);
            });
        }
        else
        {
            if (transform != this.transform)
            {
                transform.gameObject.AddComponent<RendererEvents>().OnInvisible += OnBecameInvisible;

                added = true;
            }
        }
    }

    public virtual void OnBecameInvisible() { }
}