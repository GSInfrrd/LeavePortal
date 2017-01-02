﻿using System;
using System.Web;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Net.Mail;
using System.Security.Cryptography;
using System.Text;
using NPOI.HSSF.UserModel;
using NPOI.SS.UserModel;
using System.Reflection;
using NPOI.SS.Util;
using NPOI.HSSF.Util;
using System.Globalization;
using System.ComponentModel.DataAnnotations;

namespace LMS_WebAPI_Utils
{
    public static class CommonMethods
    {

        public static string Description(this Enum enumValue)
        {
            var enumType = enumValue.GetType();
            var field = enumType.GetField(enumValue.ToString());
            var attributes = field.GetCustomAttributes(typeof(DescriptionAttribute), false);
            return attributes.Length == 0
                ? enumValue.ToString()
                : ((DescriptionAttribute)attributes[0]).Description;
        }

        public static MemoryStream CreateDownloadExcel<T>(List<T> list, string include = "", string exclude = "", string sheetName = "Sheet1", string excelHeading = "Report", List<ExcelDownloadFilterList> filtersList = null)
        {
            var stream = new MemoryStream();
            try
            {
                string sourceFilePath =HttpContext.Current.Server.MapPath("~\\Documents\\SampleFiles\\ExcelReportDownload.xls");
                // check begin
                string fileTemplate = sourceFilePath;
                using (FileStream file = new FileStream(fileTemplate, FileMode.Open, FileAccess.Read))
                {
                    HSSFWorkbook hssfWorkBook = new HSSFWorkbook(file);
                    ISheet mySheet = hssfWorkBook.GetSheet("Sheet1");
                    hssfWorkBook.SetSheetName(hssfWorkBook.GetSheetIndex("Sheet1"), sheetName);
                    int count = typeof(T).GetProperties().Count();
                    PropertyInfo[] props = typeof(T).GetProperties();
                    List<PropertyInfo> propList = GetSelectedProperties(props, include, exclude);
                    int k = 1, j = 1;

                    mySheet.AddMergedRegion(new CellRangeAddress(k, k + 2, j, propList.Count));
                    IRow reportHeading = mySheet.CreateRow(mySheet.LastRowNum + 1);
                    ICell headingCell = reportHeading.CreateCell(j);
                    headingCell.SetCellValue(excelHeading);
                    ICellStyle cellStyleHeading = mySheet.Workbook.CreateCellStyle();
                    IFont headingFont = mySheet.Workbook.CreateFont();
                    headingFont.Boldweight = (short)FontBoldWeight.Bold;
                    headingFont.FontHeightInPoints = 16;
                    cellStyleHeading.SetFont(headingFont);
                    cellStyleHeading.VerticalAlignment = VerticalAlignment.Center;
                    headingCell.CellStyle = cellStyleHeading;

                    k = k + 3;
                    mySheet.AddMergedRegion(new CellRangeAddress(k, k, j, propList.Count));
                    IRow summary = mySheet.CreateRow(k);
                    ICell summaryCell = summary.CreateCell(j);
                    ICellStyle cellStyleSummary = mySheet.Workbook.CreateCellStyle();
                    cellStyleSummary.FillForegroundColor = HSSFColor.Grey50Percent.Index;
                    cellStyleSummary.FillPattern = FillPattern.SolidForeground;
                    IFont summaryFont = mySheet.Workbook.CreateFont();
                    summaryFont.Color = HSSFColor.White.Index;
                    summaryFont.Boldweight = (short)FontBoldWeight.Bold;
                    cellStyleSummary.SetFont(summaryFont);
                    summaryCell.CellStyle = cellStyleSummary;
                    if (filtersList != null && filtersList.Count > 0)
                    {
                        summaryCell.SetCellValue("Filter Summary");
                        int summaryCount = mySheet.LastRowNum + 1;
                        foreach (var filter in filtersList)
                        {
                            IRow summaryRow = mySheet.CreateRow(summaryCount);
                            ICell summaryTypeCell = summaryRow.CreateCell(j);
                            summaryTypeCell.SetCellValue(filter.FilterType);
                            if ((j + 1) <= propList.Count)
                            {
                                mySheet.AddMergedRegion(new CellRangeAddress(summaryCount, summaryCount, j + 1, propList.Count));
                            }
                            else
                            {
                                mySheet.AddMergedRegion(new CellRangeAddress(summaryCount, summaryCount, j + 1, j + 2));
                            }
                            ICell summaryNameCell = summaryRow.CreateCell(j + 1);
                            summaryNameCell.SetCellValue(filter.FilterValue);
                            summaryNameCell.CellStyle.WrapText = true;
                            summaryCount++;
                        }
                    }
                    else
                    {
                        summaryCell.SetCellValue("No filters applied");
                    }

                    k = mySheet.LastRowNum + 2;
                    IRow runningRow1 = mySheet.CreateRow(k);
                    ICell runningCell1 = null;
                    var userCulture = CultureInfo.CurrentCulture;
                    foreach (var prop in propList)
                    {
                        IEnumerable<DisplayAttribute> displayAttributes = prop.GetCustomAttributes(typeof(DisplayAttribute), false).Cast<DisplayAttribute>();
                        var disAtt = "";
                        foreach (DisplayAttribute displayAttribute in displayAttributes)
                        {
                            disAtt = displayAttribute.Name;
                        }
                        ICellStyle cellStyle = mySheet.Workbook.CreateCellStyle();
                        cellStyle.FillForegroundColor = HSSFColor.Grey50Percent.Index;
                        cellStyle.FillPattern = FillPattern.SolidForeground;
                        IFont font = mySheet.Workbook.CreateFont();
                        font.Color = HSSFColor.White.Index;
                        font.Boldweight = (short)FontBoldWeight.Bold;
                        cellStyle.SetFont(font);
                        runningCell1 = runningRow1.CreateCell(j++);
                        runningCell1.SetCellValue(disAtt.ToString());
                        runningCell1.CellStyle = cellStyle;
                    }
                    //storing values of the list into the excel
                    IDataFormat dataFormatCustom = hssfWorkBook.CreateDataFormat();
                    ICellStyle cellDateStyle = mySheet.Workbook.CreateCellStyle();
                    cellDateStyle.DataFormat = dataFormatCustom.GetFormat("dd-MM-yyyy");
                    IDataFormat doubleFormatCustom = hssfWorkBook.CreateDataFormat();
                    ICellStyle cellDoubleStyle = mySheet.Workbook.CreateCellStyle();
                    cellDoubleStyle.DataFormat = doubleFormatCustom.GetFormat("0.00");
                    foreach (var item in list)
                    {
                        IRow runningRow = mySheet.CreateRow(++k);
                        ICell runningCell = null;
                        //Iterate through property collection for columns
                        j = 1;
                        foreach (var prop in propList)
                        {
                            var propVal = item.GetType().GetProperty(prop.Name).GetValue(item);
                            // create cell content
                            var dataType = item.GetType().GetProperty(prop.Name).PropertyType.GetTypeInfo();
                            runningCell = runningRow.CreateCell(j++);
                            if (propVal != null)
                            {
                                if (dataType == typeof(string))
                                {
                                    string cellVal = Convert.ToString(Convert.ChangeType(propVal, dataType));
                                    runningCell.SetCellValue(cellVal);
                                }
                                else if (dataType == typeof(Int64))
                                {
                                    long cellVal = Convert.ToInt64(Convert.ChangeType(propVal, dataType));
                                    runningCell.SetCellValue(cellVal);
                                }
                                else if (dataType == typeof(Int16))
                                {
                                    short cellVal = Convert.ToInt16(Convert.ChangeType(propVal, dataType));
                                    runningCell.SetCellValue(cellVal);
                                }
                                else if (dataType == typeof(Int32))
                                {
                                    int cellVal = Convert.ToInt32(Convert.ChangeType(propVal, dataType));
                                    runningCell.SetCellValue(cellVal);
                                }
                                else if (dataType == typeof(bool))
                                {
                                    bool cellVal = Convert.ToBoolean(Convert.ChangeType(propVal, dataType));
                                    runningCell.SetCellValue(cellVal);
                                }
                                else if (dataType == typeof(DateTime) || dataType == typeof(Nullable<DateTime>))
                                {
                                    Nullable<DateTime> cellVal = (Nullable<DateTime>)(propVal);
                                    runningCell.SetCellValue(cellVal.GetValueOrDefault());
                                    runningCell.CellStyle = cellDateStyle;
                                }
                                else if (dataType == typeof(double) || dataType == typeof(float) || dataType == typeof(decimal))
                                {
                                    double cellVal = Convert.ToDouble(Convert.ChangeType(propVal, dataType));
                                    runningCell.SetCellValue(cellVal);
                                    runningCell.CellStyle = cellDoubleStyle;
                                }
                                else
                                {
                                    runningCell.SetCellValue(propVal.ToString());
                                }
                                runningCell.CellStyle.WrapText = true;
                            }
                            else
                            {
                                runningCell.SetCellValue("");
                            }
                            //runningCell.CellStyle.WrapText = true;
                        }
                    }
                    for (int col = 1; col <= propList.Count; col++)
                    {
                        mySheet.AutoSizeColumn(col, true);
                        var width = mySheet.GetColumnWidth(col);
                        if (width <= 25600)
                        {
                            mySheet.SetColumnWidth(col, width + 256);
                        }
                        else
                        {
                            mySheet.SetColumnWidth(col, 25600);
                        }
                    }
                    hssfWorkBook.Write(stream);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return stream;
        }

        private static List<PropertyInfo> GetSelectedProperties(PropertyInfo[] props, string include, string exclude)
        {
            List<PropertyInfo> propList = new List<PropertyInfo>();
            if (include != "") //Do include first
            {
                var includeProps = include.ToLower().Split(',').ToList();
                foreach (var item in props)
                {
                    var propName = includeProps.Where(a => a.Trim() == item.Name.ToLower()).FirstOrDefault();
                    if (!string.IsNullOrEmpty(propName))
                        propList.Add(item);
                }
            }
            else if (exclude != "") //Then do exclude
            {
                var excludeProps = exclude.ToLower().Split(',');
                foreach (var item in props)
                {
                    var propName = excludeProps.Where(a => a.Trim() == item.Name.ToLower()).FirstOrDefault();
                    if (string.IsNullOrEmpty(propName))
                        propList.Add(item);
                }
            }
            else //Default
            {
                propList.AddRange(props.ToList());
            }
            return propList;
        }

    }
}
