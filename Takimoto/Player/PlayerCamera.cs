using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// カメラの移動、回転を行うクラス
/// </summary>
public class PlayerCamera : MonoBehaviour
{
    private Camera mainCamera;
    public void SetMainCamera(Camera camera)
    {
        mainCamera = camera;
    }
    public Camera GetMainCamera()
    {
        return mainCamera;
    }

    /// <summary>
    /// カメラの初期位置
    /// </summary>
    public static readonly Vector3 INITIAL_POSITION = new Vector3(0f, 1.6f, -4f);
    /// <summary>
    /// カメラの中心点
    /// </summary>
    private Vector3 centerPoint;

    /// <summary>
    /// カメラと中心点の初期距離
    /// </summary>
    private float initialDistance;
    /// <summary>
    /// カメラと中心点の距離
    /// </summary>
    private float distance = -1f;

    /// <summary>
    /// カメラ水平マウス感度
    /// </summary>
    private float mouseXSpeed = 2f;
    /// <summary>
    /// カメラ垂直マウス感度
    /// </summary>
    private float mouseYSpeed = 1.5f;
    /// <summary>
    /// カメラ水平コントローラ感度
    /// </summary>
    private float rotationHorizontalSpeed = 300f;
    /// <summary>
    /// カメラ垂直コントローラ感度
    /// </summary>
    private float rotationVerticalSpeed = 200f;

    private bool isAim = false;
    public void SetIsAim(bool value)
    {
        isAim = value;
    }
    private float aimSpeed = 1f / 2f;

    /// <summary>
    /// 角度上限
    /// </summary>
    const float ANGLE_UPPER_LIMIT = 340f;
    /// <summary>
    /// 角度下限
    /// </summary>
    const float ANGLE_LOWER_LIMIT = 25f;

    private Skybox skyBox;
    private Material skyBoxMat;


    void Awake()
    {
        skyBox = GetComponent<Skybox>();

        // カメラを生成
        //mainCamera = Instantiate(
        //    mainCameraPrefab, transform.position + INITIAL_POSITION, transform.rotation);

        // マウスを非表示
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
    }

    public void SetSkyBox(Material skyBoxMat)
    {
        skyBox.material = skyBoxMat;
    }

    public void InitCamera()
    {
        if (distance < 0)
        {
            // カメラの位置を初期化
            mainCamera.transform.position = transform.position + INITIAL_POSITION;
            // カメラの中心点を計算
            centerPoint = transform.position + transform.up * INITIAL_POSITION.y;
            // カメラと中心点の距離を計算
            initialDistance = Vector3.Distance(mainCamera.transform.position, centerPoint);
            distance = initialDistance;
        }
    }

    public void UpdateCamera(float mouseX, float mouseY, float rotationHorizontal, float rotationVertical)
    {
        if(distance < 0)
        {
            // カメラの位置を初期化
            mainCamera.transform.position = transform.position + INITIAL_POSITION;
            // カメラの中心点を計算
            centerPoint = transform.position + transform.up * INITIAL_POSITION.y;
            // カメラと中心点の距離を計算
            initialDistance = Vector3.Distance(mainCamera.transform.position, centerPoint);
            distance = initialDistance;
        }

        // カメラ回転
        RotateCamera(mouseX, mouseY, rotationHorizontal, rotationVertical);

        // カメラ移動
        MoveCamera();
    }

    void RotateCamera(float mouseX, float mouseY, float rotationHorizontal, float rotationVertical)
    {
        if (mainCamera.transform.localEulerAngles.x < ANGLE_UPPER_LIMIT &&
            mainCamera.transform.localEulerAngles.x > 180f)
        {
            //Debug.Log("上限");
            if (mouseY > 0) { mouseY = 0; }
            if (rotationVertical < 0) { rotationVertical = 0; }
        }
        else if (mainCamera.transform.eulerAngles.x > ANGLE_LOWER_LIMIT &&
            mainCamera.transform.localEulerAngles.x < 180f)
        {
            //Debug.Log("下限");
            if (mouseY < 0) { mouseY = 0; }
            if (rotationVertical > 0) { rotationVertical = 0; }
        }

        // カメラの中心点を更新
        centerPoint = transform.position + transform.up * INITIAL_POSITION.y;

        float mouseXSpeed = 0f;
        float mouseYSpeed = 0f;
        float rotationHorizontalSpeed = 0f;
        float rotationVerticalSpeed = 0f;

        if (isAim)
        {
            mouseXSpeed = this.mouseXSpeed * aimSpeed;
            mouseYSpeed = this.mouseYSpeed * aimSpeed;
            rotationHorizontalSpeed = this.rotationHorizontalSpeed * aimSpeed;
            rotationVerticalSpeed = this.rotationVerticalSpeed * aimSpeed;
        }
        else
        {
            mouseXSpeed = this.mouseXSpeed;
            mouseYSpeed = this.mouseYSpeed;
            rotationHorizontalSpeed = this.rotationHorizontalSpeed;
            rotationVerticalSpeed = this.rotationVerticalSpeed;
        }

        mainCamera.transform.RotateAround(
            transform.position,
            Vector3.up,
            mouseX * mouseXSpeed);
        mainCamera.transform.RotateAround(
            transform.position,
            mainCamera.transform.right,
            -mouseY * mouseYSpeed);

        mainCamera.transform.RotateAround(
            transform.position,
            Vector3.up,
            rotationHorizontal * rotationHorizontalSpeed * Time.deltaTime);
        mainCamera.transform.RotateAround(
            transform.position,
            mainCamera.transform.right,
            rotationVertical * rotationVerticalSpeed * Time.deltaTime);
    }

    /// <summary>
    /// カメラ移動
    /// </summary>
    void MoveCamera()
    {
        RaycastHit hit;

        Vector3 test = centerPoint - mainCamera.transform.forward * initialDistance;

        // 障害物の前にカメラを移動
        if (Physics.Linecast(centerPoint, test, out hit, LayerMask.GetMask(new string[] { "Field", "Enemy" })))
        {
            distance = Vector3.Distance(centerPoint, hit.point);

            if (distance > initialDistance)
            {
                distance = initialDistance;
            }
        }
        else
        {
            distance = initialDistance;
        }
        Debug.DrawLine(centerPoint, test, Color.red);

        mainCamera.transform.position = centerPoint - mainCamera.transform.forward * distance;
    }

    private IEnumerator DoShake(float shakeTime, float shakeX, float shakeY)
    {
        float time = 0f;

        while (shakeTime > time)
        {
            float x = mainCamera.transform.position.x + Random.Range(-shakeX, shakeX);
            float y = mainCamera.transform.position.y + Random.Range(-shakeY, shakeY);

            mainCamera.transform.position = new Vector3(x, y, mainCamera.transform.position.z);

            time += Time.deltaTime;

            yield return null;
        }
    }

    public void ShakeCamera(float shakeTime, float shakeX, float shakeY)
    {
        StartCoroutine(DoShake(shakeTime, shakeX, shakeY));
    }

    public void CaptureTarget(Transform targetTransform)
    {
        //Debug.Log("ターゲットを捕捉");
        mainCamera.transform.LookAt(targetTransform.position + targetTransform.up);
    }

    public void DestroyCamera()
    {
        Destroy(mainCamera.gameObject);
    }
}
