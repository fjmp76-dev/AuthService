using AuthService.Models;

namespace AuthService.Data
{
    public class CapturaStore
    {
        public static List<CapturaRecord> Records { get; } =
        [
            new CapturaRecord { Id = 1, Nombre = "Juan",  Apellido = "Pérez",  Email = "juan@test.com",
                Telefono = "555-1111", Direccion = "Calle 1", Ciudad = "CDMX",   Notas = "" },
            new CapturaRecord { Id = 2, Nombre = "María", Apellido = "López",  Email = "maria@test.com",
                Telefono = "555-2222", Direccion = "Calle 2", Ciudad = "GDL",    Notas = "VIP" },
            new CapturaRecord { Id = 3, Nombre = "Pedro", Apellido = "García", Email = "pedro@test.com",
                Telefono = "555-3333", Direccion = "Calle 3", Ciudad = "MTY",    Notas = "" },
            new CapturaRecord { Id = 4, Nombre = "Ana",   Apellido = "Torres", Email = "ana@test.com",
                Telefono = "555-4444", Direccion = "Calle 4", Ciudad = "Puebla", Notas = "" },
            new CapturaRecord { Id = 5, Nombre = "Luis",  Apellido = "Ramos",  Email = "luis@test.com",
                Telefono = "555-5555", Direccion = "Calle 5", Ciudad = "Cancún", Notas = "" },
            new CapturaRecord { Id = 6, Nombre = "Sara",  Apellido = "Díaz",   Email = "sara@test.com",
                Telefono = "555-6666", Direccion = "Calle 6", Ciudad = "Mérida", Notas = "" },
        ];

        private static int _nextId = 7;

        public static CapturaRecord GetById(int id) =>
            Records.FirstOrDefault(r => r.Id == id);

        public static List<CapturaRecord> GetAll() => Records;

        public static CapturaRecord Add(CapturaRecord record)
        {
            record.Id = _nextId++;
            Records.Add(record);
            return record;
        }

        public static CapturaRecord Update(CapturaRecord record)
        {
            var existing = GetById(record.Id);
            if (existing is null) return null;

            existing.Nombre = record.Nombre;
            existing.Apellido = record.Apellido;
            existing.Email = record.Email;
            existing.Telefono = record.Telefono;
            existing.Direccion = record.Direccion;
            existing.Ciudad = record.Ciudad;
            existing.Notas = record.Notas;

            return existing;
        }

        public static bool Delete(int id)
        {
            var record = GetById(id);
            if (record is null) return false;
            Records.Remove(record);
            return true;
        }
    }
}
