using Microsoft.AspNetCore.Mvc;
using NPOI.HSSF.UserModel;
using NPOI.SS.UserModel;
using NPOI.SS.Util;
using NPOI.XSSF.UserModel;
using System.ComponentModel;
using System.Data;
using System.Reflection;

namespace UtilityToolkit.Utils
{
    /// <summary>
    /// Execl生成返回类
    /// </summary>
    public static class ExeclHelper
    {
        #region 生成Execl
        /// <summary>
        /// 导出execl
        /// </summary>
        /// <typeparam name="OutPutT">导出的execl内的格式的数据</typeparam>
        /// <param name="list"></param>
        /// <remarks>
        /// </remarks>
        /// <returns></returns>
        public static FileStreamResult ExportTrainingSignUpUserExecl<OutPutT>(List<OutPutT> list)
        {
            // 得到数据源
            IWorkbook workBook = new HSSFWorkbook();
            string downLoadName = DateTime.Now.ToString("yyyyMMddHHmmss") + "." + "xslx";

            #region 样式
            // 设置表头单元格格式
            ICellStyle titleCell = workBook.CrateTitleCell();
            // 设置数据列表样式
            ICellStyle dataCell = workBook.CrateDataCell();
            // 获取需要导出的列名
            var descriptions = typeof(OutPutT).GetProperties().Where(p => p.GetCustomAttributes<DescriptionAttribute>() != null).Select(p => new
            {
                DescriptionValue = p.GetCustomAttribute<DescriptionAttribute>().Description,
                p.Name
            }).ToList();
            #endregion

            // 创建一个内存
            var ms = new MemoryStream();
            // 创建一个工作簿
            ISheet sheet = workBook.CreateSheet();
            // 新建一行
            IRow firstRow = sheet.CreateRow(0);
            int cellIndex = 0;
            // 设置这一行的高度
            firstRow.HeightInPoints = 40;
            // 先创建列名
            ICell celltitle = firstRow.CreateCell(0);
            descriptions.ForEach(item =>
            {
                celltitle.CellStyle = titleCell;
                celltitle.SetCellValue(item.DescriptionValue);
            });
            // 当没有数据的时候 渲染一个表头就返回
            if (list.Count == 0)
            {
                return WorkBookWrite(ms, workBook, downLoadName);
            }
            DataTable table = list.ToDataTable("测试");
            //循环生成每一行
            for (int k = 0; k < table.Rows.Count; k++)
            {
                //生成行
                IRow rowData = sheet.CreateRow(k + 1);
                rowData.HeightInPoints = 25;//设置行高
                int cellnum = 0;//设置单元格
                for (int h = 0; h < table.Columns.Count; h++)
                {
                    ICell cellData = rowData.CreateCell(cellnum);
                    string value = table.Rows[k][h].ToString();//拿到数据
                    cellData.SetCellValue(value);
                    cellData.CellStyle = dataCell;
                    cellnum++;
                }
            }
            return WorkBookWrite(ms, workBook, downLoadName);
        }

