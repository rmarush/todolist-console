using OfficeOpenXml;
using OfficeOpenXml.Style;
using todolist_console.Enums;
using todolist_console.Models;
using todolist_console.Utils;

namespace todolist_console.Services
{
    public class ExcelService
    {
        const string ColumnStatus = "A";
        const string ColumnName = "B";
        public static async Task CreateExcelTable(DoublyLinkedList<Tasks> tasks, string filePath)
        {
            ExcelPackage.LicenseContext = LicenseContext.NonCommercial;

            using var package = new ExcelPackage(new FileInfo(filePath));
            var worksheet = package.Workbook.Worksheets.Add("To-Do-List");
            var rowNumber = 1;
            IEnumerable<TasksStatus> uniqueStatuses = tasks.Select(task => task.Status).Distinct().OrderBy(status => status);
            if (uniqueStatuses == null || !uniqueStatuses.Any())
            {
                worksheet.Cells[ColumnStatus + rowNumber.ToString()].Value = "You have no task!";
            }
            else
            {
                foreach (var status in uniqueStatuses)
                {
                    CreateStatusRow(worksheet, ref rowNumber, status);
                    var count = 0;
                    var tasksWithStatus = tasks.Where(task => task.Status == status);
                    foreach (var task in tasksWithStatus)
                    {
                        CreateTaskRow(worksheet, ref rowNumber, task.Title);
                        count++;
                    }
                    CreateTaskRow(worksheet, ref rowNumber, $"Count: {count.ToString()}");
                }
            }
            await package.SaveAsync();
        }

        private static void CreateStatusRow(ExcelWorksheet worksheet, ref int rowNumber, TasksStatus status)
        {
            worksheet.Cells[ColumnStatus + rowNumber.ToString() + ":" + ColumnName + rowNumber.ToString()].Merge = true;
            SetStyles(worksheet.Cells[ColumnStatus + rowNumber.ToString() + ":" + ColumnName + rowNumber.ToString()]);
            var statusCell = worksheet.Cells[ColumnStatus + rowNumber.ToString()];
            statusCell.Value = status;
            statusCell.Style.Font.Bold = true;
            statusCell.Style.Fill.PatternType = ExcelFillStyle.Solid;
            statusCell.Style.Fill.BackgroundColor.SetColor(System.Drawing.Color.Aqua);
            rowNumber++;
        }

        private static void CreateTaskRow(ExcelWorksheet worksheet, ref int rowNumber, string taskTitle)
        {
            worksheet.Cells[ColumnStatus + rowNumber.ToString() + ":" + ColumnName + rowNumber.ToString()].Merge = true;
            SetStyles(worksheet.Cells[ColumnStatus + rowNumber.ToString() + ":" + ColumnName + rowNumber.ToString()]);
            var taskName = worksheet.Cells[ColumnStatus + (rowNumber++).ToString()];
            taskName.Value = taskTitle;
        }

        private static void SetStyles(ExcelRange? cell)
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
