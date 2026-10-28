public static class VegitoCCLightning
{
	private const int FirstHead = 2099;

	private const int LastHead = 2104;

	private const int Body = 2105;

	private const int Leg = 2106;

	private const int FrameDelay = 4;

	private const int CenterOffsetY = 23;

	private static readonly Image[] frames = new Image[4];

	private static bool isLoaded;

	public static bool IsActive(Char character)
	{
		return character != null && character.head >= FirstHead && character.head <= LastHead && character.body == Body && character.leg == Leg;
	}

	public static void Paint(mGraphics g, Char character)
	{
		if (!IsActive(character))
		{
			return;
		}
		LoadFrames();
		Image image = frames[GameCanvas.gameTick / FrameDelay % frames.Length];
		if (image == null)
		{
			return;
		}
		int transform = (character.cdir == 1) ? 0 : 2;
		g.drawRegion(image, 0, 0, mGraphics.getImageWidth(image), mGraphics.getImageHeight(image), transform, character.cx, character.cy - CenterOffsetY, mGraphics.VCENTER | mGraphics.HCENTER);
	}

	private static void LoadFrames()
	{
		if (isLoaded)
		{
			return;
		}
		isLoaded = true;
		for (int i = 0; i < frames.Length; i++)
		{
			frames[i] = GameCanvas.loadImage("/vegito_cc_lightning/frame_" + i + ".png");
		}
	}
}
