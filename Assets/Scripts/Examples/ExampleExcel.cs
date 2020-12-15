namespace KSM.Android.Utility.Example
{
    using UnityEngine;
    using UnityEngine.UI;
    using static KSM.Android.Utility.NativeExcelUtil;

    public class ExampleExcel : ExampleBase
    {
        public Button makeExcelButton;

        private void Awake()
        {
#if UNITY_ANDROID
            if(!Application.isEditor)
                makeExcelButton.onClick.AddListener(MakeExcel);
#endif
        }

        private void MakeExcel()
        {
            CreateWorkbook();

            CreateWorksheet("Test");

            InitDataTable(5, 3);

            CellStyle cellStyle = new CellStyle();
            //cellStyle.SetBorderColor(HSSFColor.BLUE);
            //cellStyle.SetBorderStyle(BorderStyle.BORDER_THIN);
            //cellStyle.SetHorizontalAlignment(HorizontalAlignment.ALIGN_CENTER);
            //cellStyle.SetVerticalAlignment(VerticalAlignment.VERTICAL_CENTER);

            Android.Utility.FontStyle fontStyle = new Android.Utility.FontStyle();

            CreateStyle(cellStyle, fontStyle);

            for (int i = 0; i < tableRowCount; ++i)
            {
                for (int j = 0; j < tableColumnCount; ++j)
                {
                    SetCellStyle(i, j, 0);
                }
            }

            SetCellValue(0, 0, "hello");
            SetCellValue(0, 1, "world");
            SetCellValue(0, 2, "^^");

            MergeRegion(0, 0, 2, 0);

            var sprite = Resources.Load<Sprite>("test");
            var tex = sprite.texture;

            byte[] data = tex.EncodeToPNG();

            AddImage(data, PICTURE_TYPE.PNG, 3, 0, 4, 1);
            AddImage(data, PICTURE_TYPE.PNG, 3, 2, 4, 3);

            SetHeader("&LLeft&RRight&CCenter");

            Save("/storage/emulated/0/SamKim/test.xls");

            NativeFileUtil.ViewFile("/storage/emulated/0/SamKim/test.xls");
        }
    }
}