﻿using Microsoft.Extensions.Logging;
using SkiaSharp.Views.Maui.Controls.Hosting;
using System.Text;

namespace MauiApp1;

public static class MauiProgram
{
	public static MauiApp CreateMauiApp()
	{
		//// NuGET のパッケージ・マネージャー・コンソールの文字化けに対応を期待したが効果なし
		//Encoding.RegisterProvider(CodePagesEncodingProvider.Instance);

		var builder = MauiApp.CreateBuilder();
		builder
			.UseMauiApp<App>()
			.ConfigureFonts(fonts =>
			{
				fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
				fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
			});

        //
        // SkiaSharp を直で使いたいときはこう書く
        // ======================================
        //
        // 📖 [[BUG] MAUI: SKCanvasView crash, unable to display SKBitmap directly #2139](https://github.com/mono/SkiaSharp/issues/2139)
        //
        builder
            .UseMauiApp<App>()
			.UseSkiaSharp();

#if DEBUG
		builder.Logging.AddDebug();
#endif

		return builder.Build();
	}
}
