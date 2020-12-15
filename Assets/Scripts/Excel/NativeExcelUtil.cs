namespace KSM.Android.Utility
{
    using System.Collections.Generic;
    using System.IO;
    using UnityEngine;

    public static class NativeExcelUtil
    {
        // If you need other functions, see https://poi.apache.org/apidocs/dev/

        private static AndroidJavaObject workbook;

        private static AndroidJavaObject worksheet;

        private static AndroidJavaObject patriarch;

        private static List<AndroidJavaObject> styles = new List<AndroidJavaObject>();

        private static AndroidJavaObject[,] dataTable = null;
        public static int tableRowCount;
        public static int tableColumnCount;
        
        public static void CreateWorkbook()
        {
            workbook = new AndroidJavaObject("org.apache.poi.hssf.usermodel.HSSFWorkbook");
            if(workbook == null)
            {
                Debug.LogError($"{nameof(workbook)} is null! Please check whether you import poi library.");
            }
        }
        public static void CreateWorksheet(string sheetName)
        {
            worksheet = workbook.Call<AndroidJavaObject>("createSheet", sheetName);
            patriarch = worksheet.Call<AndroidJavaObject>("createDrawingPatriarch");
        }

        /// <summary>
        /// Create Cell Style.
        /// <para/>Call this function and access with <see cref="NativeExcelUtil.styles"/>
        /// </summary>
        /// <param name="cellStyle">Cell style</param>
        /// <param name="fontStyle">Font style</param>
        public static void CreateStyle(CellStyle cellStyle, FontStyle fontStyle)
        {
            AndroidJavaObject styleObj = workbook.Call<AndroidJavaObject>("createCellStyle");

            styleObj.Call("setAlignment", (short)cellStyle.horizontalAlignment);
            styleObj.Call("setVerticalAlignment", (short)cellStyle.verticalAlignment);
            styleObj.Call("setBorderTop", (short)cellStyle.topBorderStyle);
            styleObj.Call("setBorderBottom", (short)cellStyle.bottomBorderStyle);
            styleObj.Call("setBorderLeft", (short)cellStyle.leftBorderStyle);
            styleObj.Call("setBorderRight", (short)cellStyle.rightBorderStyle);
            styleObj.Call("setTopBorderColor", (short)cellStyle.topBorderColor);
            styleObj.Call("setBottomBorderColor", (short)cellStyle.bottomBorderColor);
            styleObj.Call("setLeftBorderColor", (short)cellStyle.leftBorderColor);
            styleObj.Call("setRightBorderColor", (short)cellStyle.rightBorderColor);
            styleObj.Call("setFillPattern", (short)cellStyle.fillPaternType);
            styleObj.Call("setFillBackgroundColor", (short)cellStyle.backgroundColor);
            styleObj.Call("setFillForegroundColor", (short)cellStyle.foregroundColor);

            AndroidJavaObject fontObj = workbook.Call<AndroidJavaObject>("createFont");

            fontObj.Call("setColor", (short)fontStyle.fontColor);
            fontObj.Call("setFontHeightInPoints", (short)fontStyle.fontSize);
            
            if(fontStyle.decoration.HasFlag(FontDecoration.Bold))
            {
                fontObj.Call("setBoldweight", (short)700);
            }
            else
            {
                fontObj.Call("setBoldweight", (short)400);
            }
            fontObj.Call("setItalic", fontStyle.decoration.HasFlag(FontDecoration.Italic));
            fontObj.Call("setStrikeout", fontStyle.decoration.HasFlag(FontDecoration.Strikeout));
            fontObj.Call("setUnderline", fontStyle.decoration.HasFlag(FontDecoration.Underline) ? (byte)1 : (byte)0);

            styleObj.Call("setFont", fontObj);

            styles.Add(styleObj);
        }

        /// <summary>
        /// Initialize Data Table.
        /// <para/>It might be better to work with <see cref="NativeExcelUtil.dataTable"/> for enhancing performance.
        /// </summary>
        /// <param name="rowCount"></param>
        /// <param name="columnCount"></param>
        /// <returns></returns>
        public static void InitDataTable(int rowCount, int columnCount)
        {
            tableRowCount = rowCount;
            tableColumnCount = columnCount;

            dataTable = new AndroidJavaObject[rowCount, columnCount];
            for(int i = 0; i < rowCount; ++i)
            {
                AndroidJavaObject tmpRow = worksheet.Call<AndroidJavaObject>("createRow", i);
                for (short j = 0; j < columnCount; ++j)
                {
                    dataTable[i, j] = tmpRow.Call<AndroidJavaObject>("createCell", j);
                }
            }
        }

        public static void SetColumnWidth(short columnIndex, short width)
        {
            //if (columnIndex < 0 || columnIndex >= tableColumnCount) return;
            try
            {
                worksheet.Call("setColumnWidth", columnIndex, width);
            }
            catch (System.Exception e)
            {
                Debug.LogError(e);
            }
        }

        public static void SetRowHeight(int rowIndex, short height)
        {
            //if (rowIndex < 0 || rowIndex >= tableRowCount) return;
            try
            {
                worksheet.Call<AndroidJavaObject>("getRow", rowIndex).Call("setHeight", height);
            }
            catch(System.Exception e)
            {
                Debug.LogError(e);
            }
        }

        public static string GetCellValue(int rowIndex, int columnIndex)
        {
            try 
            { 
                return dataTable[rowIndex, columnIndex].Call<AndroidJavaObject>("getRichStringCellValue").Call<string>("getString");
            }
            catch(System.Exception e)
            {
                Debug.LogError(e);
                return "";
            }
        }

        public static void SetCellValue(int rowIndex, int columnIndex, string value)
        {
            try
            {
                AndroidJavaObject HSSFRichTextString = new AndroidJavaObject("org.apache.poi.hssf.usermodel.HSSFRichTextString", value);
                dataTable[rowIndex, columnIndex].Call("setCellValue", HSSFRichTextString);
            }
            catch(System.Exception e)
            {
                Debug.LogError(e);
            }
        }

        public static void SetCellStyle(int rowIndex, int columnIndex, int styleIndex)
        {
            //if (rowIndex < 0 || rowIndex >= tableRowCount || columnIndex < 0 || columnIndex >= tableColumnCount)
            //{
            //    Debug.LogError($"OutOfIndex: {nameof(dataTable)}'s row({tableRowCount}), col({tableColumnCount})\nBut you access to row({rowIndex}), col({columnIndex})");
            //    return;
            //}

            //if(styleIndex < 0 || styleIndex >= styles.Count)
            //{
            //    Debug.LogError($"OutOfIndex: {nameof(styles)}'s Count({styles.Count}\nBut you access to {styleIndex}.");
            //    return;
            //}

            // The exception is very rare when you use this function carefully.
            // And also, try-catch may not hurt the performance with this small scale.
            // note that throw cause the lack of performance.
            // ref: https://stackoverflow.com/questions/1308432/do-try-catch-blocks-hurt-performance-when-exceptions-are-not-thrown
            try
            {
                dataTable[rowIndex, columnIndex].Call("setCellStyle", styles[styleIndex]);
            }
            catch(System.Exception e)
            {
                Debug.LogError(e);
            }
        }

        /// <summary>
        /// Merge Sheet with indexes
        /// </summary>
        /// <param name="rowFrom"></param>
        /// <param name="columnFrom"></param>
        /// <param name="rowTo"></param>
        /// <param name="columnTo"></param>
        /// <returns># of function calls</returns>
        public static int MergeRegion(int rowFrom, short columnFrom, int rowTo, short columnTo)
        {
            try
            {
                AndroidJavaObject regionObj = new AndroidJavaObject("org.apache.poi.hssf.util.Region", rowFrom, columnFrom, rowTo, columnTo);
                return worksheet.Call<int>("addMergedRegion", regionObj);
            }
            catch(System.Exception e)
            {
                Debug.LogError(e);
                return 0;
            }
        }

        public static void AddImage(byte[] data, PICTURE_TYPE type, int rowFrom, short columnFrom, int rowTo, short columnTo)
        {
            AndroidJavaObject anchor = new AndroidJavaObject("org.apache.poi.hssf.usermodel.HSSFClientAnchor", 0, 0, 0, 0, columnFrom, rowFrom, columnTo, rowTo);

            anchor.Call("setAnchorType", 2);

            int pictureIndex = workbook.Call<int>("addPicture", data, (int)type);

            patriarch.Call<AndroidJavaObject>("createPicture", anchor, pictureIndex);
        }

        /// <summary>
        /// Set header text with Tags (&amp;)
        /// <para/>to get info for tags, see https://xlsxwriter.readthedocs.io/page_setup.html
        /// </summary>
        /// <param name="text"></param>
        public static void SetHeader(string text)
        {
            AndroidJavaObject header = worksheet.Call<AndroidJavaObject>("getHeader");

            header.Call("setCenter", text);
        }

        /// <summary>
        /// Set header text with Tags (&amp;)
        /// <para/>to get info for tags, see https://xlsxwriter.readthedocs.io/page_setup.html
        /// </summary>
        /// <param name="text"></param>
        public static void SetFooter(string text)
        {
            AndroidJavaObject header = worksheet.Call<AndroidJavaObject>("getFooter");

            header.Call("setCenter", text);
        }

        public static void Save(string fileName)
        {
            try
            {
                AndroidJavaObject fileObj = new AndroidJavaObject("java.io.File", fileName);

                AndroidJavaObject fileOutputStreamObj = new AndroidJavaObject("java.io.FileOutputStream", fileObj);

                workbook.Call("write", fileOutputStreamObj);

                fileOutputStreamObj.Call("close");

                Dispose();
            }
            catch (System.Exception e)
            {
                Debug.LogError($"Fail to save file {fileName}.\nSee: {e}");
            }
        }

        /// <summary>
        /// Read excel file at <paramref name="fileName"/> and save data to <see cref="dataTable"/>
        /// <para/> Use caching when you need to access data frequently.
        /// </summary>
        /// <param name="fileName"></param>
        /// <param name="sheetIndex">index of sheet</param>
        public static void Read(string fileName, int sheetIndex = 0)
        {
            AndroidJavaObject fileInputStreamObj = new AndroidJavaObject("java.io.FileInputStream", fileName);

            AndroidJavaObject workbookObj = new AndroidJavaObject("org.apache.poi.hssf.usermodel.HSSFWorkbook", fileInputStreamObj);

            AndroidJavaObject worksheetObj = workbookObj.Call<AndroidJavaObject>("getSheetAt", sheetIndex);

            AndroidJavaObject tmpRow = worksheetObj.Call<AndroidJavaObject>("getRow", 0);

            int rowCount = worksheet.Call<int>("getPhysicalNumberOfRows");
            int columnCount = tmpRow.Call<int>("getPhysicalNumberOfCells");

            dataTable = new AndroidJavaObject[rowCount, columnCount];

            for(int i = 0; i < rowCount; ++i)
            {
                tmpRow = worksheetObj.Call<AndroidJavaObject>("getRow", i);
                for(short j = 0; j < columnCount; ++j)
                {
                    dataTable[i, j] = tmpRow.Call<AndroidJavaObject>("getCell", j);
                }
            }
        }

        private static void Dispose()
        {
            worksheet.Dispose();
            worksheet = null;
            workbook.Dispose();
            workbook = null;
            patriarch.Dispose();
            patriarch = null;
            foreach(var style in styles)
            {
                style.Dispose();
            }
            styles = new List<AndroidJavaObject>();
            foreach(var data in dataTable)
            {
                data.Dispose();
            }
            dataTable = null;
        }

        #region Helper Func, private

        private static PICTURE_TYPE GetPictureType(string extension)
        {
            extension = extension.ToLower();
            switch(extension)
            {
                case ".png":
                    return PICTURE_TYPE.PNG;
                case ".jpg":
                case ".jpeg":
                    return PICTURE_TYPE.JPEG;
                case ".emf":
                    return PICTURE_TYPE.EMF;
                case ".bmp":
                case ".dib":
                    return PICTURE_TYPE.DIB;
                case ".pict":
                case ".pct":
                case ".pic":
                    return PICTURE_TYPE.PICT;
                case ".wmf":
                    return PICTURE_TYPE.WMF;
                default:
                    return PICTURE_TYPE.PNG;
            }
        }

        #endregion
    }
}