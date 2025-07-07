#if !USE_ORIGINAL_CODE
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace Terraria.Hardmode
{
	public class Assets
	{
		public static int TextBGBorderWidth;

		public static Texture2D TextBG, HardmodeUpsell;

		public static void LoadContent(ContentManager Content)
		{
#if VERSION_INITIAL
			TextBG = Content.Load<Texture2D>("UI/HowToPlay/Text_Back");
#else
			TextBG = Content.Load<Texture2D>("UI/HowToPlay/Howto_Logo"); // Since it is not used with the code equivalent, stubbing it out with the game logo.
#endif
			TextBGBorderWidth = (Main.ScreenHeightPtr == ScreenHeights.FHD) ? 32 : 20;
			HardmodeUpsell = Content.Load<Texture2D>("Images/UpsellHM"); // Courtesy of the old mates at Codeglue.
		}
	}
}
#endif