using UnityEngine;
using ModernWestern;

public class RendererHelper : MonoBehaviour
{
    private bool added;

    public virtual void Awake()
    {
        SetEvents(transform);
    }

    private void SetEvents(Transform transform)
    {
        if (!transform.GetComponent<Renderer>())
        {
            transform.GetChildren().BreakableForEach(() => added, SetEvents);
        }
        else
        {
            if (transform == this.transform)
            {
                return;
            }

            transform.gameObject.AddComponent<RendererEvents>().OnInvisible += OnBecameInvisible;

            transform.gameObject.AddComponent<RendererEvents>().OnVisible += OnBecameVisible;

            added = true;
        }
    }

    public virtual void OnBecameInvisible(){}

    public virtual void OnBecameVisible(){}
}