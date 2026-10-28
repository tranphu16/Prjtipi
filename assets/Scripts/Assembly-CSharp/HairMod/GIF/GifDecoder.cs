using System.Collections.Generic;
using UnityEngine;

public class GifDecoder
{
    public static List<Texture2D> DecodeGIF(string filePath)
    {
        List<Texture2D> frames = new List<Texture2D>();

        try
        {
            // Đọc tệp GIF vào bộ nhớ
            byte[] gifData = (Resources.Load(filePath) as TextAsset).bytes;
            int offset = 13; // Bỏ qua header và Logical Screen Descriptor

            while (offset < gifData.Length)
            {
                if (gifData[offset] == 0x2C) // Image Descriptor
                {
                    offset += 9; // Bỏ qua Image Descriptor
                    int lctFlag = gifData[offset];
                    offset++;

                    int lctSize = 2 << (lctFlag & 0x07); // Kích thước bảng màu

                    if ((lctFlag & 0x80) != 0)
                    {
                        offset += lctSize * 3; // Bỏ qua bảng màu cục bộ nếu có
                    }

                    // Giải mã khung hình
                    Texture2D frame = DecodeImage(gifData, ref offset);
                    if (frame != null)
                    {
                        frames.Add(frame);
                    }
                }
                else if (gifData[offset] == 0x21) // Extension Block
                {
                    offset += 2; // Bỏ qua giới thiệu Extension
                    int blockSize = gifData[offset];
                    offset += blockSize + 1; // Bỏ qua Extension Block
                }
                else
                {
                    break;
                }
            }
        }
        catch (System.Exception ex)
        {
            Debug.LogError("Lỗi khi giải mã GIF: " + ex.Message);
        }

        return frames;
    }

    static Texture2D DecodeImage(byte[] gifData, ref int offset)
    {
        int width = (gifData[offset + 1] << 8) | gifData[offset];
        int height = (gifData[offset + 3] << 8) | gifData[offset + 2];
        offset += 4;

        // Tạo texture
        Texture2D texture = new Texture2D(width, height);
        Color32[] pixels = new Color32[width * height];

        // Giả lập quá trình giải mã đơn giản (cần thêm giải mã LZW thực sự ở đây)
        for (int i = 0; i < pixels.Length; i++)
        {
            pixels[i] = new Color32(gifData[offset], gifData[offset + 1], gifData[offset + 2], 255);
            offset += 3; // Giả sử không nén, nhảy tới pixel tiếp theo
        }

        texture.SetPixels32(pixels);
        texture.Apply();

        return texture;
    }
}