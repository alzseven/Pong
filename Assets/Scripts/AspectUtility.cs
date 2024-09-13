using System;
using UnityEngine;

namespace pong
{
    /// <summary>
    /// 화면 비율 계산
    /// 가로의 경우 
    /// 실제 화면과 원하는 화면의 세로 비율을 1로 놓았을 때 가로의 비를 구합니다. 
    /// (원하는 화면의 가로 비 나누기 실제 화면 가로 비) 를 통해 실제 화면에서 원하는 화면의 비율을 구합니다.
    /// 이 비율은 Viewport Rect 의 W에 들어갑니다. 
    /// 이제 원하는 화면만큼만 사용할 수 있게 되었지만 화면의 위치가 중앙이 아니라는 문제가 남았습니다. 이 문제는 Viewport Rect의 X 값을 조절하여 해결할 수 있습니다. 
    /// X 값은 원하는 화면의 비율에서 1을 뺀 값 나누기 2 를 한 값 입니다.
    /// 원하는 화면의 비율에서 1을 빼게 되면 원하지 않는 화면의 비율입니다. 이 비율의 반 값 만큼 X 축으로 이동하면 정확히 중앙에 위차하게 됩니다. 
    /// </summary>
    // [ExecuteAlways]
    public class AspectUtility : MonoBehaviour
    {
        public int x;
        public int y;

        private int prevWidth;
        private int prevHeight;
        
        static float wantedAspectRatio;
        static Camera cam;
        static Camera backgroundCam;

        void Awake()
        {
            cam = GetComponent<Camera>();
            if (!cam)
            {
                cam = Camera.main;
            }

            if (!cam)
            {
                Debug.LogError("No camera available");
                return;
            }

            wantedAspectRatio = (float)x / y;
            prevWidth = Screen.width;
            prevHeight = Screen.height;
            SetCamera();
        }

        public static void SetCamera()
        {
            float currentAspectRatio = (float)Screen.width / Screen.height;
            // If the current aspect ratio is already approximately equal to the desired aspect ratio,
            // use a full-screen Rect (in case it was set to something else previously)


            if ((int)(currentAspectRatio * 100) / 100.0f == (int)(wantedAspectRatio * 100) / 100.0f)
            {
                cam.rect = new Rect(0.0f, 0.0f, 1.0f, 1.0f);
                if (backgroundCam)
                {
                    Destroy(backgroundCam.gameObject);
                }

                return;
            }

            // Pillarbox
            if (currentAspectRatio > wantedAspectRatio)
            {
                float inset = 1.0f - wantedAspectRatio / currentAspectRatio;
                //Debug.Log(new Rect(inset / 2, 0.0f, 1.0f - inset, 1.0f));
                cam.rect = new Rect(inset / 2, 0.0f, 1.0f - inset, 1.0f);
            }
            // Letterbox
            else
            {
                float inset = 1.0f - currentAspectRatio / wantedAspectRatio;
                cam.rect = new Rect(0.0f, inset / 2, 1.0f, 1.0f - inset);
            }

            if (!backgroundCam)
            {
                // Make a new camera behind the normal camera which displays black; otherwise the unused space is undefined
                backgroundCam = new GameObject("BackgroundCam", typeof(Camera)).GetComponent<Camera>();
                backgroundCam.depth = int.MinValue;
                backgroundCam.clearFlags = CameraClearFlags.SolidColor;
                backgroundCam.backgroundColor = Color.black;
                backgroundCam.cullingMask = 0;
            }
        }

        private void Update()
        {
            if(Screen.width != prevWidth || Screen.height != prevHeight) SetCamera();
        }

        public static int screenHeight
        {
            get { return (int)(Screen.height * cam.rect.height); }
        }

        public static int screenWidth
        {
            get { return (int)(Screen.width * cam.rect.width); }
        }

        public static int xOffset
        {
            get { return (int)(Screen.width * cam.rect.x); }
        }

        public static int yOffset
        {
            get { return (int)(Screen.height * cam.rect.y); }
        }

        public static Rect screenRect
        {
            get
            {
                return new Rect(cam.rect.x * Screen.width, cam.rect.y * Screen.height, cam.rect.width * Screen.width,
                    cam.rect.height * Screen.height);
            }
        }

        public static Vector3 mousePosition
        {
            get
            {
                Vector3 mousePos = Input.mousePosition;
                mousePos.y -= (int)(cam.rect.y * Screen.height);
                mousePos.x -= (int)(cam.rect.x * Screen.width);
                return mousePos;
            }
        }

        public static Vector2 guiMousePosition
        {
            get
            {
                Vector2 mousePos = Event.current.mousePosition;
                mousePos.y = Mathf.Clamp(mousePos.y, cam.rect.y * Screen.height,
                    cam.rect.y * Screen.height + cam.rect.height * Screen.height);
                mousePos.x = Mathf.Clamp(mousePos.x, cam.rect.x * Screen.width,
                    cam.rect.x * Screen.width + cam.rect.width * Screen.width);
                return mousePos;
            }
        }
    }
}