using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// カメラを制御するクラス
/// </summary>
public class CameraController : MonoBehaviour
{
    [SerializeField]
    private PlayerInput targetPlayer;
    private Vector3 targetPos;

    /// <summary>
    /// マウス水平感度
    /// </summary>
    private float mouseXSpeed = 2f;
    /// <summary>
    /// マウス垂直感度
    /// </summary>
    private float mouseYSpeed = 1.5f;
    /// <summary>
    /// コントローラ水平感度
    /// </summary>
    private float rotationHorizontalSpeed = 300f;
    /// <summary>
    /// コントローラ垂直感度
    /// </summary>
    private float rotationVerticalSpeed = 200f;

    const float UPPER_LIMIT = 320f;
    const float LOWER_LIMIT = 70f;

    private float cameraY;
    private float distance;
    private float initialDistance;

    // Use this for initialization
    void Start ()
    {
        targetPos = targetPlayer.transform.position;

        // マウス非表示
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;

        cameraY = transform.position.y;
        distance = Vector3.Distance(transform.position, targetPlayer.transform.position + targetPlayer.transform.up * cameraY);
        initialDistance = distance;
    }
	
	// Update is called once per frame
	void Update ()
    {
        // カメラ回転
        RotateCamera();
        // カメラ移動
        MoveCamera();
    }

    /// <summary>
    /// カメラ回転
    /// </summary>
    void RotateCamera()
    {
        // ↓PlayerInputから入力するように修正予定↓
        float mouseX = Input.GetAxis("Mouse X");
        float mouseY = Input.GetAxis("Mouse Y");
        float rotationHorizontal = Input.GetAxisRaw("RotationHorizontal");
        float rotationVertical = Input.GetAxisRaw("RotationVertical");

        if(transform.localEulerAngles.x < UPPER_LIMIT && transform.localEulerAngles.x > 180f)
        {
            //Debug.Log("上限");
            if (mouseY > 0) { mouseY = 0; }
            if (rotationVertical < 0) { rotationVertical = 0; }
        }
        else if (transform.eulerAngles.x > LOWER_LIMIT && transform.localEulerAngles.x < 180f)
        {
            //Debug.Log("下限");
            if (mouseY < 0) { mouseY = 0; }
            if (rotationVertical > 0) { rotationVertical = 0; }
        }

        transform.RotateAround(
            targetPos + targetPlayer.transform.up * cameraY,
            Vector3.up,
            mouseX * mouseXSpeed);
        transform.RotateAround(
            targetPos + targetPlayer.transform.up * cameraY,
            transform.right,
            -mouseY * mouseYSpeed);

        transform.RotateAround(
            targetPos + targetPlayer.transform.up * cameraY,
            Vector3.up,
            rotationHorizontal * rotationHorizontalSpeed * Time.deltaTime);
        transform.RotateAround(
            targetPos + targetPlayer.transform.up * cameraY,
            transform.right,
            rotationVertical * rotationVerticalSpeed * Time.deltaTime);
    }

    /// <summary>
    /// カメラ移動
    /// </summary>
    void MoveCamera()
    {
        RaycastHit hit;

        Vector3 test = targetPlayer.transform.position + targetPlayer.transform.up * cameraY - transform.forward * distance;

        // 障害物の前にカメラを移動
        if (Physics.Linecast(targetPlayer.transform.position + targetPlayer.transform.up * cameraY, test, out hit, LayerMask.GetMask("Field")))
        {
            initialDistance = Vector3.Distance(targetPlayer.transform.position + targetPlayer.transform.up * cameraY, hit.point);

            if (initialDistance > distance)
            {
                initialDistance = distance;
            }
        }
        else
        {
            initialDistance = distance;
        }
        Debug.DrawLine(targetPlayer.transform.position + targetPlayer.transform.up * cameraY, test, Color.red);

        transform.position = targetPlayer.transform.position + targetPlayer.transform.up * cameraY - transform.forward * initialDistance;
    }
}
