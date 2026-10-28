using System;
using System.Text;
using UnityEngine;

public class mSystem
{
	public static bool isTest = false;

	public static string strAdmob;

	public static bool loadAdOk;

	public static string publicID;

	public static string android_pack;

	public static int clientType = 4;

	public static sbyte LANGUAGE;

	public static sbyte curINAPP;

	public static sbyte maxINAPP = 5;

	public const int JAVA = 1;

	public const int ANDROID = 2;

	public const int IP_JB = 3;

	public const int PC = 4;

	public const int IP_APPSTORE = 5;

	public const int WINDOWS_PHONE = 6;

	public const int GOOGLE_PLAY = 7;

	public static mSystem instance;

	internal static bool isANDROID;

	public static void AddIpTest()
	{
	}

	public static void resetCurInapp()
	{
		curINAPP = 0;
	}

	public static int getWidth(Image img)
	{
		if (clientType == 5)
		{
			return img.getWidth();
		}
		return img.getWidth();
	}

	public static int getHeight(Image img)
	{
		if (clientType == 5)
		{
			return img.getHeight();
		}
		return img.getWidth();
	}

	public static string getTimeCountDown(long timeStart, int secondCount, bool isOnlySecond, bool isShortText)
	{
		string result = string.Empty;
		long num = (timeStart + secondCount * 1000 - currentTimeMillis()) / 1000;
		if (num <= 0)
		{
			return string.Empty;
		}
		long num2 = 0L;
		long num3 = 0L;
		long num4 = num / 60;
		long num5 = num;
		if (isOnlySecond)
		{
			return num5 + string.Empty;
		}
		if (num >= 86400)
		{
			num2 = num / 86400;
			num3 = num % 86400 / 3600;
		}
		else if (num >= 3600)
		{
			num3 = num / 3600;
			num4 = num % 3600 / 60;
		}
		else if (num >= 60)
		{
			num4 = num / 60;
			num5 = num % 60;
		}
		else
		{
			num5 = num;
		}
		if (isShortText)
		{
			if (num2 > 0)
			{
				return num2 + "d";
			}
			if (num3 > 0)
			{
				return num3 + "h";
			}
			if (num4 > 0)
			{
				return num4 + "m";
			}
			if (num5 > 0)
			{
				return num5 + "s";
			}
		}
		if (num2 > 0)
		{
			if (num2 >= 10)
			{
				result = ((num3 < 1) ? (num2 + "d") : ((num3 >= 10) ? (num2 + "d" + num3 + "h") : (num2 + "d0" + num3 + "h")));
			}
			else if (num2 < 10)
			{
				result = ((num3 < 1) ? (num2 + "d") : ((num3 >= 10) ? (num2 + "d" + num3 + "h") : (num2 + "d0" + num3 + "h")));
			}
		}
		else if (num3 > 0)
		{
			if (num3 >= 10)
			{
				result = ((num4 < 1) ? (num3 + "h") : ((num4 >= 10) ? (num3 + "h" + num4 + "m") : (num3 + "h0" + num4 + "m")));
			}
			else if (num3 < 10)
			{
				result = ((num4 < 1) ? (num3 + "h") : ((num4 >= 10) ? (num3 + "h" + num4 + "m") : (num3 + "h0" + num4 + "m")));
			}
		}
		else if (num4 > 0)
		{
			if (num4 >= 10)
			{
				if (num5 >= 10)
				{
					result = num4 + "m" + num5 + string.Empty;
				}
				else if (num5 < 10)
				{
					result = num4 + "m0" + num5 + string.Empty;
				}
			}
			else if (num4 < 10)
			{
				if (num5 >= 10)
				{
					result = num4 + "m" + num5 + string.Empty;
				}
				else if (num5 < 10)
				{
					result = num4 + "m0" + num5 + string.Empty;
				}
			}
		}
		else
		{
			result = ((num5 >= 10) ? (num5 + string.Empty) : ("0" + num5 + string.Empty));
		}
		return result;
	}

