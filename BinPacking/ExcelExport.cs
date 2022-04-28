using Excel = Microsoft.Office.Interop.Excel;

namespace BinPacking
{
    internal class ExcelExport
    {
        string path;

        public ExcelExport()
        {
            path = "C:\\Users\\Ilya\\YandexDisk\\USATU\\3_Course\\" +
                "6_Semester\\ComputerModelling\\labs\\";
        }

        public ExcelExport(string _path)
        {
            path = _path;
        }

        public void Save(Dictionary<string, int[]> dict, string name)
        {
            //Объявляем приложение
            Excel.Application ex = new Excel.Application();
            //Отобразить Excel
            ex.Visible = true;
            //Количество листов в рабочей книге
            ex.SheetsInNewWorkbook = dict.Count;
            //Добавить рабочую книгу
            Excel.Workbook workBook = ex.Workbooks.Add(Type.Missing);
            //Отключить отображение окон с сообщениями
            ex.DisplayAlerts = false;
            int i = 1;
            foreach (var vi in dict)
            {
                CellsFill(ex, vi, i++);
            }

            ex.Application.ActiveWorkbook.SaveAs(path + $"{name}.xlsx");
        }

        void CellsFill(Excel.Application ex, KeyValuePair<string, int[]> v, int n)
        {
            Excel.Worksheet sheet = (Excel.Worksheet)ex.Worksheets.get_Item(n);
            //Название листа (вкладки снизу)
            sheet.Name = v.Key;
            //Пример заполнения ячеек
            for (int j = 1; j <= v.Value.Length; j++)
                sheet.Cells[1, j] = v.Value[j - 1];
        }
    }
}
