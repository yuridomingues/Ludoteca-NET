using System;
using System.Collections.Generic;
using System.IO;
using System.Text.Json;

namespace Ludo.Models
{
    public class MemberModel
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public DateTime BirthDate { get; set; }
        public string Phone { get; set; } = string.Empty;
        public decimal Fine { get; set; } = 0;
        public bool Availability { get; set; } = true;

        public static List<MemberModel> Members = new List<MemberModel>();
        private static string FilePath = "data/members.json";

        public static void RegisterMember(MemberModel member)
        {
            member.Id = Members.Count + 1;
            Members.Add(member);
            SaveMembers();
        }

        public static void ListMembers()
        {
            Console.WriteLine("=== MEMBERS LIST ===");

            if (Members.Count == 0)
            {
                Console.WriteLine("No registered members.");
            }
            else
            {
                foreach (var member in Members)
                {
                    Console.WriteLine($"Id: {member.Id}, Name: {member.Name}, Email: {member.Email}, Phone: {member.Phone}, Fine: ${member.Fine}, Available: {member.Availability}");
                }
            }

            Console.WriteLine("Press ENTER to continue.");
            Console.ReadLine();
        }

        public static void SaveMembers()
        {
            Directory.CreateDirectory("data");
            var json = JsonSerializer.Serialize(Members, new JsonSerializerOptions { WriteIndented = true });
            File.WriteAllText(FilePath, json);
        }

        public static void LoadMembers()
        {
            if (File.Exists(FilePath))
            {
                var json = File.ReadAllText(FilePath);
                var loadedMembers = JsonSerializer.Deserialize<List<MemberModel>>(json);
                Members = loadedMembers ?? new List<MemberModel>();
            }
        }
         public static MemberModel? GetMemberById(int id)
        {
            return Members.FirstOrDefault(m => m.Id == id);
        }
    }
}
