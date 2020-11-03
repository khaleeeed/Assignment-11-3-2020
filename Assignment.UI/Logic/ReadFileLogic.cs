using Assignment.Domain.Entity;
using Assignment.Domain.Models;
using Assignment.Infrastructure.DbContext;
using ExcelDataReader;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.OleDb;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace Assignment.UI.Logic
{
    public class ReadFileLogic : IReadFileLogic
    {
        private readonly IDataBaseAssignmentContext _DBContext;

        public ReadFileLogic(IDataBaseAssignmentContext dBContext)
        {
            _DBContext = dBContext;
        }

        public Result ImportDataFromFile(Stream openReadStream, string fileExtension)
        {
            if (fileExtension.ToLower() == "csv")
            {
                var message = ReadCSV(openReadStream);
                return new Result() { Message = message };
            }
            if (fileExtension.ToLower() == "xlsx")
            {
                var message = ReadXLSX(openReadStream);
                return new Result() { Message = message };
            }


            return new Result { Message = "file not supported" };
        }
        private string ReadXLSX(Stream stream)
        {
            string lineNotInserted = string.Empty;
            int count = 0;

            using (stream)
            {
                IExcelDataReader reader = null;
                System.Text.Encoding.RegisterProvider(System.Text.CodePagesEncodingProvider.Instance);

                reader = ExcelReaderFactory.CreateOpenXmlReader(stream);

                if (reader != null)
                {
                    while (reader.Read())
                    {
                        if (count == 0)
                        {
                            count++;
                            continue;
                        }
                        string key = reader[0].ToString();
                        string itemCode = reader[1].ToString();
                        string colorCode = reader[2].ToString();
                        string description = reader[3].ToString();
                        string Price = reader[4].ToString();
                        double.TryParse(reader[5].ToString(), out double DiscountPrice);
                        string DeliveredIn = reader[6].ToString();
                        string Q1 = reader[7].ToString();
                        double.TryParse(reader[8].ToString(), out double Size);
                        string Color = reader[9].ToString();

                        var status = Add(new AssignmentTable
                        {
                            Key = key,
                            Color = Color,
                            ColorCode = colorCode,
                            DeliveredIn = DeliveredIn,
                            Description = description,
                            DiscountPrice = DiscountPrice,
                            ItemCode = itemCode,
                            Q1 = Q1,
                            Size = Size
                        });

                        count++;
                        if (!status)
                            lineNotInserted += $"-{count}";
                    }
                }
            }
            return "";
        }
        private string ReadCSV(Stream stream)
        {
            string lineNotInserted = string.Empty;
            
            using (stream)
            {
                using var sr = new StreamReader(stream);
                string line;
                long count = 0;

                while ((line = sr.ReadLine()) != null)
                {
                    if (count == 0)
                    {
                        count++;
                        continue;
                    }
                    var reader = line.Split(',');

                    string key = reader[0];
                    string itemCode = reader[1];
                    string colorCode = reader[2];
                    string description = reader[3];
                    string Price = reader[4];
                    double.TryParse(reader[5], out double DiscountPrice);
                    string DeliveredIn = reader[6];
                    string Q1 = reader[7];
                    double.TryParse(reader[8], out double Size);
                    string Color = reader[9];

                    var status = Add(new AssignmentTable
                    {
                        Key = key,
                        Color = Color,
                        ColorCode = colorCode,
                        DeliveredIn = DeliveredIn,
                        Description = description,
                        DiscountPrice = DiscountPrice,
                        ItemCode = itemCode,
                        Q1 = Q1,
                        Size = Size
                    });

                    count++;
                    if (!status)
                        lineNotInserted += $"-{count}";
                }
            }
            return lineNotInserted;
        }

        private string ReadXLSX(string path)
        {
            string lineNotInserted = string.Empty;
            string conString = @"Provider=Microsoft.ACE.OLEDB.12.0;" + "Data Source=" + path + ";" + "Extended Properties=" + "\"" + "Excel 12.0;HDR=YES;" + "\"";
            string textCommand = "SELECT count(1) FROM [iru-assignment-2018$]";
            using var con = new OleDbConnection(conString);
            con.Open();
            using var cmd = new OleDbCommand(textCommand, con);
            var totalRowCount = Convert.ToInt32(cmd.ExecuteScalar());
            var count = 0;
            textCommand = "select * from [iru-assignment-2018$A{0}:j{1}]";

            for (; count < totalRowCount;)
            {
                cmd.CommandText = string.Format(textCommand, count, count + 200);
                using var reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    string key = reader[0].ToString();
                    string itemCode = reader[1].ToString();
                    string colorCode = reader[2].ToString();
                    string description = reader[3].ToString();
                    string Price = reader[4].ToString();
                    double.TryParse(reader[5].ToString(), out double DiscountPrice);
                    string DeliveredIn = reader[6].ToString();
                    string Q1 = reader[7].ToString();
                    double.TryParse(reader[8].ToString(), out double Size);
                    string Color = reader[9].ToString();

                    var status = Add(new AssignmentTable
                    {
                        Key = key,
                        Color = Color,
                        ColorCode = colorCode,
                        DeliveredIn = DeliveredIn,
                        Description = description,
                        DiscountPrice = DiscountPrice,
                        ItemCode = itemCode,
                        Q1 = Q1,
                        Size = Size
                    });

                    count++;
                    if (!status)
                        lineNotInserted += $"-{count}";
                }
            }

            return lineNotInserted;

        }

        private  bool Add(AssignmentTable entity)
        {
            _DBContext.AssignmentTable.Add(entity);
            var rowEffected =  _DBContext.SaveChangesAsync(System.Threading.CancellationToken.None).Result;
            if (rowEffected > 0)
                return true;
            else
                return false;
        }

      
    }
}
