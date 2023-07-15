namespace MauiApp1
{
    using TheGraphics = Microsoft.Maui.Graphics;

    /// <summary>
    ///     ２Ｄ画像の描画
    /// </summary>
    internal class SimpleDrawing : BindableObject, IDrawable
    {
        // - パブリック・プロパティ

        #region 束縛可能プロパティ（ソース画像）
        /// <summary>
        ///     ソース画像
        /// </summary>
        public TheGraphics.IImage SourceImage
        {
            get => (TheGraphics.IImage)GetValue(SourceImageProperty);
            set => SetValue(SourceImageProperty, value);
        }

        /// <summary>
        ///     タイル・カーソルの線の半分の太さ
        /// </summary>
        public static BindableProperty SourceImageProperty = BindableProperty.Create(
            // プロパティ名
            propertyName: nameof(SourceImage),
            // 返却型
            returnType: typeof(TheGraphics.IImage),
            // これを含んでいるクラス
            declaringType: typeof(SimpleDrawing));
        #endregion

        // - パブリック・メソッド

        #region メソッド（描画）
        /// <summary>
        ///     描画
        /// </summary>
        /// <param name="canvas">描画領域</param>
        /// <param name="dirtyRect">位置とサイズ</param>
        public void Draw(ICanvas canvas, RectF dirtyRect)
        {
            // 画像を描く
            canvas.DrawImage(this.SourceImage, 0, 0, 64, 64);

            // 矩形を塗りつぶす
            canvas.FillColor = new Color(220, 220, 255, 96);
            canvas.FillRectangle(40, 50, 20, 20);

            // 矩形を引く
            canvas.StrokeColor = Colors.Green;
            canvas.StrokeSize = 2.0f;
            canvas.DrawRectangle(40, 50, 20, 20);

            // 線を引く
            canvas.StrokeColor = Colors.Red;
            canvas.StrokeSize = 4.0f;
            canvas.DrawLine(0, 0, 100, 100);
        }
        #endregion
    }
}
