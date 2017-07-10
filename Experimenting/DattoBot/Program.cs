using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace DattoBot
{
    class Program
    {
        static void Main(string[] args)
        {
        }
        static int[] incrementalBackups(int lastBackupTime, int[][] changes)
        {
            return changes.Where(x => x[0] > lastBackupTime).Select(x => x[1]).Distinct().OrderBy(x => x).ToArray();
            //return changes.GroupBy(p => p[1]).Select(g => new int[] { g.Max(h => h[0]), g.Key }).Where(c => c[0] > lastBackupTime).Select(b => b[1]).OrderBy(j => j).ToArray();
            //return Enumerable.Where(changes, (c) => c[0] > lastBackupTime).OrderBy((c) => c[1]).Select((c) => c[1]).Distinct().ToArray();
        }
        static int[] troubleFiles(int[][] files, int[] backups)
        {
            var troubles = new int[backups.Length];
            //for (int i = 0; i < backups.Length; i++)
            //{
            //    if (files.Select(x => x[0]).Count(x => backups[i] > x && x > 0) > 0)
            //        troubles[i] += files.Select(x => x[0]).Count(x => backups[i] > x && x > 0);

            //}
            for (int i = 0; i < files.Length; i++)
            {
                if(files[i][0]>0 && files[i][0]+files[1])
            }
        }
    }
}
