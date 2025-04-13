using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Ots.Api.Domain;
using Ots.Schema;
using System.Diagnostics;
using System.Text;

namespace Ots.Api.Impl.PerformanceTest
{
    public class InsertPerformanceTester
    {
        private readonly OtsDbContext dbContext;
        private readonly IMapper mapper;

        public InsertPerformanceTester(OtsDbContext dbContext, IMapper mapper)
        {
            this.dbContext = dbContext;
            this.mapper = mapper;
        }

        public async Task RunInsertTestsAsync()
        {
            var logBuilder = new StringBuilder();
            var customers1 = GenerateFakeCustomers(10000);
            var stopwatch1 = Stopwatch.StartNew();

            foreach (var customer in customers1)
            {
                await dbContext.Customers.AddAsync(customer);
            }
            try
            {
                await dbContext.SaveChangesAsync(); 
            }
            catch (Exception ex)
            {
                LogError(ex, "SaveChanges foreach ile tek tek", logBuilder);
            }
            stopwatch1.Stop();
            logBuilder.AppendLine($"Foreach ile tek tek ekleme süresi: {stopwatch1.Elapsed.TotalSeconds} saniye");

            var customers2 = GenerateFakeCustomers(10000);
            var stopwatch2 = Stopwatch.StartNew();

            await dbContext.Customers.AddRangeAsync(customers2); 
            try
            {
                await dbContext.SaveChangesAsync();  
            }
            catch (Exception ex)
            {
                LogError(ex, "SaveChanges AddRange ile toplu ekleme", logBuilder);
            }
            stopwatch2.Stop();
            logBuilder.AppendLine($"AddRange ile toplu ekleme süresi: {stopwatch2.Elapsed.TotalSeconds} saniye");

         
            var customers3 = GenerateFakeCustomers(10000);
            var stopwatch3 = Stopwatch.StartNew();
            await BulkInsertAsync(customers3);  
            stopwatch3.Stop();
            logBuilder.AppendLine($"Bulk Insert ile toplu ekleme süresi: {stopwatch3.Elapsed.TotalSeconds} saniye");

            var logFilePath = Path.Combine(Directory.GetCurrentDirectory(), "Logs", "insert_log.txt");
            await File.WriteAllTextAsync(logFilePath, logBuilder.ToString());

            Console.WriteLine("Performans testleri tamamlandı. Log dosyasına yazıldı.");
        }

        private async Task BulkInsertAsync(List<Customer> customers)
        {
            // BulkInsert için örnek kod, örneğin EntityFrameworkCore.BulkExtensions kullanılabilir
            // var result = await dbContext.BulkInsertAsync(customers);
          
            await dbContext.Customers.AddRangeAsync(customers);
            await dbContext.SaveChangesAsync();
        }

        private void LogError(Exception ex, string methodName, StringBuilder logBuilder)
        {
            string errorMessage = $"{methodName} - Hata: {ex.Message}\nStackTrace: {ex.StackTrace}";
            Console.WriteLine(errorMessage);
            
            string logDirectory = Path.Combine(Directory.GetCurrentDirectory(), "Logs", "NewPerformance-log");
            Directory.CreateDirectory(logDirectory);
            File.AppendAllText(Path.Combine(logDirectory, "error_log.txt"), errorMessage);
            logBuilder.AppendLine(errorMessage);
        }

        private List<Customer> GenerateFakeCustomers(int count)
        {
            var customers = new List<Customer>();
            var random = new Random();
            var customerNumbers = new HashSet<int>();

            for (int i = 0; i < count; i++)
            {
                int customerNumber;

                do
                {
                    customerNumber = random.Next(1000000, 999999999);
                }
                while (customerNumbers.Contains(customerNumber));

                customerNumbers.Add(customerNumber);

                customers.Add(new Customer
                {
                    FirstName = $"Name{i}",
                    MiddleName = $"Middle{i}",
                    LastName = $"Surname{i}",
                    Email = $"name{i}@example.com",
                    CustomerNumber = customerNumber,
                    IdentityNumber = random.Next(100000000, 999999999).ToString(),
                    OpenDate = DateTime.Now,
                    InsertedDate = DateTime.Now,
                    InsertedUser = "performance-test",
                    IsActive = true
                });
            }

            return customers;
        }

    }
}