	public static string numberTostring2(int aa)
	{
		try
		{
			string text = string.Empty;
			string text2 = string.Empty;
			string text3 = aa + string.Empty;
			if (text3.Equals(string.Empty))
			{
				return text;
			}
			if (text3[0] == '-')
			{
				text2 = "-";
				text3 = text3.Substring(1);
			}
			for (int num = text3.Length - 1; num >= 0; num--)
			{
				text = (((text3.Length - 1 - num) % 3 != 0 || text3.Length - 1 - num <= 0) ? (text3[num] + text) : (text3[num] + "." + text));
			}
			return text2 + text;
		}
		catch (Exception)
		{
			return aa + string.Empty;
		}
	}

	public static string numberTostring(long number)
	{
		string text = string.Empty + number;
		bool flag = false;
		try
		{
			string empty = string.Empty;
			if (number < 0)
			{
				flag = true;
				number = -number;
				text = string.Empty + number;
			}
			int num = 0;
			if (number >= 1000000000)
			{
				empty = "b";
				number /= 1000000000;
				num = (string.Empty + number).Length;
			}
			else if (number >= 1000000)
			{
				empty = "m";
				number /= 1000000;
				num = (string.Empty + number).Length;
			}
			else
			{
				if (number < 1000)
				{
					if (flag)
					{
						return "-" + text;
					}
					return text;
				}
				empty = "k";
				number /= 1000;
				num = (string.Empty + number).Length;
			}
			int num2 = int.Parse(text.Substring(num, 2));
			text = ((num2 == 0) ? (text.Substring(0, num) + empty) : ((num2 % 10 != 0) ? (text.Substring(0, num) + "," + text.Substring(num, 2) + empty) : (text.Substring(0, num) + "," + text.Substring(num, 1) + empty)));
		}
		catch (Exception)
		{
		}
		if (flag)
		{
			return "-" + text;
		}
		return text;
	}

	public static void callHotlinePC()
	{
		Application.OpenURL("http://freenro.com//");
	}

	public static void callHotlineJava()
	{
	}

	public static void callHotlineIphone()
	{
	}

	public static void callHotlineWindowsPhone()
	{
	}

	public static void closeBanner()
	{
	}

	public static void showBanner()
	{
	}

	public static void createAdmob()
	{
	}

	public static void checkAdComlete()
	{
	}

	public static void paintPopUp2(mGraphics g, int x, int y, int w, int h)
	{
		g.fillRect(x, y, w + 10, h, 0, 90);
	}

	public static void arraycopy(sbyte[] scr, int scrPos, sbyte[] dest, int destPos, int lenght)
	{
		Array.Copy(scr, scrPos, dest, destPos, lenght);
	}

	public static void arrayReplace(sbyte[] scr, int scrPos, ref sbyte[] dest, int destPos, int lenght)
	{
		if (scr != null && dest != null && scrPos + lenght <= scr.Length)
		{
			sbyte[] array = new sbyte[dest.Length + lenght];
			for (int i = 0; i < destPos; i++)
			{
				array[i] = dest[i];
			}
			for (int j = destPos; j < destPos + lenght; j++)
			{
				array[j] = scr[scrPos + j - destPos];
			}
			for (int k = destPos + lenght; k < array.Length; k++)
			{
				array[k] = dest[destPos + k - lenght];
			}
		}
	}

