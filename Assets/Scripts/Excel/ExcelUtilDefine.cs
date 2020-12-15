namespace KSM.Android.Utility
{
    #region style
    public enum HSSFColor
    {
        BLACK = 8,
        WHITE = 9,
        RED = 10,
        BRIGHT_GREEN = 11,
        BLUE = 12,
        YELLOW = 13,
        PINK = 14,
        TURQUOISE = 15,
        GREEN = 17,
        DARK_BLUE = 18,
        DARK_YELLOW = 19,
        VIOLET = 20,
        TEAL = 21,
        GREY_25_PERCENT = 22,
        GREY_50_PERCENT = 23,
        CORNFLOWER_BLUE = 24,
        MAROON = 25,
        LEMON_CHIFFON = 26,
        ORCHID = 28,
        CORAL = 29,
        ROYAL_BLUE = 30,
        LIGHT_CORNFLOWER_BLUE = 31,
        DARK_RED = 37,
        SKY_BLUE = 40,
        LIGHT_TURQUOISE = 41,
        LIGHT_GREEN = 42,
        LIGHT_YELLOW = 43,
        PALE_BLUE = 44,
        ROSE = 45,
        LAVENDER = 46,
        LIGHT_BLUE = 48,
        AQUA = 49,
        LIME = 50,
        GOLD = 51,
        LIGHT_ORANGE = 52,
        ORANGE = 53,
        BLUE_GREY = 54,
        GREY_40_PERCENT = 55,
        DARK_TEAL = 56,
        SEA_GRENN = 57,
        DARK_GREEN = 58,
        OLIVE_GREEN = 59,
        BROWN = 60,
        PLUM = 61,
        INDIGO = 62,
        GREY_80_PERCENT = 63
    }

    public enum HorizontalAlignment
    {
        ALIGN_GENERAL = 0,
        ALIGN_LEFT = 1,
        ALIGN_CENTER = 2,
        ALIGN_RIGHT = 3,
        ALIGN_FILL = 4,
        ALIGN_JUSTIFY = 5,
        ALIGN_CENTER_SELECTION = 6
    }

    public enum VerticalAlignment
    {
        VERTICAL_TOP = 0,
        VERTICAL_CENTER = 1,
        VERTICAL_BOTTOM = 2,
        VERTICAL_JUSTIFY = 3
    }

    public enum BorderStyle
    {
        BORDER_NONE = 0,
        BORDER_THIN = 1,
        BORDER_MEDIUM = 2,
        BORDER_DASHED = 3,
        BORDER_HAIR = 4,
        BORDER_THICK = 5,
        BORDER_DOUBLE = 6,
        BORDER_DOTTED = 7,
        BORDER_MEDIUM_DASHED = 8,
        BORDER_DASH_DOT = 9,
        BORDER_MEDIUM_DASH_DOT = 10,
        BORDER_DASH_DOT_DOT = 11,
        BORDER_MEDIUM_DASH_DOT_DOT = 12,
        BORDER_SLANTED_DASH_DOT = 13
    }

    public enum FillPaternType
    {
        NO_FILL = 0,
        SOLID_FOREGROUND = 1,
        FINE_DOTS = 2,
        ALT_BARS = 3,
        SPARSE_DOTS = 4,
        THICK_HORZ_BANDS = 5,
        THICK_VERT_BANDS = 6,
        THICK_BACKWARD_DIAG = 7,
        THICK_FORWARD_DIAG = 8,
        BIG_SPOTS = 9,
        BRICKS = 10,
        THIN_HORZ_BANDS = 11,
        THIN_VERT_BANDS = 12,
        THIN_BACKWARD_DIAG = 13,
        THIN_FORWARD_DIAG = 14,
        SQUARES = 15,
        DIAMONDS = 16,
        LESS_DOTS = 17,
        LEAST_DOTS = 18
    }

    public enum FontDecoration
    {
        Bold = 1 << 0,
        Underline = 1 << 1,
        Italic = 1 << 2,
        Strikeout = 1 << 3
    }

    public class CellStyle
    {
        public HorizontalAlignment horizontalAlignment { get; private set; }
        public VerticalAlignment verticalAlignment { get; private set; }

        public BorderStyle topBorderStyle { get; private set; }
        public BorderStyle bottomBorderStyle { get; private set; }
        public BorderStyle leftBorderStyle { get; private set; }
        public BorderStyle rightBorderStyle { get; private set; }

        public HSSFColor topBorderColor { get; private set; }
        public HSSFColor bottomBorderColor { get; private set; }
        public HSSFColor leftBorderColor { get; private set; }
        public HSSFColor rightBorderColor { get; private set; }

        public FillPaternType fillPaternType { get; private set; }

        public HSSFColor backgroundColor { get; private set; }
        public HSSFColor foregroundColor { get; private set; }

        public CellStyle()
        {
            horizontalAlignment = HorizontalAlignment.ALIGN_CENTER;
            verticalAlignment = VerticalAlignment.VERTICAL_CENTER;

            SetBorderColor(HSSFColor.BLACK);
            SetBorderStyle(BorderStyle.BORDER_THIN);

            fillPaternType = FillPaternType.NO_FILL;

            backgroundColor = HSSFColor.WHITE;
            foregroundColor = HSSFColor.WHITE;
        }

        public void SetHorizontalAlignment(HorizontalAlignment alignment)
        {
            this.horizontalAlignment = alignment;
        }

        public void SetVerticalAlignment(VerticalAlignment alignment)
        {
            this.verticalAlignment = alignment;
        }

        public void SetBorderColor(HSSFColor topBorderColor, HSSFColor bottomBorderColor, HSSFColor leftBorderColor, HSSFColor rightBorderColor)
        {
            this.topBorderColor = topBorderColor;
            this.bottomBorderColor = bottomBorderColor;
            this.leftBorderColor = leftBorderColor;
            this.rightBorderColor = rightBorderColor;
        }

        public void SetBorderColor(HSSFColor color)
        {
            this.topBorderColor = color;
            this.bottomBorderColor = color;
            this.leftBorderColor = color;
            this.rightBorderColor = color;
        }

        public void SetBorderStyle(BorderStyle topBorderStyle, BorderStyle bottomBorderStyle, BorderStyle leftBorderStyle, BorderStyle rightBorderStyle)
        {
            this.topBorderStyle = topBorderStyle;
            this.bottomBorderStyle = bottomBorderStyle;
            this.leftBorderStyle = leftBorderStyle;
            this.rightBorderStyle = rightBorderStyle;
        }

        public void SetBorderStyle(BorderStyle borderStyle)
        {
            this.topBorderStyle = borderStyle;
            this.bottomBorderStyle = borderStyle;
            this.leftBorderStyle = borderStyle;
            this.rightBorderStyle = borderStyle;
        }

        public void SetFillPattern(FillPaternType fillPaternType)
        {
            this.fillPaternType = fillPaternType;
        }

        public void SetBackgroundColor(HSSFColor color)
        {
            this.backgroundColor = color;
        }

        public void SetForegroundColor(HSSFColor color)
        {
            this.foregroundColor = color;
        }
    }

    public class FontStyle
    {
        /// <summary>
        /// font size [points]
        /// </summary>
        public short fontSize { get; private set; }

        public HSSFColor fontColor { get; private set; }

        public FontDecoration decoration { get; private set; }

        public FontStyle()
        {
            fontSize = 10;
            fontColor = HSSFColor.BLACK;
            decoration = 0;
        }

        public void SetFontSize(short points)
        {
            fontSize = points;
        }

        public void SetFontColor(HSSFColor color)
        {
            fontColor = color;
        }

        public void SetBold(bool isBold)
        {
            if (isBold)
                decoration |= FontDecoration.Bold;
            else
                decoration &= ~FontDecoration.Bold;
        }

        public void SetItalic(bool isItalic)
        {
            if (isItalic)
                decoration |= FontDecoration.Italic;
            else
                decoration &= ~FontDecoration.Italic;
        }

        public void SetUnderline(bool isUnderline)
        {
            if (isUnderline)
                decoration |= FontDecoration.Underline;
            else
                decoration &= ~FontDecoration.Underline;
        }

        public void SetStrikeThrough(bool isStrike)
        {
            if (isStrike)
                decoration |= FontDecoration.Strikeout;
            else
                decoration &= FontDecoration.Strikeout;
        }
    }
    #endregion

    #region workbook

    public enum PICTURE_TYPE
    {
        EMF = 2,
        WMF = 3,
        PICT = 4,
        JPEG = 5,
        PNG = 6,
        DIB = 7
    }

    #endregion

    #region Header

    public enum HEADER_POS
    {
        LEFT = 0,
        CENTER = 1,
        RIGHT = 2
    }

    #endregion
}
