using UnityEngine.Networking;
using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
using Unity.VisualScripting;

public class WebIP : MonoBehaviour
{
    // URL của API hoặc tệp văn bản mà bạn muốn lấy
    private string url = "https://raw.githubusercontent.com/HairModNRO/WebRequest/main/textIPV";

    void Start()
    {
        //DontDestroyOnLoad(this);
        // Gọi coroutine để thực hiện yêu cầu
        StartCoroutine(GetTextFromURL());
    }
    public static IEnumerable loadImageWeb(string url, Image image)
    {
        UnityWebRequest unityWebRequest = UnityWebRequest.Get(url);
       yield return unityWebRequest.SendWebRequest();
        if (unityWebRequest.result != UnityWebRequest.Result.ConnectionError && unityWebRequest.result != UnityWebRequest.Result.ProtocolError)
        {
            byte[] bytes = unityWebRequest.downloadHandler.data;
            image = Image.createImage(bytes);
        }
        
    }
    IEnumerator GetTextFromURL()
    {
        // Tạo một UnityWebRequest để yêu cầu dữ liệu văn bản từ URL
        UnityWebRequest request = UnityWebRequest.Get(url);

        // Chờ cho đến khi yêu cầu hoàn thành
        yield return request.SendWebRequest();

        // Kiểm tra xem yêu cầu có gặp lỗi gì không
        if (request.result == UnityWebRequest.Result.ConnectionError || request.result == UnityWebRequest.Result.ProtocolError)
        {
            //gọi lại nếu nó bị lỗi.
            StartCoroutine(GetTextFromURL());
        }
        else
        {
            // Lấy dữ liệu văn bản từ phản hồi của yêu cầu
            string responseText = request.downloadHandler.text;

            // In ra console hoặc xử lý dữ liệu theo cách khác
            ServerListScreen.smartPhoneVN = ServerListScreen.IPVIET;
            SceneManager.LoadScene("NROL");
        }
    }
}