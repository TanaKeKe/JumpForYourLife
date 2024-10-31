using Sirenix.OdinInspector;
using UnityEngine;

namespace Anchor
{
    public class AnchorToCamera : MonoBehaviour
    {
        [SerializeField] private ECamAnchor anchor;
        [SerializeField] private Vector2 offset;

        private void OnValidate()
        {
            SetAnchorPosition();
        }
        #region Cam thay đổi size thì dùng nhưng game này chưa dùng
        /*private void Awake()
        {
            this.RegisterListener(GameConfig.Event.OnChangeCamSize, RefreshAnchorPos);
        }

        private void OnDestroy()
        {
            this.RemoveListener(GameConfig.Event.OnChangeCamSize, RefreshAnchorPos);
        }*/
        #endregion
        private void RefreshAnchorPos(object obj)
        {
            SetAnchorPosition();
        }

        private void OnEnable()
        {
            SetAnchorPosition();
        }

        [ButtonGroup("Button")] [Button]
        public void UpdateOffset()
        {
            var currentPos = transform.position;
            var anchorPos = GetPosByCamAnchor(anchor);
            offset = currentPos - anchorPos;
        }

        [ButtonGroup("Button")] [Button]
        private void SetAnchorPosition()
        {
            var newPos = GetPosByCamAnchor(anchor) + (Vector3)offset;
            transform.localPosition = new Vector3(newPos.x, newPos.y, 0f);
        }

        private Vector3 GetPosByCamAnchor(ECamAnchor anchorType)
        {
            var cam = Camera.main;
            if (cam == null)
            {
                Debug.LogError("Main Camera not found!");
                return Vector3.zero;
            }

            var pos = Vector3.zero;

            if (cam.orthographic)
            {
                float camHeight = cam.orthographicSize * 2;
                float camWidth = camHeight * cam.aspect;

                switch (anchorType)
                {
                    case ECamAnchor.Center:
                        pos = new Vector3(0, 0, 0);
                        break;
                    case ECamAnchor.TopLeft:
                        pos = new Vector3(-camWidth / 2, camHeight / 2, 0);
                        break;
                    case ECamAnchor.Top:
                        pos = new Vector3(0, camHeight / 2, 0);
                        break;
                    case ECamAnchor.TopRight:
                        pos = new Vector3(camWidth / 2, camHeight / 2, 0);
                        break;
                    case ECamAnchor.Left:
                        pos = new Vector3(-camWidth / 2, 0, 0);
                        break;
                    case ECamAnchor.Right:
                        pos = new Vector3(camWidth / 2, 0, 0);
                        break;
                    case ECamAnchor.BottomLeft:
                        pos = new Vector3(-camWidth / 2, -camHeight / 2, 0);
                        break;
                    case ECamAnchor.Bottom:
                        pos = new Vector3(0, -camHeight / 2, 0);
                        break;
                    case ECamAnchor.BottomRight:
                        pos = new Vector3(camWidth / 2, -camHeight / 2, 0);
                        break;
                }
            }
            else
            {
                Debug.LogError("Camera is not orthographic!");
            }

            return pos;
        }
    }
}