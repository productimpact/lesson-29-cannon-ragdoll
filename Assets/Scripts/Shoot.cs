using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class Shoot : MonoBehaviour
{
    [SerializeField] Rigidbody hipRigidBody;

    [Range(1, 25)]
    [SerializeField] private float rotationSpeed;

    [SerializeField] private Slider angleSlider;
    [SerializeField] private Slider forceSlider;

    private bool rotateForward = true;
    private bool isFirstStage = true;
    private bool increasing = true;

    private float currentAngle = 0f;
    private float maxAngle = 40;


    private void Start()
    {
        Time.timeScale = 0;
    }

    void Update() 
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            angleSlider.gameObject.SetActive(false);
            forceSlider.gameObject.SetActive(true);

            if (!isFirstStage)
            {
                Throw(forceSlider.value);
                forceSlider.gameObject.SetActive(false);
            }

            isFirstStage = false;
        }
        if (isFirstStage)
        {
            // Check if we need to rotate forward or backward
            if (rotateForward)
            {
                // Rotate forward
                transform.Rotate(Vector3.right * Time.unscaledDeltaTime * rotationSpeed); // Adjust speed as needed
                currentAngle += Time.unscaledDeltaTime * rotationSpeed; // Adjust speed as needed

                // Check if we reached the maximum angle
                if (currentAngle >= maxAngle)
                {
                    rotateForward = false; // Change direction
                    currentAngle = maxAngle; // Make sure the current angle is set to the maximum
                }
            }
            else
            {
                // Rotate backward
                transform.Rotate(Vector3.left * Time.unscaledDeltaTime * rotationSpeed); // Adjust speed as needed
                currentAngle -= Time.unscaledDeltaTime * rotationSpeed; // Adjust speed as needed

                // Check if we returned to the starting angle
                if (currentAngle <= -maxAngle) // Invert the condition here
                {
                    rotateForward = true; // Change direction
                    currentAngle = -maxAngle; // Make sure the current angle is set to the negative maximum
                }
            }
            float unghiRotație = transform.rotation.eulerAngles.x;
            unghiRotație = (unghiRotație > 180) ? unghiRotație - 360 : unghiRotație;
            unghiRotație = Mathf.Clamp(unghiRotație, -40f, 40f);

            // Setează valoarea slider-ului în funcție de unghiul de rotație
            angleSlider.value = unghiRotație;
        }
        else
        {
            if (increasing)
            {
                forceSlider.value += (100 *  rotationSpeed) * Time.unscaledDeltaTime;

                if (forceSlider.value >= (forceSlider.maxValue))
                {
                    forceSlider.value = forceSlider.maxValue;
                    increasing = false;
                }
            }
            else
            {
                forceSlider.value -= (100 * rotationSpeed) * Time.unscaledDeltaTime;

                if (forceSlider.value <= forceSlider.minValue)
                {
                    forceSlider.value = forceSlider.minValue;
                    increasing = true;
                }
            }
        }
    }

    void Throw(float force)
    {
        hipRigidBody.AddForce(Vector3.forward * force, ForceMode.VelocityChange);
        Time.timeScale = 1f;
    }
}
