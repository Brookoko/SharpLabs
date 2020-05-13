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
            AddOption("--before #y #m #d", p => PrintResult(Queries.StartBefore(ToDate(p.Ints.ToArray()))));
            AddOption("--after #y #m #d", p => PrintResult(Queries.StartAfter(ToDate(p.Ints.ToArray()))));
            AddOption("--range #y1 #m1 #d1 #y2 #m2 #d2", p => PrintResult(Range(p.Ints)));
            AddOption("--first", p => PrintResult(Queries.FirstProject()));
            AddOption("--cost", p => PrintResult(Queries.CostOfProjects()));
            AddOption("--lastOf #id", p =>  PrintResult(Queries.LastProjectOf(GetWorker(p.Int))));
            AddOption("--workingOn #id", p =>  PrintResult(Queries.CurrentlyWorkingOn(GetWorker(p.Int))));
            AddOption("--withName $name", p => PrintResult(Queries.WorkersWithName(p.String)));
            AddOption("--common", _ => PrintResult(Queries.CommonName()));
            AddOption("--most", _ => PrintResult(Queries.WorkOnMostProjects()));
            AddOption("--count #id", p =>  PrintResult(Queries.ProjectCount(GetWorker(p.Int))));
            AddOption("--allWorkers", _ => PrintResult(Queries.AllWorkers()));
        }
        
        private Worker GetWorker(int id)
        {
            return Queries.AllWorkers().First(w => w.Id == id);
        }
        
        private IEnumerable<Project> Range(List<int> ints)
        {
            var start = ToDate(ints.Take(3).ToArray());
            var end = ToDate(ints.Skip(3).ToArray());
            return Queries.InRange(start, end);
        }
        
        private DateTime ToDate(int[] ints)
        {
            return new DateTime(ints[0], ints[1], ints[2]);
        }
        
        private void PrintResult<T>(IEnumerable<T> result)
        {
            foreach (var res in result)
            {
                Console.WriteLine(res);
            }
        }

        private void PrintResult<T>(T res)
        {
            Console.WriteLine(res);
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