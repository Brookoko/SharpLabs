namespace AppSetup
{
    using System;
    using System.Collections.Generic;
    using System.Data;
    using System.Linq;
    using ConsoleApp;
    using Data;
    using DependencyInjection;

    public class StartOptions : Options
    {
        [Inject]
        public Queries Queries { get; set; }
        
        public override string Id => "Start";
        
        public StartOptions()
        {
            AddOption("--all", _ => PrintResult(Queries.AllProjects()));
            AddOption("--completed", _ => PrintResult(Queries.CompletedProjects()));
            AddOption("--order", _ => PrintResult(Queries.ProjectsOrderByStart()));
            AddOption("--inDepartment $name", p => PrintResult(Queries.GetAllWorkersInDepartment(p.String)));
            AddOption("--most", _ => PrintResult(Queries.WorkOnMostProjects()));
            AddOption("--workingOn #id", p =>  PrintResult(Queries.WorkingOn(p.Int)));
            AddOption("--incomingFrom #id", p =>  PrintResult(Queries.IncomingFrom(p.Int)));
            AddOption("--legal", _ =>  PrintResult(Queries.Legal()));
            AddOption("--productive", _ =>  PrintResult(Queries.MostProductive()));
            AddOption("--cost", _ => PrintResult(Queries.CostOfProjects()));
            
            AddOption("--create $name $type", p => PrintResult(Create(p.Strings)));
            AddOption("--delete #id", p => PrintResult(Queries.Delete(p.Int)));
        }
        
        private DataSet Create(List<string> strings)
        {
            var name = strings[0];
            var type = strings[1];
            if (string.IsNullOrEmpty(name)) throw new ArgumentException("Invalid name");
            if (type != "l" && type != "n") throw new ArgumentException("Invalid type");
            return Queries.Create(name, type[0]);
        }

        private void PrintResult(DataSet set)
        {
            var matrix = ToMatrix(set.Tables[0]);
            var widths = ColumnsWidth(matrix);
            for (var i = 0; i < matrix.GetLength(0); i++)
            {
                for (var j = 0; j < matrix.GetLength(1); j++)
                {
                    var el = matrix[i, j].ToString();
                    Console.Write(AlignLeft(el, widths[j]));
                }
                Console.WriteLine();
            }
        }

        private object[,] ToMatrix(DataTable table)
        {
            var cols = new DataColumn[table.Columns.Count];
            table.Columns.CopyTo(cols, 0);
            var header = cols.Select(c => c.ColumnName).ToArray();
            var rows = new DataRow[table.Rows.Count];
            table.Rows.CopyTo(rows, 0);
            var matrix = new object[rows.Length + 1, cols.Length];
            for (var i = 0; i < header.Length; i++)
            {
                matrix[0, i] = header[i];
            }
            for (var i = 0; i < rows.Length; i++)
            {
                var items = rows[i].ItemArray;
                for (var j = 0; j < items.Length; j++)
                {
                    matrix[i + 1, j] = items[j];
                }
            }
            return matrix;
        }

        private int[] ColumnsWidth(object[,] matrix)
        {
            return Enumerable.Range(0, matrix.GetLength(1))
                .Select(i => GetColumn(matrix, i))
                .Select(col => col
                    .OrderByDescending(el => el.ToString().Length)
                    .First().ToString().Length + 4)
                .ToArray();
        }
        
        private T[] GetColumn<T>(T[,] matrix, int columnNumber)
        {
            return Enumerable.Range(0, matrix.GetLength(0))
                .Select(x => matrix[x, columnNumber])
                .ToArray();
        }
        
        private string AlignLeft(string s, int target)
        {
            return s.PadRight(target, ' ');
        }
    }
}