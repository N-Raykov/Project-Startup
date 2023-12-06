using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Nut : Interactable
{
    [SerializeField] float rotationSpeed = 5f;
    [SerializeField] float tighteningSspeed = 0.5f;
    [SerializeField] float tighteningGoal = 5f;
    private float tighteningProgress;

    private PipeManager pipeManager;

    public int nutIndex { get; set; }

    private void Start()
    {
        pipeManager = FindObjectOfType<PipeManager>();
    }

    protected override void Update()
    {
        if (tighteningProgress < tighteningGoal)
        {
            base.Update();

            if (isSelected && Input.GetMouseButton(0))
            {
                RotateNut();
            }
        }
    }

    void RotateNut()
    {
        transform.Rotate(Vector3.forward * rotationSpeed * Time.deltaTime, Space.World);

        tighteningProgress = tighteningProgress + (tighteningSspeed * Time.deltaTime);

        if (tighteningProgress >= tighteningGoal)
        {
            isSelected = false;
            ToggleHighlight(false);
            pipeManager.NutTightened(nutIndex);
        }
    }
}
