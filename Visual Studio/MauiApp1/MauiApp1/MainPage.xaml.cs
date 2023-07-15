namespace MauiApp1;

#if IOS || ANDROID || MACCATALYST
using Microsoft.Maui.Graphics.Platform;
#elif WINDOWS
using Microsoft.Maui.Graphics.Win2D;
using System.Diagnostics;
using System.Net;
#endif


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

    // - プライベート・イベントハンドラ

    #region イベントハンドラ（［画像読込］ボタン・クリック時）
    /// <summary>
    ///		［画像読込］ボタン・クリック時
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    private void LoadImageButton_Clicked(object sender, EventArgs e)
    {
        var bindingContext = this.BindingContext as MainPageViewModel;

        // bindingContext.ImageFilePath

        //
        // 画像の読込
        // ==========
        //
        var task = Task.Run(() =>
        {
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
        });

    }
    #endregion
}