        /// <summary>
        /// 导出execl文件流
        /// </summary>
        /// <param name="rowNames">列名集合</param>
        /// <param name="data"></param>
        /// <param name="sheetRowNum"></param>
        /// <param name="imagecolnums"></param>
        /// <returns></returns>
        public static FileStreamResult ReturnExeclFileStreamTest(List<ExeclModel> rowNames, DataTable data, int sheetRowNum, string imagecolnums = "")
        {
            // 判断版本
            string version = "xlsx";
            string downLoadName = DateTime.Now.ToString("yyyyMMddHHmmss") + "." + version;
            // 创造一个工作簿
            IWorkbook workBook = version == "xlsx" ? new XSSFWorkbook() : new HSSFWorkbook();
            // 获取需要渲染的工作簿数量
            int sheetNum = data.Rows.Count % sheetRowNum == 0 ? data.Rows.Count / sheetRowNum : data.Rows.Count / sheetRowNum + 1;
            // 获取列名
            string[] showFields = rowNames.Select(x => x.Name).ToArray();
            // 设置表头单元格格式
            ICellStyle titleCell = workBook.CrateTitleCell();
            // 设置数据列表样式
            ICellStyle dataCell = workBook.CrateDataCell();
            // 创建一个内存流
            MemoryStream ms = new MemoryStream();
            // 当没有数据的时候 渲染一个表头就返回
            if (sheetNum == 0)
            {
                ISheet sheet = workBook.CreateSheet("Sheet1");
                IRow row = sheet.CreateRow(0);
                ICell celltitle = row.CreateCell(0);
                rowNames.ForEach(item =>
                {
                    celltitle.CellStyle = titleCell;
                    celltitle.SetCellValue(item.Description);
                });
                return WorkBookWrite(ms, workBook, downLoadName);
            }
            // 有数据的话看需要渲染多少个工作簿
            for (int i = 0; i < sheetNum; i++)
            {
                // 每个表格一共多少列
                int Colnum = data.Columns.Count;
                // 如果数据的条数能正常塞满一个工作簿的话
                int Rownum = data.Rows.Count % sheetRowNum != 0 && i == sheetNum - 1 ? data.Rows.Count % sheetRowNum : sheetRowNum;
                ISheet sheet = workBook.CreateSheet($"Sheet{i + 1}");
                IRow row = sheet.CreateRow(0);
                // 设置这一行的高度
                row.HeightInPoints = 40;
                int clonums = 0;
                //生成列名
                for (int j = 0; j < Colnum; ++j)
                {
                    // 只渲染数据表格中存在的列名
                    if (!showFields.Contains(data.Columns[j].ColumnName))
                    {
                        continue;
                    }
                    // 循环渲染这一行每一个列名单元格的数据
                    // 创建表头单元格
                    ICell cellTitle = row.CreateCell(clonums);
                    // 设置表头单元格样式
                    cellTitle.CellStyle = titleCell;
                    // 填充名字
                    cellTitle.SetCellValue(GetDescription(rowNames, data.Columns[j].ColumnName));
                    clonums++;
                }
                // 生成数据
                string[] imagecolnum = imagecolnums.Split(",");
                // 循环生成每一行
                for (int k = 0; k < Rownum; k++)
                {
                    //生成行
                    IRow rowData = sheet.CreateRow(k + 1);
                    rowData.HeightInPoints = 25;// 设置行高
                    int cellnum = 0;// 设置单元格
                    for (int h = 0; h < Colnum; h++)
                    {
                        // 只渲染数据表格中存在的列名   
                        if (!showFields.Contains(data.Columns[h].ColumnName)) continue;
                        // 循环渲染这一行每一个列名单元格的数据                                                                
                        ICell cellData = rowData.CreateCell(cellnum);
                        // 拿到datetable里面的这一行的这一格子数据
                        // 如果有需要转换的图片                
                        if (!imagecolnums.IsNullOrEmpty() && imagecolnum.Contains(data.Columns[h].ColumnName))
                        {
                            cellData.CellStyle = dataCell;
                            if (!data.Rows[i * sheetRowNum + k][h].ToString().IsNullOrEmpty())
                            {
                                // 获取图片byte                                          
                                byte[] bytes = Convert.FromBase64String(((byte[])data.Rows[i * sheetRowNum + k][h]).BytesToString().Split(',')[1]);
                                int pictureIdx = workBook.AddPicture(bytes, PictureType.JPEG);
                                var drawing = sheet.CreateDrawingPatriarch();
                                XSSFClientAnchor anchor = new XSSFClientAnchor(0, 0, 0, 0, cellnum, k + 1, cellnum + 1, k + 2);
                                drawing.CreatePicture(anchor, pictureIdx);
                            }

                        }
                        else
                        {
                            string value = data.Rows[i * sheetRowNum + k][h].ToString();//拿到数据
                            cellData.SetCellValue(value);
                            cellData.CellStyle = dataCell;
                        }
                        cellnum++;
                    }
                }
            }
            return WorkBookWrite(ms, workBook, downLoadName);
        }
        #endregion

