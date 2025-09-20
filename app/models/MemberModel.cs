using System;
using System.Collections.Generic;
using System.IO;
using System.Text.Json;

namespace Ludo.Models
{
    public class Member
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public DateTime BirthDate { get; set; }
        public string Phone { get; set; } = string.Empty;
        public decimal Fine { get; set; } = 0;
        public bool Availability { get; set; } = true;

        public static List<Member> Members = new List<Member>();
        private static string FilePath = "data/members.json";

        public static void RegisterMember(Member member)
        {
            member.Id = Members.Count + 1;
            Members.Add(member);
            SaveMembers();
        }

        public static void ListMembers()
        {
            if (Members.Count == 0)
            {
                Console.WriteLine("Nenhum membro cadastrado.");
            }
            else
            {
                foreach (var member in Members)
                {
                    Console.WriteLine($"=== MEMBRO ID: {member.Id} ===");
                    Console.WriteLine($"Nome: {member.Name} \nEmail: {member.Email} \nTelefone: {member.Phone} \nMulta: ${member.Fine} \nDisponível: {member.Availability}\n");
                }
            }

            Console.WriteLine("ENTER para continuar.");
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
                var loadedMembers = JsonSerializer.Deserialize<List<Member>>(json);
                Members = loadedMembers ?? new List<Member>();
            }
        }
    }
}
