using System.Globalization;
using CsvHelper;
using System.IO;
using WebBanSach.Models;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System.Collections.Generic;
using System.Linq;

namespace WebBanSach.Services
{
    public class CsvUserSeedService
    {
        private readonly ApplicationDbContext _db;
        private readonly UserManager<ApplicationUser> _userManager;

        public CsvUserSeedService()
        {
            _db = new ApplicationDbContext();
            _userManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(_db));
        }

        public void SeedUsersFromCsv(string csvPath)
        {
            var existingMappings = _db.UserMaps.Select(u => u.CSVUserId).ToHashSet();

            using (var reader = new StreamReader(csvPath))
            using (var csv = new CsvReader(reader, CultureInfo.InvariantCulture))
            {
                var records = csv.GetRecords<RatingRow>();
                var userIds = new HashSet<int>();

                foreach (var record in records)
                {
                    int csvUserId = record.UserID;
                    if (existingMappings.Contains(csvUserId) || userIds.Contains(csvUserId))
                        continue;

                    userIds.Add(csvUserId);

                    var username = $"csv_user_{csvUserId}";
                    var email = $"csv_user_{csvUserId}@example.com";
                    var password = "Default123!";

                    var user = new ApplicationUser
                    {
                        FullName = username,
                        UserName = username,
                        Email = email,
                        Role = "User"
                    };

                    var result = _userManager.Create(user, password);
                    if (result.Succeeded)
                    {
                        _db.UserMaps.Add(new UserMap
                        {
                            AppUserId = user.Id,
                            CSVUserId = csvUserId
                        });
                    }
                }

                _db.SaveChanges();
            }
        }
    }
}
