namespace MauiApp1;

#if IOS || ANDROID || MACCATALYST
using Microsoft.Maui.Graphics.Platform;
#elif WINDOWS
using Microsoft.Maui.Graphics.Win2D;
using System.Diagnostics;
using System.Net;
#endif

using Microsoft.Maui.Controls;
using SkiaSharp;
using SkiaSharp.Views.Maui;
using SkiaSharp.Views.Maui.Controls;
using System.IO;

public partial class MainPage : ContentPage
{
    // - その他

    #region その他（生成）
    /// <summary>
    ///		生成
    /// </summary>
    public MainPage()
    {
        InitializeComponent();
    }
    #endregion

    // - パブリック・プロパティ

    #region プロパティ（ビューモデル）
    /// <summary>
    ///     ビューモデル
    /// </summary>
    public MainPageViewModel MainPageVM => this.BindingContext as MainPageViewModel;
    #endregion

    // - プライベート・プロパティ

    #region プロパティ（タッチ位置）
    /// <summary>
    ///     タッチ位置
    /// </summary>
    SKPoint? touchLocation;
    #endregion

    // - プライベート・イベントハンドラ

    #region イベントハンドラ（［画像読込］ボタン・クリック時）
    /// <summary>
    ///		［画像読込］ボタン・クリック時
    /// </summary>
    /// <param name="sender">このイベントを送っているコントロール</param>
    /// <param name="e">イベント</param>
    private void LoadImageButton_Clicked(object sender, EventArgs e)
    {
        var bindingContext = this.BindingContext as MainPageViewModel;

        // bindingContext.ImageFilePath

        //
        // 画像の読込
        // ==========
        //
        var task = Task.Run(async () =>
        {
            // ↓ MAUI の流儀
            try
            {
                // タイル・セット読込（読込元：　ウィンドウズ・ローカルＰＣ）
                using (Stream inputFileStream = System.IO.File.OpenRead(bindingContext.ImageFilePath))
                {
#if IOS || ANDROID || MACCATALYST
                    // PlatformImage isn't currently supported on Windows.
                    bindingContext.Image = PlatformImage.FromStream(inputFileStream);
#elif WINDOWS
                    bindingContext.Image = new W2DImageLoadingService().FromStream(inputFileStream);
#endif

                    // 必ず再描画が行われるわけではない
                    graphicsView1.Invalidate();

                    ////
                    //// 作業中のタイル・セット画像の保存
                    ////
                    //if (image != null)
                    //{
                    //    // 書出先（ウィンドウズ・ローカルＰＣ）
                    //    using (Stream outputFileStream = System.IO.File.Open(workingTileSetImagefilePathAsStr, FileMode.OpenOrCreate))
                    //    {
                    //        image.Save(outputFileStream);
                    //    }
                    //}

                    //// 作業中のタイル・セット画像の再描画
                    //context.RefreshWorkingTileSetImage();
                }
            }
            catch (Exception ex)
            {
                // TODO エラー対応どうする？
            }

            // ↓ SkiaSharp の流儀
            try
            {
                // タイル・セット読込（読込元：　ウィンドウズ・ローカルＰＣ）
                using (Stream inputFileStream = System.IO.File.OpenRead(bindingContext.ImageFilePath))
                {
                    // ↓ １つのストリームが使えるのは、１回切り
                    using (MemoryStream memStream = new MemoryStream())
                    {
                        await inputFileStream.CopyToAsync(memStream);
                        memStream.Seek(0, SeekOrigin.Begin);

                        bindingContext.SKBitmap = SKBitmap.Decode(memStream);

                        // 画像処理（明度を下げる）
                        ReduceBrightness.DoItInPlace(bindingContext.SKBitmap);
                    };

                    // 再描画
                    skiaView1.InvalidateSurface();
                }
            }
            catch (Exception ex)
            {
                // TODO エラー対応どうする？
            }
        });

    }
    #endregion

    #region イベントハンドラ（表面の描画時）
    /// <summary>
    ///     表面の描画時
    /// </summary>
    /// <param name="sender">このイベントを送っているコントロール</param>
    /// <param name="e">イベント</param>
    private void skiaView_PaintSurface(object sender, SkiaSharp.Views.Maui.SKPaintSurfaceEventArgs e)
    {
        var bindingContext = this.BindingContext as MainPageViewModel;

        //
        // タッチしたところにテキストを置くというサンプルか？
        // ==================================================
        //

        // the the canvas and properties
        var canvas = e.Surface.Canvas;

        // make sure the canvas is blank
        canvas.Clear(SKColors.White);

        // decide what the text looks like
        using var paint = new SKPaint
        {
            Color = SKColors.Black,
            IsAntialias = true,
            Style = SKPaintStyle.Fill,
            TextAlign = SKTextAlign.Center,
            TextSize = 24
        };

        // adjust the location based on the pointer
        var coord = (touchLocation is SKPoint loc)
            ? new SKPoint(loc.X, loc.Y)
            : new SKPoint(e.Info.Width / 2, (e.Info.Height + paint.TextSize) / 2);

        // 画像描画
        if (bindingContext.SKBitmap != null)
        {
            canvas.DrawImage(
                image: SKImage.FromBitmap(bindingContext.SKBitmap),
                p: coord);
        }

        // draw some text
        canvas.DrawText("SkiaSharp", coord, paint);
    }
    #endregion

    #region イベントハンドラ（タッチ時）
    /// <summary>
    ///     タッチ時
    /// </summary>
    /// <param name="sender">このイベントを送っているコントロール</param>
    /// <param name="e">イベント</param>
    void skiaView_Touch(object sender, SkiaSharp.Views.Maui.SKTouchEventArgs e)
    {
        var bindingContext = this.BindingContext as MainPageViewModel;

        if (e.InContact)
            touchLocation = e.Location;
        else
            touchLocation = null;

        skiaView1.InvalidateSurface();

        e.Handled = true;
    }
    #endregion
}

