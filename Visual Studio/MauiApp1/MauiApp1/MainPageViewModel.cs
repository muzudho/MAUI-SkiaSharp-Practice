namespace MauiApp1
{
    using CommunityToolkit.Mvvm.ComponentModel;
    using SkiaSharp;
    using TheGraphics = Microsoft.Maui.Graphics;

    /// <summary>
    ///     ビューモデル
    ///     
    ///     <list type="bullet">
    ///         <item>CommunityToolkit.Mvvm パッケージを NuGET でインストールしてほしい</item>
    ///     </list>
    /// </summary>
    public class MainPageViewModel : ObservableObject
    {
        // - パブリック・プロパティ

        #region プロパティ（画像ファイルパス）
        string imageFilePath = @"C:\Users\むずでょ\Documents\GitHub\MAUI-SkiaSharp-Practice\Workspace\adventure_field.png";

        /// <summary>
        ///     画像ファイルパス
        /// </summary>
        public string ImageFilePath
        {
            get => this.imageFilePath;
            set
            {
                if (this.imageFilePath!=value)
                {
                    this.imageFilePath = value;
                    this.OnPropertyChanged(nameof(ImageFilePath));
                }
            }
        }
        #endregion

        #region プロパティ（画像）
        TheGraphics.IImage image;

        /// <summary>
        ///     画像
        /// </summary>
        public TheGraphics.IImage Image
        {
            get => this.image;
            set
            {
                if (this.image != value)
                {
                    this.image = value;
                    this.OnPropertyChanged(nameof(Image));
                }
            }
        }
        #endregion

        #region プロパティ（画像。ビットマップ形式）
        SKBitmap skBitmap;

        /// <summary>
        ///     画像。ビットマップ形式
        /// </summary>
        public SKBitmap SKBitmap
        {
            get => this.skBitmap;
            set
            {
                if (this.skBitmap != value)
                {
                    this.skBitmap = value;
                    this.OnPropertyChanged(nameof(SKBitmap));
                }
            }
        }
        #endregion
    }
}
