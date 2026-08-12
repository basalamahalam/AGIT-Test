using System;
using System.Collections.Generic;
using System.Linq;

namespace AgitAssessment
{
    class Program
    {
        static void Main(string[] args)
        {
            // maaf saya buatkan pengujian secara manual saja
            Console.WriteLine("PENGUJIAN MANUAL CASE 1");

            RunTest("Test 1: Sample Case", 
                new List<int> { 4, 5, 1, 7, 6, 4, 0 }, 
                new List<int> { 4, 5, 4, 5, 5, 4, 0 });

            RunTest("Test 2: Total yang Habis dibagi", 
                new List<int> { 2, 4, 0, 1, 5 }, 
                new List<int> { 3, 3, 0, 3, 3 });

            RunTest("Test 3: Total Bersisa (Prioritas nilai awal)", 
                new List<int> { 2, 0, 6, 2 }, 
                new List<int> { 3, 0, 4, 3 });

            RunTest("Test 4: Semua Nol (Libur Semua)", 
                new List<int> { 0, 0, 0, 0 }, 
                new List<int> { 0, 0, 0, 0 });

            RunTest("Test 5: Edge Case - Nilai Sama Persis (Prioritas index depan)", 
                new List<int> { 4, 3, 3 }, 
                new List<int> { 4, 3, 3 });

            Console.WriteLine("\nPengujian selesai.");
        }

        static void RunTest(string namaTest, List<int> input, List<int> expected)
        {
            Console.Write($"{namaTest} ... ");
            try
            {
                List<int> inputAsli = new List<int>(input);
                List<int> result = ProdScheduler.Balance(input);

                bool isPassed = true;
                if (result.Count != expected.Count) isPassed = false;
                
                for (int i = 0; i < expected.Count; i++)
                {
                    if (result[i] != expected[i])
                    {
                        isPassed = false;
                        break;
                    }
                }

                if (isPassed)
                {
                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.WriteLine("PASSED");
                    Console.ResetColor();
                }
                else
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("FAILED");
                    Console.ResetColor();
                    Console.WriteLine($"   Input   : [{string.Join(", ", inputAsli)}]");
                    Console.WriteLine($"   Expected: [{string.Join(", ", expected)}]");
                    Console.WriteLine($"   Result  : [{string.Join(", ", result)}]");
                }
            }
            catch (Exception ex)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine($"FAILED (Error: {ex.Message})");
                Console.ResetColor();
            }
        }
    }

    public class ProdScheduler
    {
        public static List<int> Balance(List<int> rencana_awal)
        {
            if (rencana_awal.Any(x => x < 0))
            {
                throw new ArgumentException("Input tidak boleh mengandung angka negatif ya");
            }

            int total_prod = rencana_awal.Sum();
            
            var idx_aktif = rencana_awal
                .Select((value, index) => new { value, index })
                .Where(x => x.value > 0)
                .Select(x => x.index)
                .ToList();

            if (!idx_aktif.Any())
            {
                return new List<int>(new int[rencana_awal.Count]);
            }

            int avg_prod = total_prod / idx_aktif.Count;
            int sisa = total_prod % idx_aktif.Count;

            var priority = idx_aktif
                .OrderByDescending(i => rencana_awal[i]) 
                .ThenBy(i => i)                        
                .ToList();

            List<int> hasil = new List<int>(new int[rencana_awal.Count]);

            foreach (var idx in idx_aktif)
            {
                hasil[idx] = avg_prod;
            }

            for (int i = 0; i < sisa; i++)
            {
                int temp_idx = priority[i];
                hasil[temp_idx] += 1;
            }

            return hasil;
        }
    }
}