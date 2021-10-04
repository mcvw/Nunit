using System;
using System.Configuration;
using System.Data;
using NUnit.Framework;
using NUnit.Framework.Api;
using NUnit.Framework.Internal;

/*
 * WTWICT SDET Part2
 *
 * Description:
 *
 * Table A below represents an example of a pricing table containing a number of products and their varieties.
 * The prices of these products are in pounds sterling.
 * Table B represents the same products and varieties with their prices converted to Euros.
 * The value in each cell of Table B is calculated from the value in the corresponding cell in Table A.
 * In other words: Value (Product1, Variety1) in “Table B” = (Value (Product1, Variety1) in “Table A”) * conversion rate.
 * In the example below, the conversion rate is 1.5.
 *
 * The shaded cells should not contain values.
 *
 * The value in each cell in the bottom row is equal to the sum of the values in the corresponding column.

Table A - Prices in Sterling	Variety 1	Variety 2	Variety 3	Variety 4
Product 1	10	12	14	45
Product 2	20	15	24	?
Product 3	22	60	?	?
Product 4	28	?	?	?
Total	80	87	38	45

    Table B - Prices in Euro	Variety 1	Variety 2	Variety 3	Variety 4
Product 1	15	18	21	67.5
Product 2	30	22.5	36	?
Product 3	33	90	?	?
Product 4	42	?	?	?
Total	120	130.5	57	67.5

 * Exercise:
 * write a test to check the correctness of Table B in relation to Table A.
 * Consideration should be given to the variety of possible errors conditions within other real-world data,
 * and how these can be tested.
 *
 * We are not concerned solely with whether or not the test works, but also with how tidy, structured and robust it is.
 *
 */
namespace WTWICTSDETPART2
{
    [TestFixture]
    public class TestWTWICTSDETPart2
    {
        public static DataTable TableA;
        public static DataTable TableB;
        public static double ExchangeRate = 1.5;

        [Test]
        // Check tables are not null
        public void CheckTablesNotNull()
        {
            Assert.NotNull(TableA);
            System.Console.WriteLine("PASS: TableA is not null");

            Assert.NotNull(TableB);
            System.Console.WriteLine("PASS: TableB is not null");
        }

        [Test]
        // Check if tables are equal
        public void CheckTablesAreEqual()
        {
            Assert.AreNotEqual(TableA, TableB, "FAIL: Contents of TableA equals TableB");
            System.Console.WriteLine("PASS: TableA does not equal TableB");
        }

        [Test]
        // Check number of columns in tableA = number of columns in tableB
        public void CheckColumnsAreEqual()
        {
            Assert.AreEqual(TableA.Columns.Count, TableB.Columns.Count,
                "FAIL: Number of columns differs between TableA and TableB");
            System.Console.WriteLine("PASS: Number of columns in TableA matches number of columns in TableB");
        }

        [Test]
        // Check number of rows in tableA = number of rows in tableB
        public void CheckRowsAreEqual()
        {
            Assert.AreEqual(TableA.Rows.Count, TableB.Rows.Count,
                "FAIL: Number of rows differs between TableA and TableB");
            System.Console.WriteLine("PASS: Number of rows in TableA matches number of rows in TableB");
        }

        [Test]
        // Check tableA columnN total = tableB columnN total (multiplied by exchange rate)
        public void CheckColumnTotalsAreEqual()
        {
            var sumtblACol1 = ((double)TableA.Compute("SUM(Variety1)", string.Empty) / 2);
            var sumtblBCol1 = ((double)TableB.Compute("SUM(Variety1)", string.Empty) / 2);
            var convertedSumTblBCol1 = sumtblBCol1 / ExchangeRate;
            Assert.AreEqual(sumtblACol1, convertedSumTblBCol1,
                "FAIL: Converted Variety1 total differs between TableA and TableB");
            System.Console.WriteLine("PASS: Converted Variety1 total matches between TableA and TableB");

            // Check tableA column 2 total = tableB column 2 total (multiplied by exchange rate)
            var sumtblACol2 = ((double)TableA.Compute("SUM(Variety2)", string.Empty) / 2);
            var sumtblBCol2 = ((double)TableB.Compute("SUM(Variety2)", string.Empty) / 2);
            var convertedSumTblBCol2 = sumtblBCol2 / ExchangeRate;
            Assert.AreEqual(sumtblACol2, convertedSumTblBCol2,
                "FAIL: Converted Variety2 total differs between TableA and TableB");
            System.Console.WriteLine("PASS: Converted Variety2 total matches between TableA and TableB");

            // Check tableA column 3 total = tableB column 3 total (multiplied by exchange rate)
            var sumtblACol3 = ((double)TableA.Compute("SUM(Variety3)", string.Empty) / 2);
            var sumtblBCol3 = ((double)TableB.Compute("SUM(Variety3)", string.Empty) / 2);
            var convertedSumTblBCol3 = sumtblBCol3 / ExchangeRate;
            Assert.AreEqual(sumtblACol3, convertedSumTblBCol3,
                "FAIL: Converted Variety3 total differs between TableA and TableB");
            System.Console.WriteLine("PASS: Converted Variety3 total matches between TableA and TableB");

            // Check tableA column 4 total = tableB column 4 total (multiplied by exchange rate)
            var sumtblACol4 = ((double)TableA.Compute("SUM(Variety4)", string.Empty) / 2);
            var sumtblBCol4 = ((double)TableB.Compute("SUM(Variety4)", string.Empty) / 2);
            var convertedSumTblBCol4 = sumtblBCol4 / ExchangeRate;
            Assert.AreEqual(sumtblACol4, convertedSumTblBCol4,
                "FAIL: Converted Variety4 total differs between TableA and TableB");
            System.Console.WriteLine("PASS: Converted Variety4 total matches between TableA and TableB");
        }
        