        #region Execl转其他数据格式
        /// <summary>
        /// Execl转List
        /// </summary>
        /// <param name="fileStream"></param>
        /// <param name="rowRange"></param>
        /// <param name="excelExtend"></param>
        /// <param name="sheetName"></param>
        /// <returns></returns>
        public static List<List<string>> ExcelToList(Stream fileStream, int rowRange = 1, string excelExtend = ".xlsx", string sheetName = null, string imagecolnums = "")
        {
            List<List<string>> lstResult = new List<List<string>>();
            ISheet sheet = null;
            IWorkbook workbook = null;
            try
            {
                IWorkbook work = excelExtend.EqualIgnoreCase(".xlsx") ? new XSSFWorkbook(fileStream) : new HSSFWorkbook(fileStream);
                if (sheetName != null)
                {
                    sheet = workbook.GetSheet(sheetName);
                    // 如果没有找到指定的sheetName对应的sheet，则尝试获取第一个sheet
                    if (sheet == null)
                    {
                        sheet = workbook.GetSheetAt(0);
                    }
                }
                else
                {
                    sheet = workbook.GetSheetAt(0);
                }
                if (sheet != null)
                {
                    for (int i = 0; i < rowRange; i++)
                    {
                        IRow row = sheet.GetRow(0);
                        if (row.LastCellNum != 0)
                        {
                            List<string> list = new List<string>();
                            for (int j = row.FirstCellNum; j < row.LastCellNum; ++j)
                            {
                                list.Add(row.GetCell(j).ToString());
                            }
                            lstResult.Add(list);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                workbook?.Close();
            }
            return lstResult;
        }


        /// <summary>
        /// Execl转DateTable
        /// </summary>
        /// <param name="stream"></param>
        /// <param name="sheetName"></param>
        /// <param name="fileExtend"></param>
        /// <param name="isFirstRowColumn"></param>
        /// <returns></returns>
        public static DataTable ExcelToDataTable(Stream stream, string sheetName = null, string fileExtend = ".xlsx", bool isFirstRowColumn = true)
        {
            ISheet sheet = null;
            DataTable data = new DataTable();
            IWorkbook workbook = null;
            int startRow = 0;
            try
            {
                workbook = fileExtend.Equals(".xlsx") ? new XSSFWorkbook(stream) : new HSSFWorkbook(stream);
                if (sheetName != null)
                {
                    sheet = workbook.GetSheet(sheetName);
                    // 如果没有找到指定的sheetName对应的sheet，则尝试获取第一个sheet
                    if (sheet == null)
                    {
                        sheet = workbook.GetSheetAt(0);
                    }
                }
                else
                {
                    sheet = workbook.GetSheetAt(0);
                }
                if (sheet != null)
                {
                    IRow firstRow = sheet.GetRow(0);
                    // 一行最后一个cell的编号 即总的列数
                    int cellCount = firstRow.LastCellNum;
                    if (isFirstRowColumn)
                    {
                        for (int i = firstRow.FirstCellNum; i < cellCount; ++i)
                        {
                            ICell cell = firstRow.GetCell(i);
                            if (cell != null)
                            {
                                string cellValue = cell.StringCellValue;
                                if (cellValue != null)
                                {
                                    DataColumn column = new DataColumn(cellValue);
                                    data.Columns.Add(column);
                                }
                            }
                        }
                        startRow = sheet.FirstRowNum + 1;
                    }
                    else
                    {
                        startRow = sheet.FirstRowNum;
                    }
                    // 最后一列的标号
                    int rowCount = sheet.LastRowNum;
                    for (int i = startRow; i <= rowCount; ++i)
                    {
                        IRow row = sheet.GetRow(i);
                        // 没有数据的行默认是null　
                        if (row == null || row.Cells.Count == 0) continue;
                        DataRow dataRow = data.NewRow();
                        bool isEmptyRow = true;
                        for (int j = row.FirstCellNum; j < cellCount; ++j)
                        {
                            // 同理，没有数据的单元格都默认是null
                            if (row.GetCell(j) != null)
                            {
                                string cellString = row.GetCell(j).ToString().Trim();
                                dataRow[j] = cellString;
                                if (!string.IsNullOrWhiteSpace(cellString))
                                {
                                    isEmptyRow = false;
                                }
                            }
                        }
                        if (!isEmptyRow)
                        {
                            data.Rows.Add(dataRow);
                        }
                    }
                }

                return data;
            }
            catch (Exception ex)
            {
                Console.WriteLine("Exception: " + ex.Message);
                return null;
            }
        }



        #endregion

        #region 辅助函数
        /// <summary>
        /// 获取列名翻译
        /// </summary>
        /// <param name="rowNames"></param>
        /// <param name="ColumnName"></param>
        /// <returns></returns>
        private static string GetDescription(List<ExeclModel> rowNames, string ColumnName)
        {
            foreach (var item in rowNames)
            {
                if (item.Name == ColumnName)
                {
                    return item.Description;
                }
            }
            return ColumnName;

        }

        /// <summary>
        /// 列名模型
        /// </summary>
        public class ExeclModel
        {
            public string Name { get; set; }

            public string Description { get; set; }
        }

        /// <summary>
        /// 写入缓存
        /// </summary>
        /// <param name="ms"></param>
        /// <param name="workBook"></param>
        /// <param name="downLoadName"></param>
        private static FileStreamResult WorkBookWrite(MemoryStream ms, IWorkbook workBook, string downLoadName)
        {
            workBook.Write(ms, true);
            var result = new FileStreamResult(ms, "application/octet-stream") { FileDownloadName = downLoadName };
            ms.Flush();
            ms.Seek(0, SeekOrigin.Begin);
            return result;
        }

        /// <summary>
        /// 生成下拉列表
        /// </summary>
        /// <param name="sheet">工作簿</param>
        /// <param name="rowIndex">行号</param>
        /// <param name="cellIndex">单元格号</param>
        /// <param name="arrays">范围数组</param>
        private static void SetCellDropdownList(ISheet sheet, int rowIndex, int cellIndex, string[] arrays)
        {
            // 6400是指的作用到的最大格子数
            var list = new CellRangeAddressList(rowIndex, 64000, cellIndex, cellIndex);
            var dVConstraint = DVConstraint.CreateExplicitListConstraint(arrays);
            var hSSFDataValidation = new HSSFDataValidation(list, dVConstraint);
            hSSFDataValidation.CreateErrorBox("输入不合法", "请选择下拉框中的值");
            hSSFDataValidation.ShowPromptBox = true;
            sheet.AddValidationData(hSSFDataValidation);
        }

        /// <summary>
        /// 创建表头单元格样式
        /// </summary>
        /// <returns></returns>
        private static ICellStyle CrateTitleCell(this IWorkbook workBook)
        {
            var titleCell = workBook.CreateCellStyle();
            titleCell.Alignment = HorizontalAlignment.Center;
            // 单元格字体
            IFont titleFont = workBook.CreateFont();
            // 字体字号
            titleFont.FontHeightInPoints = 20;
            titleFont.FontName = "宋体";
            titleFont.Boldweight = (short)FontBoldWeight.Bold;
            titleCell.SetFont(titleFont);
            // 自动换行
            titleCell.WrapText = true;
            titleCell.BorderLeft = BorderStyle.Thin;
            titleCell.BorderRight = BorderStyle.Thin;
            titleCell.BorderTop = BorderStyle.Thin;
            titleCell.BorderBottom = BorderStyle.Thin;
            return titleCell;
        }

        /// <summary>
        /// 创建数据单元格样式
        /// </summary>
        /// <returns></returns>
        private static ICellStyle CrateDataCell(this IWorkbook workBook)
        {
            ICellStyle dataCell = workBook.CreateCellStyle();
            dataCell.Alignment = HorizontalAlignment.Center;
            IFont dataFont = workBook.CreateFont();
            dataFont.FontHeightInPoints = 10;
            dataFont.FontName = "宋体";
            dataCell.SetFont(dataFont);
            dataCell.WrapText = true;
            dataCell.BorderLeft = BorderStyle.Thin;
            dataCell.BorderRight = BorderStyle.Thin;
            dataCell.BorderTop = BorderStyle.Thin;
            dataCell.BorderBottom = BorderStyle.Thin;
            return dataCell;
        }
        #endregion
    }
}