	public static long currentTimeMillis()
	{
		DateTime dateTime = new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc);
		return (DateTime.UtcNow.Ticks - dateTime.Ticks) / 10000;
	}

	public static void freeData()
	{
		Resources.UnloadUnusedAssets();
		GC.Collect();
	}

	public static sbyte[] convertToSbyte(byte[] scr)
	{
		sbyte[] array = new sbyte[scr.Length];
		for (int i = 0; i < scr.Length; i++)
		{
			array[i] = (sbyte)scr[i];
		}
		return array;
	}

	public static sbyte[] convertToSbyte(string scr)
	{
		ASCIIEncoding aSCIIEncoding = new ASCIIEncoding();
		byte[] bytes = aSCIIEncoding.GetBytes(scr);
		return convertToSbyte(bytes);
	}

	public static byte[] convetToByte(sbyte[] scr)
	{
		byte[] array = new byte[scr.Length];
		for (int i = 0; i < scr.Length; i++)
		{
			if (scr[i] > 0)
			{
				array[i] = (byte)scr[i];
			}
			else
			{
				array[i] = (byte)(scr[i] + 256);
			}
		}
		return array;
	}

	public static char[] ToCharArray(sbyte[] scr)
	{
		char[] array = new char[scr.Length];
		for (int i = 0; i < scr.Length; i++)
		{
			array[i] = (char)scr[i];
		}
		return array;
	}

	public static int currentHour()
	{
		return DateTime.Now.Hour;
	}

	public static void println(object str)
	{
		Debug.Log(str);
	}

	public static void gcc()
	{
		Resources.UnloadUnusedAssets();
		GC.Collect();
	}

	public static mSystem gI()
	{
		if (instance == null)
		{
			instance = new mSystem();
		}
		return instance;
	}

	public static void onConnectOK()
	{
		Controller.isConnectOK = true;
	}

	public static void onConnectionFail()
	{
		Controller.isConnectionFail = true;
		Debug.LogError(">>>>>>>> Controller.isConnectionFail = true;");
	}

	public static void onDisconnected()
	{
		Controller.isDisconnected = true;
	}

	public static void exitWP()
	{
	}

	public static void paintFlyText(mGraphics g)
	{
		if (GameScr.flyTextState == null)
		{
			return;
		}
		Color prevColor = GUI.color;
		Matrix4x4 prevMatrix = GUI.matrix;
		for (int i = 0; i < GameScr.flyTextState.Length; i++)
		{
			if (GameScr.flyTextState[i] != -1 && GameCanvas.isPaint(GameScr.flyTextX[i], GameScr.flyTextY[i]))
			{
				int time = (GameScr.flyTime != null && i < GameScr.flyTime.Length) ? GameScr.flyTime[i] : 0;
				float alpha = 1f;
				if (time > 18)
				{
					alpha = Mathf.Clamp01((36f - (float)time) / 18f);
				}
				GUI.color = new Color(prevColor.r, prevColor.g, prevColor.b, prevColor.a * alpha);

				float scale = 1f;
				if (time <= 1)
				{
					scale = 1.25f;
				}
				else if (time == 2)
				{
					scale = 1.15f;
				}
				else if (time == 3)
				{
					scale = 1.05f;
				}

				int screenX = GameScr.flyTextX[i] + g.translateX;
				int screenY = GameScr.flyTextY[i] + g.translateY;

				if (scale != 1f)
				{
					GUIUtility.ScaleAroundPivot(new Vector2(scale, scale), new Vector2(screenX, screenY));
				}

				int colorId = GameScr.flyTextColor[i];
				string text = GameScr.flyTextString[i];

				// Hiệu ứng hạt năng lượng lấp lánh nhẹ nhàng khi cộng Tiềm Năng & Chí Mạng (Pure math, 0% CPU)
				if (colorId == mFont.GREEN || colorId == mFont.FATAL || colorId == mFont.ADDMONEY)
				{
					if (time > 2 && time < 26)
					{
						Color spkColor = (colorId == mFont.GREEN) ? new Color(0.2f, 1f, 0.4f, alpha * 0.7f) : new Color(1f, 0.9f, 0.2f, alpha * 0.7f);
						GUI.color = spkColor;
						int spkOff1 = (time * 3) % 18 - 9;
						int spkOff2 = ((time + 6) * 3) % 20 - 10;
						int spkY1 = -(time * 12 / 10);
						int spkY2 = -(time * 9 / 10) - 2;
						g.fillRect(GameScr.flyTextX[i] - 16 + spkOff1, GameScr.flyTextY[i] + spkY1, 2, 2);
						g.fillRect(GameScr.flyTextX[i] + 16 + spkOff2, GameScr.flyTextY[i] + spkY2, 2, 2);
						GUI.color = new Color(prevColor.r, prevColor.g, prevColor.b, prevColor.a * alpha);
					}
				}

				if (colorId == mFont.RED)
				{
					mFont.bigNumber_red.drawStringBorder(g, text, GameScr.flyTextX[i], GameScr.flyTextY[i], mFont.CENTER);
				}
				else if (colorId == mFont.YELLOW)
				{
					mFont.bigNumber_yellow.drawStringBorder(g, text, GameScr.flyTextX[i], GameScr.flyTextY[i], mFont.CENTER);
				}
				else if (colorId == mFont.GREEN)
				{
					mFont.bigNumber_green.drawStringBorder(g, text, GameScr.flyTextX[i], GameScr.flyTextY[i], mFont.CENTER, mFont.bigNumber_black);
				}
				else if (colorId == mFont.FATAL)
				{
					mFont.bigNumber_yellow.drawStringBorder(g, text, GameScr.flyTextX[i], GameScr.flyTextY[i], mFont.CENTER, mFont.bigNumber_black);
				}
				else if (colorId == mFont.FATAL_ME)
				{
					mFont.bigNumber_green.drawStringBorder(g, text, GameScr.flyTextX[i], GameScr.flyTextY[i], mFont.CENTER, mFont.bigNumber_black);
				}
				else if (colorId == mFont.MISS)
				{
					mFont.bigNumber_While.drawStringBorder(g, text, GameScr.flyTextX[i], GameScr.flyTextY[i], mFont.CENTER, mFont.tahoma_7_grey);
				}
				else if (colorId == mFont.ORANGE)
				{
					mFont.bigNumber_orange.drawStringBorder(g, text, GameScr.flyTextX[i], GameScr.flyTextY[i], mFont.CENTER);
				}
				else if (colorId == mFont.ADDMONEY)
				{
					mFont.bigNumber_yellow.drawStringBorder(g, text, GameScr.flyTextX[i], GameScr.flyTextY[i], mFont.CENTER, mFont.bigNumber_black);
				}
				else if (colorId == mFont.MISS_ME)
				{
					mFont.bigNumber_While.drawStringBorder(g, text, GameScr.flyTextX[i], GameScr.flyTextY[i], mFont.CENTER, mFont.bigNumber_black);
				}
				else if (colorId == mFont.HP)
				{
					mFont.bigNumber_red.drawStringBorder(g, text, GameScr.flyTextX[i], GameScr.flyTextY[i], mFont.CENTER, mFont.bigNumber_black);
				}
				else if (colorId == mFont.MP)
				{
					mFont.bigNumber_blue.drawStringBorder(g, text, GameScr.flyTextX[i], GameScr.flyTextY[i], mFont.CENTER, mFont.bigNumber_black);
				}

				if (scale != 1f)
				{
					GUI.matrix = prevMatrix;
				}
			}
		}
		GUI.color = prevColor;
		GUI.matrix = prevMatrix;
	}

	public static void endKey()
	{
	}

	public static FrameImage getFraImage(string nameImg)
	{
		FrameImage result = null;
		MainImage mainImage = null;
		if (mainImage == null)
		{
			mainImage = ImgByName.getImagePath(nameImg, ImgByName.hashImagePath);
		}
		if (mainImage != null && mainImage.img != null)
		{
			int nFrame = (mainImage.nFrame > 0) ? (int)mainImage.nFrame : 1;
			int num = mainImage.img.getHeight() / nFrame;
			if (num < 1)
			{
				num = 1;
			}
			result = new FrameImage(mainImage.img, mainImage.img.getWidth(), num);
		}
		return result;
	}

	public static Image loadImage(string path)
	{
		return GameCanvas.loadImage(path);
	}
}
