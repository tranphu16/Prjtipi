using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public class EncryptAccount
{
    public static string Encrypt(string input)
    {
        string result = string.Empty;

        byte[] data = System.Text.Encoding.UTF8.GetBytes(input);

        for (int i = 0; i < data.Length; i++)
        {
            data[i] = (byte)((data[i] & 0xff) ^ (0x5A & 0xff));
        }

        result = System.Convert.ToBase64String(data);

        return result;
    }
    public static string Decrypt(string encryptedText, int key)
    {
        StringBuilder stringBuilder = new StringBuilder();
        foreach (char c in encryptedText)
        {
            stringBuilder.Append((char)(c - key));
        }

        byte[] bytes = Convert.FromBase64String(stringBuilder.ToString());
        return Encoding.UTF8.GetString(bytes);
    }
    public static string HexToString(string hexInput)
    {
        string[] hexValues = hexInput.Split(new[] { ' ', '\n', '\t' }, StringSplitOptions.RemoveEmptyEntries);
        byte[] bytes = new byte[hexValues.Length];
        for (int i = 0; i < hexValues.Length; i++)
        {
            bytes[i] = Convert.ToByte(hexValues[i].Substring(2), 16);
        }
        return Encoding.UTF8.GetString(bytes);
    }
    public static unsafe int FloatToIntBits(float value)
    {
        return *(int*)(&value);
    }

    public static unsafe float IntBitsToFloat(int value)
    {
        return *(float*)(&value);
    }
}