        static void Main(string[] args)
        {
        }

        [SetUp]
        // Create and populate DataTables
        public void SetupDataTables()
        {
            string[] columnNames = {"Product", "Variety1", "Variety2", "Variety3", "Variety4"};
            string[] rowNames = {"Product 1", "Product 2", "Product 3", "Product 4", "Total    "};
            double[][] tableARows = new double[][]
            {
                new double[] {10, 12, 14, 45},
                new double[] {20, 15, 24, 0},
                new double[] {22, 60, 0, 0},
                new double[] {28, 0, 0, 0}
            };

            TableA = CreateDataTable(columnNames, rowNames, tableARows);
            System.Console.WriteLine("\nTable A: Prices in Sterling");
            System.Console.WriteLine(columnNames[0] + "\t\t" + columnNames[1] + "\t" + columnNames[2] + "\t" +
                                     columnNames[3] + "\t" + columnNames[4]);
            foreach (DataRow row in TableA.Rows)
            {
                System.Console.WriteLine(row.Field<string>(0) + "\t" + row.Field<double>(1)
                                         + "\t\t" + row.Field<double>(2) + "\t\t" + row.Field<double>(3)
                                         + "\t\t" + row.Field<double>(4));
            }

            double[][] tableBRows = new double[][]
            {
                new double[] {15, 18, 21, 67.5},
                new double[] {30, 22.5, 36, 0},
                new double[] {33, 90, 0, 0},
                new double[] {42, 0, 0, 0}
            };

            TableB = CreateDataTable(columnNames, rowNames, tableBRows);
            System.Console.WriteLine("\nTable B: Prices in Euro");
            System.Console.WriteLine(columnNames[0] + "\t\t" + columnNames[1] + "\t" + columnNames[2] + "\t" +
                                     columnNames[3] + "\t" + columnNames[4]);
            foreach (DataRow row in TableB.Rows)
            {
                System.Console.WriteLine(row.Field<string>(0) + "\t" + row.Field<double>(1)
                                         + "\t\t" + row.Field<double>(2) + "\t\t" + row.Field<double>(3)
                                         + "\t\t" + row.Field<double>(4));
            }
        }

        // Create DataTable
        static DataTable CreateDataTable(string[] columnNames, string[] rowNames, double[][] tableRows)
        {
            DataTable table = new DataTable();

            try
            {
                for (int i = 0; i < columnNames.Length; i++)
                {
                    if (i == 0)
                        table.Columns.Add(columnNames[i], typeof(string));
                    else
                        table.Columns.Add(columnNames[i], typeof(double));
                }

                for (int i = 0; i < tableRows.Length; i++)
                {
                    double[] rowValues = tableRows[i];
                    table.Rows.Add(rowNames[i], rowValues[0], rowValues[1], rowValues[2], rowValues[3]);
                }

                // Total values for all 'Variety' columns
                var totalTblCol1 = (double) table.Compute("SUM(Variety1)", string.Empty);
                var totalTblCol2 = (double) table.Compute("SUM(Variety2)", string.Empty);
                var totalTblCol3 = (double) table.Compute("SUM(Variety3)", string.Empty);
                var totalTblCol4 = (double) table.Compute("SUM(Variety4)", string.Empty);

                // Add 'totals' row
                table.Rows.Add(rowNames[4], totalTblCol1, totalTblCol2, totalTblCol3, totalTblCol4);
            }
            catch (Exception e)
            {
                Console.WriteLine("Exception caught within CreateDataTable: " + e);
                throw;
            }

            return table;
        }
    }
}
