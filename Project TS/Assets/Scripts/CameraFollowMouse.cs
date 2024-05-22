using System.Collections;
using UnityEngine;
using PrimeTween;
using UnityEngine.UI;

public class CameraFollowMouse : MonoBehaviour
{
    [SerializeField] private Image image;
    float mDelta = 150; // Pixels. The width border at the edge in which the movement work
    [SerializeField] private float mSpeed = 20.0f; // Scale. Speed of the movement
    float maxXPos = 8f;
    float maxYPos = 4.5f;
    [SerializeField] private TweenSettings<float> settingsX;
    [SerializeField] private TweenSettings<float> settingsY;
    public bool canMove;

    Tween tweenX;
    Tween tweenY;

    private void Start()
    {
        mSpeed += 100 * GlobalManager.speedLevel;
    }

    void Update()
    {
        if (canMove)
        {
            // Omars "change position part"
            // Check if on the right edge
            if (Input.mousePosition.x >= Screen.width - mDelta && transform.position.x < maxXPos)
            {
                if (tweenX.isAlive)
                {
                    if (tweenX.progress > 0.5f)
                    {
                        tweenX.Stop();
                        settingsX.startValue = transform.position.x;
                        settingsX.endValue =
                            Mathf.Min(transform.position.x + transform.right.x * Time.deltaTime * mSpeed, maxXPos);
                        tweenX = Tween.PositionX(transform, settingsX);
                    }
                }
                else
                {
                    settingsX.startValue = transform.position.x;
                    settingsX.endValue = Mathf.Min(transform.position.x + transform.right.x * Time.deltaTime * mSpeed,
                        maxXPos);
                    tweenX = Tween.PositionX(transform, settingsX);
                }

                // Move the camera

                // transform.position += transform.right * Time.deltaTime * mSpeed;
            }


            if (Input.mousePosition.x <= 0 + mDelta && transform.position.x > -maxXPos)
            {
                if (tweenX.isAlive)
                {
                    if (tweenX.progress > 0.5f)
                    {
                        tweenX.Stop();
                        settingsX.startValue = transform.position.x;
                        settingsX.endValue =
                            Mathf.Max(transform.position.x - transform.right.x * Time.deltaTime * mSpeed, -maxXPos);
                        tweenX = Tween.PositionX(transform, settingsX);
                    }
                }
                else
                {
                    settingsX.startValue = transform.position.x;
                    settingsX.endValue = Mathf.Max(transform.position.x - transform.right.x * Time.deltaTime * mSpeed,
                        -maxXPos);
                    tweenX = Tween.PositionX(transform, settingsX);
                }

                // Move the camera

                //transform.position -= transform.right * Time.deltaTime * mSpeed;
            }


            if (Input.mousePosition.y >= Screen.height - mDelta && transform.position.y < maxYPos)
            {
                if (tweenY.isAlive)
                {
                    if (tweenY.progress > 0.5f)
                    {
                        tweenY.Stop();
                        settingsY.startValue = transform.position.y;
                        settingsY.endValue = Mathf.Min(transform.position.y + transform.up.y * Time.deltaTime * mSpeed,
                            maxYPos);
                        tweenY = Tween.PositionY(transform, settingsY);
                    }
                }
                else
                {
                    settingsY.startValue = transform.position.y;
                    settingsY.endValue = Mathf.Min(transform.position.y + transform.up.y * Time.deltaTime * mSpeed,
                        maxYPos);
                    tweenY = Tween.PositionY(transform, settingsY);
                }

                // Move the camera

                //transform.position += transform.up * Time.deltaTime * mSpeed;
            }

            if (Input.mousePosition.y <= 0 + mDelta && transform.position.y > -maxYPos)
            {
                if (tweenY.isAlive)
                {
                    if (tweenY.progress > 0.5f)
                    {
                        tweenY.Stop();
                        settingsY.startValue = transform.position.y;
                        settingsY.endValue = Mathf.Max(transform.position.y - transform.up.y * Time.deltaTime * mSpeed,
                            -maxYPos);
                        tweenY = Tween.PositionY(transform, settingsY);
                    }
                }
                else
                {
                    settingsY.startValue = transform.position.y;
                    settingsY.endValue = Mathf.Max(transform.position.y - transform.up.y * Time.deltaTime * mSpeed,
                        -maxYPos);
                    tweenY = Tween.PositionY(transform, settingsY);
                }

                if (transform.position.y > maxYPos)
                {
                    transform.position = new Vector3(transform.position.x, maxYPos, transform.position.z);
                }

                if (transform.position.y < -maxYPos)
                {
                    transform.position = new Vector3(transform.position.x, -maxYPos, transform.position.z);
                }

                if (transform.position.x > maxXPos)
                {
                    transform.position = new Vector3(maxXPos, transform.position.y, transform.position.z);
                }

                if (transform.position.x < -maxXPos)
                {
                    transform.position = new Vector3(-maxXPos, transform.position.y, transform.position.z);
                }
                // Move the camera

                //transform.position -= transform.up * Time.deltaTime * mSpeed;
            }
        }
    }
}