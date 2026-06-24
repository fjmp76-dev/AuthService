using AuthService.Data;
using AuthService.Models;

namespace AuthService.Services.Captura;

public class CapturaService : ICapturaService
{
    public List<CapturaRecord> GetAll() => CapturaStore.GetAll();

    public CapturaRecord GetById(int id) => CapturaStore.GetById(id);

    public CapturaRecord Add(CapturaRecord record) => CapturaStore.Add(record);

    public CapturaRecord Update(CapturaRecord record) => CapturaStore.Update(record);

    public bool Delete(int id) => CapturaStore.Delete(id);
}