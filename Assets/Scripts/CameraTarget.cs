using Cinemachine;
using UnityEngine;


public class CameraTarget : MonoBehaviour
{
  [SerializeField] private CinemachineVirtualCamera _cinemachineVirtualCamera;
  private float ortographicSize;
  private float targetOrtographicSize;

  private void Start()
  {
    ortographicSize = _cinemachineVirtualCamera.m_Lens.OrthographicSize;
    targetOrtographicSize = ortographicSize;
  }

  private void Update()
  {
    HandleMovement();
    HandleZoom();
  }

  private void HandleMovement()
  {
    float x = Input.GetAxisRaw("Horizontal");
    float y = Input.GetAxisRaw("Vertical");

    Vector3 moveDir = new Vector3(x, y).normalized;
    float moveSpeed = 30f;

    transform.position += moveDir * moveSpeed * Time.deltaTime;
  }

  private void HandleZoom()
  {
    float zoomAmount = 2f;
    targetOrtographicSize += Input.mouseScrollDelta.y * zoomAmount;

    float minOrtographicSize = 10;
    float maxOrtographicSize = 30;
    targetOrtographicSize = Mathf.Clamp(targetOrtographicSize, minOrtographicSize, maxOrtographicSize);

    float zoomSpeed = 5f;
    ortographicSize = Mathf.Lerp(ortographicSize, targetOrtographicSize, Time.deltaTime * zoomSpeed);
    
    _cinemachineVirtualCamera.m_Lens.OrthographicSize = ortographicSize;
  }
}
