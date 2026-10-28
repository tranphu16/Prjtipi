//using System;
//using UnityEngine;
//using UnityEngine.Video;

//// Token: 0x020000F1 RID: 241
//public class VideoScript : MonoBehaviour
//{
//    // Token: 0x17000006 RID: 6
//    // (get) Token: 0x06000C38 RID: 3128 RVA: 0x000AA9F0 File Offset: 0x000A8BF0
//    // (set) Token: 0x06000C37 RID: 3127 RVA: 0x000AA9E8 File Offset: 0x000A8BE8
//    public static VideoScript instance { get; private set; }

//    // Token: 0x06000C39 RID: 3129 RVA: 0x000AA9F7 File Offset: 0x000A8BF7
//    private void Awake()
//    {
//        if (VideoScript.instance == null)
//        {
//            VideoScript.instance = this;
//        }
//    }

//    // Token: 0x06000C3A RID: 3130 RVA: 0x000AAA0C File Offset: 0x000A8C0C
//    private void Start()
//    {
//        if (this.video == null)
//        {
//            this.video = Camera.main.GetComponent<VideoPlayer>();
//            return;
//        }
//        this.video.Play();
//    }

//    // Token: 0x06000C3B RID: 3131 RVA: 0x000AAA38 File Offset: 0x000A8C38
//    public void paint()
//    {
//        GUI.DrawTexture(new Rect(0f, 0f, (float)Screen.width, (float)Screen.height), this.video.texture);
//    }

//    // Token: 0x04001546 RID: 5446
//    public VideoPlayer video;

//    // Token: 0x04001547 RID: 5447
//    public Texture2D videoFrames;
//}
