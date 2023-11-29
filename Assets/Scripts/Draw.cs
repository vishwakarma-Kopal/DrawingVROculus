using Oculus.Interaction;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Draw : MonoBehaviour
{
    public HandVisual RightHand;
    public HandVisual LeftHand;

    public Transform RightIndexTip;
    public Transform LeftIndexTip;
    public Material DrawingMaterial;

    [Range(0.01f, 0.1f)]
    public float penWidth;

    [Range(0.001f, 0.05f)]
    public float precesion;

    int indexR = 0;
    int indexL = 0;

    LineRenderer LeftLineRenderer;
    LineRenderer RightLineRenderer;

    GameObject DrawingContainer;
    // Start is called before the first frame update
    void Start()
    {
        DrawingContainer = new GameObject("DrawingContainer");
    }

    // Update is called once per frame
    void Update()
    {
        if (RightHand.Hand.IsTrackedDataValid)
        {
            if (RightHand.Hand.GetIndexFingerIsPinching())
            {
                DrawRight(RightIndexTip);
            }
            else if (RightLineRenderer != null)
            {
                RightLineRenderer = null;
            }
        }
        if (LeftHand.Hand.IsTrackedDataValid)
        {
            if (LeftHand.Hand.GetIndexFingerIsPinching())
            {
                DrawLeft(LeftIndexTip);
            }
            else if (LeftLineRenderer != null)
            {
                LeftLineRenderer = null;
            }
        }
    }

    void DrawRight(Transform drawingPoint)
    {
        if (RightLineRenderer == null)
        {
            RightLineRenderer = new GameObject().AddComponent<LineRenderer>();
            RightLineRenderer.material = DrawingMaterial;
            RightLineRenderer.positionCount = 1;
            RightLineRenderer.startWidth = RightLineRenderer.endWidth = penWidth;
            RightLineRenderer.SetPosition(0, drawingPoint.position);
            indexR = 0;
            RightLineRenderer.transform.parent = DrawingContainer.transform;
        }
        else
        {
            if (Vector3.Distance(RightLineRenderer.GetPosition(indexR), drawingPoint.position) >= precesion)
            {
                indexR++;
                RightLineRenderer.positionCount++;
                RightLineRenderer.SetPosition(indexR, drawingPoint.position);
            }
        }

    }
    void DrawLeft(Transform drawingPoint)
    {
        if (LeftLineRenderer == null)
        {
            LeftLineRenderer = new GameObject().AddComponent<LineRenderer>();
            LeftLineRenderer.material = DrawingMaterial;
            LeftLineRenderer.positionCount = 1;
            LeftLineRenderer.startWidth = LeftLineRenderer.endWidth = penWidth;
            LeftLineRenderer.SetPosition(0, drawingPoint.position);
            indexL = 0;
            LeftLineRenderer.transform.parent = DrawingContainer.transform;
        }
        else
        {
            if (Vector3.Distance(LeftLineRenderer.GetPosition(indexL), drawingPoint.position) >= precesion)
            {
                indexL++;
                LeftLineRenderer.positionCount++;
                LeftLineRenderer.SetPosition(indexL, drawingPoint.position);
            }
        }
    }


    public void changeColor(Material material)
    {
        DrawingMaterial = material;
    }
    public void Undo()
    {
        Destroy((DrawingContainer.transform.GetChild(DrawingContainer.transform.childCount-1)).gameObject);
    }
    public void EraseAll()
    {
        Destroy(DrawingContainer);
        DrawingContainer = new GameObject("DrawingContainer");

    }
}
     
