using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Service.DAL.Models;
using OfficeOpenXml;
namespace Service.Api.Repository
{
    public interface IExcel
    {
        //возвращае данные колонок col1..col20 в формате таблицы Sheet для сохранения их в БД,
        //данные берутся с листа Excel под индексом Index
        IBaseSheet LoadFromSheet(ExcelPackage ep, int Index);
    }
    public class Excel : IExcel
    {
        public IBaseSheet LoadFromSheet(ExcelPackage ep, int Index)
        {
            ExcelWorksheet ws = ep.Workbook.Worksheets[Index];
            Service.DAL.Models.BaseSheet sheet = new Service.DAL.Models.BaseSheet();
            sheet.Created = DateTime.Now;
            for (int i = 1; i <= 20; i++) sheet[string.Format("Col{0}", i)] = ws.Cells[1, i].Text;
            return sheet;
        }
    }
}
