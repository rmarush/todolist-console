using OfficeOpenXml;
using OfficeOpenXml.Style;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using todolist_console.Enums;
using todolist_console.Models;

namespace todolist_console.Services
{
    internal class ExcelService
    {
        public static async Task CreateExcelTable(List<Tasks> tasks, string filePath)
        {
            ExcelPackage.LicenseContext = LicenseContext.NonCommercial;

            using (var package = new ExcelPackage(new FileInfo(filePath)))
            {
                var worksheet = package.Workbook.Worksheets.Add("To-Do-List");
                int rowNumber = 1;
                string columnStatus = "A";
                string columnName = "B";
                IEnumerable<TasksStatus> uniqueStatuses = tasks.Select(task => task.Status).Distinct().OrderBy(status => status);
                if (uniqueStatuses == null || !uniqueStatuses.Any())
                {
                    worksheet.Cells[columnStatus + rowNumber.ToString()].Value = "You have no task!";
                }
                else
                {
                    foreach (var status in uniqueStatuses)
                    {
                        worksheet.Cells[columnStatus + rowNumber.ToString() + ":" + columnName + rowNumber.ToString()].Merge = true;
                        SetStyles(worksheet.Cells[columnStatus + rowNumber.ToString() + ":" + columnName + rowNumber.ToString()]);
                        var statusCell = worksheet.Cells[columnStatus + rowNumber.ToString()];
                        statusCell.Value = status;
                        statusCell.Style.Font.Bold = true;
                        statusCell.Style.Fill.PatternType = ExcelFillStyle.Solid;
                        statusCell.Style.Fill.BackgroundColor.SetColor(System.Drawing.Color.Aqua);
                        rowNumber++;
                        var tasksWithStatus = tasks.Where(task => task.Status == status);
                        foreach (var task in tasksWithStatus)
                        {
                            worksheet.Cells[columnStatus + rowNumber.ToString() + ":" + columnName + rowNumber.ToString()].Merge = true;
                            SetStyles(worksheet.Cells[columnStatus + rowNumber.ToString() + ":" + columnName + rowNumber.ToString()]);
                            var taskName = worksheet.Cells[columnStatus + (rowNumber++).ToString()];
                            taskName.Value = task.Title;
                        }
                    }
                }
                await package.SaveAsync();
            }
        }
        public static void SetStyles(ExcelRange? cell)
        {
            cell.Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
            cell.Style.Font.Size = 14;
            cell.Style.Border.Top.Style = ExcelBorderStyle.Thin;
            cell.Style.Border.Bottom.Style = ExcelBorderStyle.Thin;
            cell.Style.Border.Left.Style = ExcelBorderStyle.Thin;
            cell.Style.Border.Right.Style = ExcelBorderStyle.Thin;
        }
    }
}
